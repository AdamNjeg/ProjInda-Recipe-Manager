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
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            
            InitializeComponent();
            RecipeListbox.ItemsSource = Global.myRecipes;
        }

        private void RecipeListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(RecipeListbox.SelectedItem == null)
            {
                titleBox.Content = "No recipe chosen";
                Instructions.Content = "Instructions..";
                Ingredientslist.Content = "Ingredients..";
            }
            else
            {
                Recipe chosenRecipe = RecipeListbox.SelectedItem as Recipe;
                titleBox.Content = chosenRecipe.Name;
                Instructions.Content = chosenRecipe.Instructions;

                StringBuilder ingredientsText = new StringBuilder();
                foreach (var ingredient in chosenRecipe.Ingredients)
                {
                    ingredientsText.AppendLine(ingredient.ToString());
                }
                Ingredientslist.Content = ingredientsText.ToString();
                Instructions.Content = chosenRecipe.Instructions;
            }
           
        }
        private void Backbtn_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new startPage();
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {   
            if (RecipeListbox.SelectedItem != null) {
                Recipe selectedRecipe = RecipeListbox.SelectedItem as Recipe;
                if (MessageBox.Show("Are you sure you want to delete " + selectedRecipe.Name + "? This action cannot be undone.", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    //do no stuff
                }
                else
                {
                    
                    Global.theRecipeManager.deleteRecipe(selectedRecipe);
                    RecipeListbox.Items.Refresh();
                }
                
            }
           
            
        }
    }
}
