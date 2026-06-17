using System;
using System.Linq;

namespace GameCore.AnalyticService
{
    public static class UrlUtils
    {
        public enum ValidationResult
        {
            Valid,
            InvalidFormat,
            InvalidScheme,
            Empty
        }

        public static string[] SplitUrls(string input, char[] splitChars = null)
        {
            splitChars ??= new[] { ' ', '\n', '\r', ',' };

            return string.IsNullOrEmpty(input)
                ? Array.Empty<string>()
                : input.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
        }

        public static ValidationResult ValidateHttpHttpsUrl(string url)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
            {
                return ValidationResult.Empty;
            }

            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri uri))
            {
                return ValidationResult.InvalidFormat;
            }

            if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
            {
                return ValidationResult.InvalidScheme;
            }

            return ValidationResult.Valid;
        }

        public static NormalizationResult[] NormalizeUrls(string urls)
        {
            NormalizationResult[] results = SplitUrls(urls)
                .Select(u =>
                    {
                        ValidationResult validationResult = ValidateHttpHttpsUrl(u);

                        return new NormalizationResult(
                            u,
                            validationResult
                        );
                    }
                )
                .ToArray();


            return results;
        }

        public class NormalizationResult
        {
            public string Url { get; }
            public ValidationResult ValidationResult { get; }

            public NormalizationResult(string url, ValidationResult validationResult)
            {
                Url = url;
                ValidationResult = validationResult;
            }
        }
    }
}
