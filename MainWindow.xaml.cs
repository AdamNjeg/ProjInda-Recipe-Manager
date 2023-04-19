using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;

namespace ProjInda_Recipe_Manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Recipe> myRecipes { get; set; }
        public Window1 newWindow;
        public Recipe newRecipe { get; set; }

        public MainWindow()
        {
            RecipeManager theRecipeManager = new RecipeManager();
            myRecipes = theRecipeManager.GetAllRecipes();
            InitializeComponent();
            RecipeListbox.ItemsSource = myRecipes;

        }

        private void TestButton_Click(object sender, RoutedEventArgs e)
        {
            
            Window1 addRecipeWindow = new Window1();
            if (addRecipeWindow.ShowDialog() == true)
            {   
                newRecipe = new Recipe
                {
                    Name = addRecipeWindow.recipe.Name,
                    Instructions = addRecipeWindow.recipe.Instructions,
                    Ingredients = addRecipeWindow.recipe.Ingredients
                };
                myRecipes.Add(newRecipe);
                using (var connection = new SQLiteConnection("Data Source=TheDataBase.db;Version=3;"))
                {
                    connection.Open();

                    // Perform CRUD operations using the connection

                    string recipeSql = "INSERT INTO Recipes (Name, Instructions) VALUES (@recipeName, @instructions)";
                    string ingredientSql = "INSERT INTO Ingredients (Name, Amount, Unit) VALUES (@ingredientName, @amount, @unit)";
                    string recipeIngredientSql = "INSERT INTO RecipeIngredients (RecipeId, IngredientId) VALUES (@recipeId, @ingredientId)";

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

                                // Execute the Ingredient query
                                ingredientCommand.ExecuteNonQuery();

                                // Get the last inserted row ID (IngredientId)
                                int ingredientId = (int)connection.LastInsertRowId;

                                // Create a new SQLite command for inserting RecipeIngredient data
                                using (SQLiteCommand recipeIngredientCommand = new SQLiteCommand(recipeIngredientSql, connection))
                                {
                                    // Replace the parameters with actual values from the Recipe object and the current Ingredient object
                                    recipeIngredientCommand.Parameters.AddWithValue("@recipeId", recipeId);
                                    recipeIngredientCommand.Parameters.AddWithValue("@ingredientId", ingredientId);

                                    // Execute the RecipeIngredient query
                                    recipeIngredientCommand.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                    connection.Close();
                }

                RecipeListbox.Items.Refresh();
            }
        }
    }
}
