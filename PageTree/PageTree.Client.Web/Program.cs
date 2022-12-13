using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PageTree.Client.Shared;
using PageTree.Client.Web;
using PageTree.Client.Web.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMediator();
builder.Services.AddCQRS();

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes
        .Add("https://pagetree.onmicrosoft.com/32b11564-4bac-4a95-b8eb-0bdccefd99db/access_as_user");
});

builder.Services.AddAuthorizationAndSignInRedirection<
    WebAuthUser, WebSignInRedirector, AccessTokenNotAvailableException, BaseAddressAuthorizationMessageHandler>(
    builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();
