using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using HtmlAgilityPack;

namespace ProjInda_Recipe_Manager
{

    public static class RecipeDownloader
    {

        public static void RecipeDownloadICA(string url)
        {
            Recipe newRecipe = new Recipe();
            

            

            System.Net.WebClient wc = new System.Net.WebClient();
            byte[] raw = wc.DownloadData(url);


            string webData = Encoding.UTF8.GetString(raw);






            // ...

            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(webData);
            
            //Get the name of the recipe
            var h1Node = htmlDoc.DocumentNode.SelectSingleNode("//h1");
            if (h1Node != null)
            {
                string h1Text = h1Node.InnerText.Trim();
                newRecipe.Name = h1Text;
            }
            //Get instructions
            var cookingSteps = htmlDoc.DocumentNode.Descendants("div")
                .Where(d => d.GetAttributeValue("class", "") == "cooking-steps-main__text")
                .Select(d => HtmlEntity.DeEntitize(d.InnerText.Trim()));


            string stepsString = string.Join("\n\n", cookingSteps);

            newRecipe.Instructions = stepsString;


            // Get the recipe portions
            var portionsDiv = htmlDoc.DocumentNode.SelectSingleNode("//div[@class='ingredients-change-portions']");
            var portionsText = portionsDiv.InnerText.Trim();

            // Extract the number from the text using regular expressions
            var match = Regex.Match(portionsText, @"\d+");
            if (match.Success)
            {
                var portions = int.Parse(match.Value);
                newRecipe.Portions = portions;
            }


            var ingredientGroups = htmlDoc.DocumentNode.Descendants("div")
                .Where(d => d.GetAttributeValue("class", "") == "ingredients-list-group__card");

            foreach (var group in ingredientGroups)
            {
                var qtySpan = group.Descendants("span")
                    .FirstOrDefault(d => d.GetAttributeValue("class", "") == "ingredients-list-group__card__qty");
                var ingrSpan = group.Descendants("span")
                    .FirstOrDefault(d => d.GetAttributeValue("class", "") == "ingredients-list-group__card__ingr");

                Ingredient newIngredient = new Ingredient();
                if (qtySpan != null)
                {
                    var qty = qtySpan.InnerText.Trim();
                    string[] parts = qty.Split(' ');

                    string amount = parts[0];
                    string unit = parts.Length > 1 ? parts[1] : "";

                    decimal result;
                    if (Regex.IsMatch(amount, @"^\d+/\d+$"))
                    {
                        // Split the fraction into numerator and denominator
                        string[] amountParts = amount.Split('/');

                        decimal numerator = decimal.Parse(amountParts[0]);
                        decimal denominator = decimal.Parse(amountParts[1]);

                        // Divide the numerator by the denominator to get the result
                        result = numerator / denominator;
                    }
                    else
                    {
                        // Parse the input string as a decimal
                        result = decimal.Parse(amount);
                    }
                    newIngredient.Amount = result;
                    if (unit == "")
                    {
                        newIngredient.Unit = "st";
                    }
                    else
                    {
                        newIngredient.Unit = unit;
                    }
                    // Assign the result to the newIngredient.Amount property

                

                }
                else
                {
                    newIngredient.Unit = " ";
                }
                if (ingrSpan != null)
                {

                    var ingr = ingrSpan.InnerText.Trim();
                    newIngredient.IngredientName = ingr;
                    newRecipe.Ingredients.Add(newIngredient);

                }
            }
            Global.theRecipeManager.saveRecipe(newRecipe);
            Global.myRecipes.Add(newRecipe);


        }

    }
}
