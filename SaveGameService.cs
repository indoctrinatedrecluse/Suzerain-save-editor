using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SuzerainSaveEditor
{
    public class SaveGameService
    {
        public async Task<string> LoadSaveGameAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Save file not found.", filePath);
            }
            return await File.ReadAllTextAsync(filePath);
        }

        public Dictionary<string, object> ParseSaveGame(string content)
        {
            var data = new Dictionary<string, object>();
            // Regex updated to handle optional whitespace around the equals sign.
            var regex = new Regex(@"\[\\\""(?<key>[\w\._-]+)\\\""\]\s*=\s*(?<value>[\w\.-]+|true|false)");

            foreach (Match match in regex.Matches(content))
            {
                string key = match.Groups["key"].Value;
                string valueStr = match.Groups["value"].Value;

                if (TryParsePrimitiveValue(valueStr, out var value))
                {
                    data[key] = value;
                }
            }
            return data;
        }

        public bool TryParsePrimitiveValue(string value, out object parsedValue)
        {
            if (bool.TryParse(value, out bool boolValue))
            {
                parsedValue = boolValue;
                return true;
            }

            if (int.TryParse(value, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture, out int intValue))
            {
                parsedValue = intValue;
                return true;
            }

            if (double.TryParse(value, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out double doubleValue))
            {
                if (double.IsNaN(doubleValue) || double.IsInfinity(doubleValue))
                {
                    parsedValue = string.Empty;
                    return false;
                }

                parsedValue = doubleValue;
                return true;
            }

            parsedValue = string.Empty;
            return false;
        }

        public string CreateModifiedSaveContent(string? originalContent, Dictionary<string, object> modifiedValues)
        {
            if (originalContent == null) return string.Empty;

            var newContent = originalContent;
            foreach (var pair in modifiedValues)
            {
                // Regex updated to handle optional whitespace.
                string pattern = $@"(\[\\\""{Regex.Escape(pair.Key)}\\\""\]\s*=\s*)([\w\.-]+|true|false)";
                string replacement = $"${{1}}{FormatValue(pair.Value)}";
                newContent = Regex.Replace(newContent, pattern, replacement);
            }
            return newContent;
        }

        private static string FormatValue(object? value)
        {
            return value switch
            {
                bool boolValue => boolValue ? "true" : "false",
                IFormattable formattable => formattable.ToString(null, System.Globalization.CultureInfo.InvariantCulture),
                _ => value?.ToString() ?? string.Empty
            };
        }

        public async Task WriteSaveGameAsync(string filePath, string content)
        {
            await File.WriteAllTextAsync(filePath, content);
        }
    }
}