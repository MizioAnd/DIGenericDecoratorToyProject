using System.Collections;
using Microsoft.AspNetCore.Mvc;
using ProductServices.Services;
using ProductServices.Services.Generic;
using ProductServices.ViewModel;

namespace ProductServices.Controllers.Generic;

[ApiController]
[Route("[controller]")]
public class InventoryController : Controller
{
    private readonly ICommandService<AdjustInventory> _service;
    private readonly AdjustInventoryViewModel _adjustInventoryViewModel;

    public InventoryController(ICommandService<AdjustInventory> service, AdjustInventoryViewModel adjustInventoryViewModel)
    {
        _adjustInventoryViewModel = adjustInventoryViewModel;
        _service = service;
    }

    [HttpPost]
    public ActionResult AdjustInventory(AdjustInventoryViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        return RedirectToAction("Index");
    }

    // called by /Inventory as endpoint
    [HttpGet(Name = "TestIfGenericDecoratorGetsRegistered")]
    public IEnumerable Get()
    {
        AdjustInventory command = _adjustInventoryViewModel.Command;

        try
        {
            _service.Execute(command);
        }
        catch (Exception e)
        {
            // Normally we would handle inner exception in this outer scope and display a nice message to user
            Console.WriteLine("Call to service failed");
            Console.WriteLine(e.Message);
            // But throw to test output
            throw;
        }

        return Enumerable.Range(1, 5).ToArray();
    }
}