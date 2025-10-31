using System;

namespace CookieCookbook.Models
{
    public class Ingredient
    {
        public int Id { get; }
        public string Name { get; }
        public string Preparation { get; }

        public Ingredient(int id, string name, string preparation)
        {
            Id = id;
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Preparation = preparation ?? throw new ArgumentNullException(nameof(preparation));
        }

        public override string ToString() => $"{Name}. {Preparation}";
    }
}
