var builder = DistributedApplication.CreateBuilder(args);

builder.Add
builder.AddProject<Projects.Basket_API>("basket");

builder.Build().Run();
