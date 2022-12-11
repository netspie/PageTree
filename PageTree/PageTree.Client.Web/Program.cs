using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PageTree.Client.Shared.Services;
using PageTree.Client.Web;
using PageTree.Client.Web.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("authorized", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient("anonymous", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

builder.Services.AddMediator();
builder.Services.AddCQRS(builder.HostEnvironment.BaseAddress);

builder.Services.AddTransient<IAuthUser, WebAuthUser>();

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes
        .Add("https://pagetree.onmicrosoft.com/32b11564-4bac-4a95-b8eb-0bdccefd99db/access_as_user");
});

await builder.Build().RunAsync();
