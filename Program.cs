using BlazorSignalRApp.Client.Pages;
using BlazorSignalRApp.Components;
using Microsoft.AspNetCore.ResponseCompression;
using ChatApp.Hubs;
using BlazorSignalRApp;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();


builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        ["application/octet-stream"]);
});
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddLocalization();

var app = builder.Build();

string[] strings = new string[] { "en-US", "es" };
var localizationOptions = new RequestLocalizationOptions()
   .SetDefaultCulture(strings[0])
   .AddSupportedCultures(strings)
   .AddSupportedUICultures(strings);
app.UseRequestLocalization(localizationOptions);

app.UseResponseCompression();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(BlazorSignalRApp.Client._Imports).Assembly);
app.MapHub<ChatHub>("/chathub");
app.Run();
