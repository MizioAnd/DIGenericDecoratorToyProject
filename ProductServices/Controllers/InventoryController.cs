using System.Collections;
using Microsoft.AspNetCore.Mvc;
using ProductServices.Services;
using ProductServices.ViewModel;

namespace ProductServices.Controllers;

[ApiController]
[Route("[controller]/non-generic/")]
public class InventoryController : Controller
{
    private readonly ICommandService _service;
    private readonly AdjustInventoryViewModel _adjustInventoryViewModel;

    public InventoryController(ICommandService service, AdjustInventoryViewModel adjustInventoryViewModel)
    {
        _service = service;
        _adjustInventoryViewModel = adjustInventoryViewModel;
    }

    // TODO: not working
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
    [HttpGet(Name = "TestIfNonGenericDecoratorGetsRegistered")]
    public IEnumerable Get()
    {
        AdjustInventory command = _adjustInventoryViewModel.Command;

        _service.Execute(command);
        return Enumerable.Range(1, 5).ToArray();
    }
}