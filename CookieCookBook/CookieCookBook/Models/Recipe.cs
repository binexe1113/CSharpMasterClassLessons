using System;
using System.Collections.Generic;
using System.Linq;

namespace CookieCookbook.Models
{
    public class Recipe
    {
        public List<int> IngredientIds { get; }

        public Recipe(IEnumerable<int> ids)
        {
            IngredientIds = ids?.ToList() ?? new List<int>();
        }

        public bool IsValid() => IngredientIds.Count > 0;

        /// <summary>
        /// Returns a string representation used for storage: "1,2,3"
        /// </summary>
        public string ToStorageString() => string.Join(',', IngredientIds);

        public static Recipe FromStorageString(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return new Recipe(Enumerable.Empty<int>());

            var parts = s.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var ids = new List<int>();
            foreach (var p in parts)
            {
                if (int.TryParse(p, out var id))
                    ids.Add(id);
                // ignore invalid entries (shouldn't happen if files are correct)
            }

            return new Recipe(ids);
        }
    }
}
