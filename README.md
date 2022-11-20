# DIGenericDecoratorToyProject

To test that the Polly retry and wait decorator prevents `AdjustInventoryFailingService` from failing using the generic code perform the following request twice,
```
https://localhost:7180/Inventory
```

For the non-generic code perform the following request,
```
https://localhost:7180/Inventory/non-generic/
```
