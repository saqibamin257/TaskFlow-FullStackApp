using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using TaskFlow.BuildingBlocks.Localization.Abstraction;

namespace TaskFlow.BuildingBlocks.Localization.Services
{
    public class JsonLocalizationService
    : ILocalizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JsonLocalizationService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetString(string key)
        {
            var language =
                _httpContextAccessor.HttpContext?
                    .Request.Headers["Accept-Language"]
                    .ToString()
                    .Split(',')
                    .FirstOrDefault()
                    ?.Trim()
                    .ToLower()
                ?? "en";

            var filePath = Path.Combine(
                AppContext.BaseDirectory,
                "Localization",
                "Resources",
                $"{language}.json");

            if (!File.Exists(filePath))
            {
                filePath = Path.Combine(
                    AppContext.BaseDirectory,
                    "Localization",
                    "Resources",
                    "en.json");
            }

            var json = File.ReadAllText(filePath);

            var translations =
                JsonSerializer.Deserialize<
                    Dictionary<string, string>>(json);

            if (translations is null)
            {
                return key;
            }

            return translations.TryGetValue(key, out var value)
                ? value
                : key;
        }
    }
}
