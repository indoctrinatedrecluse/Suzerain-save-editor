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

                if (bool.TryParse(valueStr, out bool boolValue))
                {
                    data[key] = boolValue;
                }
                else if (int.TryParse(valueStr, out int intValue))
                {
                    data[key] = intValue;
                }
                else if (double.TryParse(valueStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double doubleValue))
                {
                    data[key] = doubleValue;
                }
                else
                {
                    data[key] = valueStr;
                }
            }
            return data;
        }

        public string CreateModifiedSaveContent(string? originalContent, Dictionary<string, object> modifiedValues)
        {
            if (originalContent == null) return string.Empty;

            var newContent = originalContent;
            foreach (var pair in modifiedValues)
            {
                // Regex updated to handle optional whitespace.
                string pattern = $@"(\[\\\""{Regex.Escape(pair.Key)}\\\""\]\s*=\s*)([\w\.-]+|true|false)";
                string replacement = $"${{1}}{pair.Value?.ToString()?.ToLower()}";
                newContent = Regex.Replace(newContent, pattern, replacement);
            }
            return newContent;
        }

        public async Task WriteSaveGameAsync(string filePath, string content)
        {
            await File.WriteAllTextAsync(filePath, content);
        }
    }
}