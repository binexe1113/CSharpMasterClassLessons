using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CookieCookbook.Models;
using CookieCookbook.Storage;

internal static class Program
{
    // Change this const to FileFormat.Txt if you prefer .txt file storage.
    private const FileFormat AppFileFormat = FileFormat.Json;
    private const string DataFileNameTxt = "recipes.txt";
    private const string DataFileNameJson = "recipes.json";

    private static string DataFilePath => AppFileFormat == FileFormat.Txt ? DataFileNameTxt : DataFileNameJson;

    private static readonly List<Ingredient> AvailableIngredients = new()
    {
        new Ingredient(1, "Wheat flour", "Sieve. Add to other ingredients."),
        new Ingredient(2, "Coconut flour", "Sieve. Add to other ingredients."),
        new Ingredient(3, "Butter", "Melt on low heat. Add to other ingredients."),
        new Ingredient(4, "Chocolate", "Melt in a water bath. Add to other ingredients."),
        new Ingredient(5, "Sugar", "Add to other ingredients."),
        new Ingredient(6, "Cardamom", "Take half a teaspoon. Add to other ingredients."),
        new Ingredient(7, "Cinnamon", "Take half a teaspoon. Add to other ingredients."),
        new Ingredient(8, "Cocoa powder", "Add to other ingredients.")
    };

    private static void Main()
    {
        var repo = new RecipeRepository(DataFilePath, AppFileFormat);

        // 1) Load existing recipes (if any) and print them
        var stored = repo.LoadAllStorageStrings();
        if (stored.Any())
        {
            Console.WriteLine("Existing recipes are:");
            Console.WriteLine();
            int n = 1;
            foreach (var storageString in stored)
            {
                Console.WriteLine($"***** {n} *****");
                var recipe = Recipe.FromStorageString(storageString);
                PrintSingleRecipe(recipe);
                Console.WriteLine();
                n++;
            }
        }

        // 2) Print available ingredients
        Console.WriteLine("Create a new cookie recipe! Available ingredients are:");
        foreach (var ing in AvailableIngredients)
        {
            Console.WriteLine($"{ing.Id}. {ing.Name}");
        }

        // 3) Selection loop
        var selectedIds = new List<int>();
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("Add an ingredient by its ID or type anything else if finished.");
            Console.Write("> ");
            var input = Console.ReadLine();
            if (input == null)
            {
                // treat null as finished
                break;
            }

            if (int.TryParse(input.Trim(), out var id))
            {
                var ingredient = AvailableIngredients.FirstOrDefault(i => i.Id == id);
                if (ingredient != null)
                {
                    selectedIds.Add(id);
                    Console.WriteLine($"Added: {ingredient.Name}");
                    // loop continues
                }
                else
                {
                    // number that doesn't match any ingredient -> do nothing and continue loop
                    // Per spec "we execute the loop again"
                    Console.WriteLine("No ingredient with such ID. Try again or type anything else to finish.");
                }
            }
            else
            {
                // anything else -> finish selection
                break;
            }
        }

        var newRecipe = new Recipe(selectedIds);

        // 4) If no ingredients -> print message, do not save
        if (!newRecipe.IsValid())
        {
            Console.WriteLine();
            Console.WriteLine("No ingredients have been selected. Recipe will not be saved.");
        }
        else
        {
            // Save and print
            repo.AppendStorageString(newRecipe.ToStorageString());
            Console.WriteLine();
            Console.WriteLine("Recipe added:");
            PrintSingleRecipe(newRecipe);
        }

        Console.WriteLine();
        Console.WriteLine("Press any key to exit.");
        Console.ReadKey(true);
    }

    private static void PrintSingleRecipe(Recipe recipe)
    {
        // Each ingredient in its own line with name + preparation
        foreach (var id in recipe.IngredientIds)
        {
            var ingredient = AvailableIngredients.FirstOrDefault(i => i.Id == id);
            if (ingredient != null)
            {
                Console.WriteLine(ingredient.ToString());
            }
            else
            {
                // If ingredient ID is unknown, print it as raw ID (defensive)
                Console.WriteLine($"Unknown ingredient id {id}");
            }
        }
    }
}
