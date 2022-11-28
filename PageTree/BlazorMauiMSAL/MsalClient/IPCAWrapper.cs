using Microsoft.Identity.Client;

namespace BlazorMauiMSAL.MsalClient
{
	internal interface IPCAWrapper
	{
		string[] Scopes { get; set; }
		Task<AuthenticationResult> AcquireTokenInteractiveAsync(string[] scopes);
		Task<AuthenticationResult> AcquireTokenSilentAsync(string[] scopes);
		Task SignOutAsync();
	}
}
