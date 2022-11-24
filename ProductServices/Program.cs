using ProductServices.Services;
using ProductServices.ViewModel;
using Generic = ProductServices.Services.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IInventoryRepository, Repository>();
builder.Services.AddSingleton<AdjustInventory>();
builder.Services.AddSingleton<AdjustInventoryViewModel>();

// Non-generic case
builder.Services.AddSingleton<AdjustInventoryService>();
builder.Services.AddSingleton<ICommandService>(p => new TransactionCommandServiceDecorator(p.GetRequiredService<AdjustInventoryService>()));

// Generic case
builder.Services.AddSingleton<Generic.AdjustInventoryService>();
// builder.Services.AddSingleton<Generic.ICommandService<AdjustInventory>>(p => new Generic.TransactionCommandServiceDecorator<AdjustInventory>(p.GetRequiredService<Generic.AdjustInventoryService>()));

// Generic Failing service case that when decorated with retry decorator does not fail
// Fails
// builder.Services.AddSingleton<Generic.ICommandService<AdjustInventory>>(p => new Generic.TransactionCommandServiceDecorator<AdjustInventory>(p.GetRequiredService<Generic.AdjustInventoryFailingService>()));
// builder.Services.AddSingleton<Generic.ICommandService<AdjustInventory>>(p => new Generic.TransactionCommandServiceDecorator<AdjustInventory>(
//     new Generic.FailingServiceDecorator<AdjustInventory>(p.GetRequiredService<IInventoryRepository>(), p.GetRequiredService<Generic.AdjustInventoryService>())));
// Succeeds with retry decorator
builder.Services.AddSingleton<Generic.ICommandService<AdjustInventory>>(p => new Generic.TransactionCommandServiceDecorator<AdjustInventory>(
    new Generic.RetryDecorator<AdjustInventory>(
        new Generic.FailingServiceDecorator<AdjustInventory>(p.GetRequiredService<IInventoryRepository>(), p.GetRequiredService<Generic.AdjustInventoryService>()))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
