using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CookieCookbook.Storage
{
    public class RecipeRepository
    {
        private readonly string _filePath;
        private readonly FileFormat _format;

        public RecipeRepository(string filePath, FileFormat format)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            _format = format;
        }

        public List<string> LoadAllStorageStrings()
        {
            if (!File.Exists(_filePath))
                return new List<string>();

            try
            {
                if (_format == FileFormat.Txt)
                {
                    var lines = File.ReadAllLines(_filePath);
                    return lines.Where(l => !string.IsNullOrWhiteSpace(l)).Select(l => l.Trim()).ToList();
                }
                else // Json
                {
                    var json = File.ReadAllText(_filePath);
                    if (string.IsNullOrWhiteSpace(json))
                        return new List<string>();

                    var list = JsonSerializer.Deserialize<List<string>>(json);
                    return list ?? new List<string>();
                }
            }
            catch
            {
                // If file is corrupt/unreadable, treat as empty.
                return new List<string>();
            }
        }

        public void AppendStorageString(string storageString)
        {
            var existing = LoadAllStorageStrings();
            existing.Add(storageString);

            try
            {
                if (_format == FileFormat.Txt)
                {
                    // Write all lines (to preserve consistent state)
                    File.WriteAllLines(_filePath, existing);
                }
                else // Json
                {
                    var json = JsonSerializer.Serialize(existing, new JsonSerializerOptions { WriteIndented = true });
                    File.WriteAllText(_filePath, json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save recipe: {ex.Message}");
                // swallow after writing message per console-app simplicity
            }
        }
    }
}
