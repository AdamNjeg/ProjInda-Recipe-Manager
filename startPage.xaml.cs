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

namespace ProjInda_Recipe_Manager
{
    /// <summary>
    /// Interaction logic for startPage.xaml
    /// </summary>
    public partial class startPage : Page
    {
        public Window1 newWindow;
        public startPage()
        {
            InitializeComponent();
        }
        private void TestButton_Click(object sender, RoutedEventArgs e)
        {

            Window1 addRecipeWindow = new Window1();
            if (addRecipeWindow.ShowDialog() == true)
            {
                Recipe newRecipe = new Recipe
                {
                    Name = addRecipeWindow.recipe.Name,
                    Instructions = addRecipeWindow.recipe.Instructions,
                    Ingredients = addRecipeWindow.recipe.Ingredients
                };
                Global.myRecipes.Add(newRecipe);
                Global.theRecipeManager.saveRecipe(newRecipe);

            }
        }

        private void ViewRecipes_Click(object sender, RoutedEventArgs e)
        {
            Page1 viewrecipe = new Page1();
            Window.GetWindow(this).Content = viewrecipe;
        }
    }
}


