using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ProjInda_Recipe_Manager
{
    public class RecipeManager
    {
        // Connection string for your SQLite database
        private string connectionString = @"Data Source=TheDataBase.db;Version=3;";
        public void saveRecipe(Recipe newRecipe)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Perform CRUD operations using the connection

                string recipeSql = "INSERT INTO Recipes (Name, Instructions) VALUES (@recipeName, @instructions)";
                string ingredientSql = "INSERT INTO Ingredients (Name, Amount, Unit, RecipeID) VALUES (@ingredientName, @amount, @unit, @recipeID)";
                

                // Create a new SQLite command for inserting Recipe data
                using (SQLiteCommand recipeCommand = new SQLiteCommand(recipeSql, connection))
                {
                    // Replace the parameters with actual values from the Recipe object
                    recipeCommand.Parameters.AddWithValue("@recipeName", newRecipe.Name); // Replace with the actual RecipeName value
                    recipeCommand.Parameters.AddWithValue("@instructions", newRecipe.Instructions); // Replace with the actual Instructions value

                    // Execute the Recipe query
                    recipeCommand.ExecuteNonQuery();

                    // Get the last inserted row ID (RecipeId)
                    int recipeId = (int)connection.LastInsertRowId;

                    // Create a new SQLite command for inserting Ingredient data
                    using (SQLiteCommand ingredientCommand = new SQLiteCommand(ingredientSql, connection))
                    {
                        // Loop through the list of Ingredients in the Recipe object
                        foreach (Ingredient ingredient in newRecipe.Ingredients)
                        {
                            // Replace the parameters with actual values from the Ingredient object
                            ingredientCommand.Parameters.AddWithValue("@ingredientName", ingredient.IngredientName); // Replace with the actual IngredientName value
                            ingredientCommand.Parameters.AddWithValue("@amount", ingredient.Amount); // Replace with the actual Amount value
                            ingredientCommand.Parameters.AddWithValue("@unit", ingredient.Unit); // Replace with the actual Unit value
                            ingredientCommand.Parameters.AddWithValue("@recipeId", recipeId);

                            // Execute the Ingredient query
                            ingredientCommand.ExecuteNonQuery();
                            
                        }
                    }
                }
                connection.Close();
            }

        }
        
        
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
                            string selectIngredientsQuery = "SELECT * FROM Ingredients WHERE RecipeId = @RecipeId";
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
                                        ingredient.Unit = ingredientReader["Unit"].ToString();
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
