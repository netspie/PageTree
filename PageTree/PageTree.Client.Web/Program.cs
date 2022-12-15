using Corelibs.BlazorShared;
using Corelibs.BlazorWebAssembly;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PageTree.Client.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.Cache.CacheLocation = "localStorage";
    options.ProviderOptions.DefaultAccessTokenScopes
        .Add("https://pagetree.onmicrosoft.com/32b11564-4bac-4a95-b8eb-0bdccefd99db/access_as_user");
});

builder.Services.AddAuthorizationAndSignInRedirection(builder.HostEnvironment.BaseAddress);
builder.Services.AddCQRS();

await builder.Build().RunAsync();
