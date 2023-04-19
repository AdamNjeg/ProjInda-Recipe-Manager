using System;
using System.Collections.Generic;

public class Recipe
{
    public int? RecipeId { get; set; }
    public string Name { get; set; }
    public string Instructions { get; set; }
    public List<Ingredient> Ingredients { get; set; }

    public override string ToString()
    {
        return $"{Name}";
    }

    public Recipe()
    {
        Ingredients = new List<Ingredient>();
    }
}
