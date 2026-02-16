var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.FluentUI>("fluentui");

builder.Build().Run();
