using BlazorSignalRApp;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
await builder.Build().RunAsync();

builder.Services.AddLocalization();

var app = builder.Build();

