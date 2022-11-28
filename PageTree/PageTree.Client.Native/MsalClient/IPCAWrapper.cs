using Microsoft.Identity.Client;

namespace PageTree.Client.Native.MsalClient
{
	internal interface IPCAWrapper
	{
		string[] Scopes { get; set; }
		Task<AuthenticationResult> AcquireTokenInteractiveAsync(string[] scopes);
		Task<AuthenticationResult> AcquireTokenSilentAsync(string[] scopes);
		Task SignOutAsync();
	}
}
