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

        // In case of LSP violation when _service is not an object implementation using AdjustInventory (the method call on _service.Execute(..) will fail) 
        // but some other type and this will throw 'Object reference not set to an instance of an object' as ICommandService type was not registered correctly in DI
        // as it would not be using a type cast to AdjustInventory as below,
        // var command = (AdjustInventory)cmd;
        // but instead would try to cast to some other object not being possible when cmd is of type AdjustInventory.
        _service.Execute(command);
        return Enumerable.Range(1, 5).ToArray();
    }
}