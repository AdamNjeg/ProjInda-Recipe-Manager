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
        public RecipeManager theRecipeManager { get; set; }

        public MainWindow()
        {
            theRecipeManager = new RecipeManager();
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
                theRecipeManager.saveRecipe(newRecipe);
                RecipeListbox.Items.Refresh();
            }
        }
    }
}
