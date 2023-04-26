using System;
using System.ComponentModel;
using System.Xml.Linq;

public class Ingredient
{
    public int? IngredientId { get; set; }
    private string ingredientName;
    private decimal amount;
    private string unit;

    public override string ToString()
    {
        return $"{ingredientName} - {amount} {unit}";
    }
    public string IngredientName
    {
        get { return ingredientName; }
        set
        {
            ingredientName = value;
        }
    }

    public decimal Amount
    {
        get { return amount; }
        set
        {
            amount = value;
        }
    }

    public string Unit
    {
        get { return unit;}
        set
        {
            unit = value;
        }
    }
}
