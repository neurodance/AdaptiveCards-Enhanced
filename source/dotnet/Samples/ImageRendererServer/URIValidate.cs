using System;

namespace ImageRendererServer.Controllers
{
    /// <summary>
    /// Helper for validating URLs against an allow-list of domains.
    /// Used to mitigate SSRF by ensuring outbound requests target only trusted hosts.
    /// </summary>
    internal static class URIValidate
    {
        /// <summary>
        /// Returns true if <paramref name="url"/> is an absolute http/https URL whose
        /// host exactly matches (or is a subdomain of) one of <paramref name="allowedDomains"/>.
        /// </summary>
        public static bool InDomain(string url, string[] allowedDomains)
        {
            if (string.IsNullOrWhiteSpace(url) || allowedDomains == null)
            {
                return false;
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
            {
                return false;
            }

            if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            {
                return false;
            }

            string host = uri.Host;
            foreach (var domain in allowedDomains)
            {
                if (string.IsNullOrWhiteSpace(domain))
                {
                    continue;
                }

                if (host.Equals(domain, StringComparison.OrdinalIgnoreCase) ||
                    host.EndsWith("." + domain, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
