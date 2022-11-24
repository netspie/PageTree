namespace BlazorMauiMSAL
{
    internal class Settings
    {
        public string ClientId { get; set; }
        public string Tenant { get; set; }
        public string TenantId { get; set; }

        public string InstanceUrl { get; set; }
        public string PolicySignUpSignIn { get; set; }
        public string Authority { get; set; }
        public NestedSettings[] Scopes { get; set; }
        public string ApiUrl { get; set; }
    }

    internal class NestedSettings
    {
        public string Value { get; set; } = null;
    }

    internal static class Extensions
    {
        public static string[] ToStringArray(this NestedSettings[] nestedSettings)
        {
            var result = new string[nestedSettings.Length];

            for (int i = 0; i < nestedSettings.Length; i++)
                result[i] = nestedSettings[i].Value;

            return result!;
        }
    }
}
