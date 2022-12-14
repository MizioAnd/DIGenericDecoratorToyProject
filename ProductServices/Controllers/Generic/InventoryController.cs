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
        _adjustInventoryViewModel = adjustInventoryViewModel ?? throw new ArgumentNullException(nameof(adjustInventoryViewModel));
        _service = service ?? throw new ArgumentNullException(nameof(service));
    }

    // TODO: not working
    [HttpPost]
    public async Task<ActionResult> AdjustInventory(AdjustInventoryViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        return Ok(RedirectToAction("Index"));
    }

    // called by /Inventory as endpoint
    [HttpGet(Name = "TestIfGenericDecoratorGetsRegistered")]
    public async Task<ActionResult<IEnumerable>> Get()
    {
        AdjustInventory command = _adjustInventoryViewModel.Command;

        try
        {
            await Task.Run(() => _service.Execute(command));
        }
        catch (Exception e)
        {
            // Normally we would handle inner exception in this outer scope and display a nice message to user
            Console.WriteLine("Call to service failed");
            Console.WriteLine(e.Message);
            // But throw to test output
            throw;
        }

        return Ok(Enumerable.Range(1, 5).ToArray());
    }
}