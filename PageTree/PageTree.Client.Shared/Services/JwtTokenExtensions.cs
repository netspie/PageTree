using System.IdentityModel.Tokens.Jwt;

namespace PageTree.Client.Shared.Services
{
    public static class JwtTokenExtensions
    {
        public static bool IsExpiredToken(this JwtSecurityToken token, DateTime utcTimeNow)
        {
            if (token == null)
                return true;

            if (utcTimeNow > token.ValidFrom && utcTimeNow < token.ValidTo)
                return false;

            return true;
        }
    }
}
