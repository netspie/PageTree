using Android.App;
using Android.Content;
using Microsoft.Identity.Client;

namespace PageTree.Client.Native.Platforms.Android;

[Activity(Exported = true)]
[IntentFilter(new[] { Intent.ActionView },
    Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
    DataHost = "auth",
    DataScheme = "msal32b11564-4bac-4a95-b8eb-0bdccefd99db")]
public class MsalActivity : BrowserTabActivity
{
}
