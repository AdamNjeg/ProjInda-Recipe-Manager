using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ProjInda_Recipe_Manager
{
    internal class RecipeManager
    {
        // Connection string for your SQLite database
        private string connectionString = @"Data Source=TheDataBase.db;Version=3;";

        // Retrieve all recipes from the database and store them in a list
        public  List<Recipe> GetAllRecipes()
        {
            List<Recipe> recipes = new List<Recipe>();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Retrieve recipes
                string selectRecipesQuery = "SELECT * FROM Recipes";
                using (SQLiteCommand command = new SQLiteCommand(selectRecipesQuery, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Recipe recipe = new Recipe();
                            recipe.RecipeId = Convert.ToInt32(reader["RecipeId"]);
                            recipe.Name = reader["Name"].ToString();
                            recipe.Ingredients = new List<Ingredient>();

                            // Retrieve ingredients for the current recipe
                            string selectIngredientsQuery = "SELECT * FROM RecipeIngredient WHERE RecipeId = @RecipeId";
                            using (SQLiteCommand ingredientCommand = new SQLiteCommand(selectIngredientsQuery, connection))
                            {
                                ingredientCommand.Parameters.AddWithValue("@RecipeId", recipe.RecipeId);
                                using (SQLiteDataReader ingredientReader = ingredientCommand.ExecuteReader())
                                {
                                    while (ingredientReader.Read())
                                    {
                                        Ingredient ingredient = new Ingredient();
                                        ingredient.IngredientId = Convert.ToInt32(ingredientReader["IngredientId"]);
                                        ingredient.IngredientName = ingredientReader["Name"].ToString();
                                        ingredient.Amount = (decimal)Convert.ToDouble(ingredientReader["Amount"]);
                                        recipe.Ingredients.Add(ingredient);
                                    }
                                }
                            }

                            recipes.Add(recipe);
                        }
                    }
                }

                connection.Close();
            }

            return recipes;
        }
    }
}
