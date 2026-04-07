using System.Security.Claims;
using System.Text.Json;

namespace PatinhasMagicasPWA.Services
{
    public class JwtTokenParserService
    {
        public IEnumerable<Claim> ParseClaims(string jwt)
        {
            var payload = ParsePayload(jwt);

            if (payload is null)
            {
                return Enumerable.Empty<Claim>();
            }

            return payload.Select(claim => new Claim(claim.Key, claim.Value.ToString()));
        }

        public bool IsExpired(string jwt)
        {
            var payload = ParsePayload(jwt);

            if (payload is null || !payload.TryGetValue("exp", out var expElement))
            {
                return true;
            }

            long expUnix;

            if (expElement.ValueKind == JsonValueKind.Number && expElement.TryGetInt64(out expUnix))
            {
                return DateTimeOffset.UtcNow >= DateTimeOffset.FromUnixTimeSeconds(expUnix);
            }

            if (expElement.ValueKind == JsonValueKind.String &&
                long.TryParse(expElement.GetString(), out expUnix))
            {
                return DateTimeOffset.UtcNow >= DateTimeOffset.FromUnixTimeSeconds(expUnix);
            }

            return true;
        }

        private static Dictionary<string, JsonElement>? ParsePayload(string jwt)
        {
            try
            {
                var parts = jwt.Split('.');

                if (parts.Length < 2)
                {
                    return null;
                }

                var jsonBytes = ParseBase64WithoutPadding(parts[1]);
                return JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonBytes);
            }
            catch
            {
                return null;
            }
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            base64 = base64.Replace('-', '+').Replace('_', '/');

            switch (base64.Length % 4)
            {
                case 2:
                    base64 += "==";
                    break;
                case 3:
                    base64 += "=";
                    break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}
