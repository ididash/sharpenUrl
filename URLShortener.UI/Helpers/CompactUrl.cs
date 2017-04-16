using System;

namespace URLShortener.UI.Helpers
{
    public static class CompactUrl
    {
        public static string GenerateCompactUrl()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }
    }
}