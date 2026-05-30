using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using ccballot;
using ccballot.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<LocalStorageService>();

var app = builder.Build();

var js = app.Services.GetRequiredService<IJSRuntime>();
var redirect = await js.InvokeAsync<string?>("eval", """(function(){var r=sessionStorage.getItem('redirect');sessionStorage.removeItem('redirect');return r;})()""");
if (!string.IsNullOrEmpty(redirect))
{
    var nav = app.Services.GetRequiredService<NavigationManager>();
    nav.NavigateTo(redirect);
}

await app.RunAsync();