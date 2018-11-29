using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DCICC.Seguridad.TokensJWT
{
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }
}
