var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.ToyShopWebApp>("toyshopwebapp");

builder.Build().Run();
