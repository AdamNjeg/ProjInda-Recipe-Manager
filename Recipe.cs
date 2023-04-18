using System;
using System.Collections.Generic;

public class Recipe
{
    public string Name { get; set; }
    public string Instructions { get; set; }
    public List<Ingredient> Ingredients { get; set; }

    public Recipe()
    {
        Ingredients = new List<Ingredient>();
    }
}
