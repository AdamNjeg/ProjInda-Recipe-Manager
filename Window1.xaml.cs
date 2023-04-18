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
using System.Windows.Shapes;

namespace ProjInda_Recipe_Manager
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private Recipe recipe;

        public Window1()
        {   
            InitializeComponent();
            recipe = new Recipe();
            this.DataContext = recipe;
        }

        private void btnAddIngredient_Click(object sender, RoutedEventArgs e)
        {
            AddIngredientWindow addIngredientWindow = new AddIngredientWindow();
            if (addIngredientWindow.ShowDialog() == true)
            {
                Ingredient ingredient = new Ingredient
                {
                    IngredientName = addIngredientWindow.IngredientName,
                    Amount = addIngredientWindow.IngredientAmount,
                    Unit = addIngredientWindow.IngredientUnit
                };
                recipe.Ingredients.Add(ingredient);
                IngredientsList.Items.Add(ingredient);
            }

        }

        private void btnRemoveIngredient_Click_1(object sender, RoutedEventArgs e)
        {
            Ingredient? selectedIngredient = IngredientsList.SelectedItem as Ingredient;
            if (selectedIngredient != null)
            {
                recipe.Ingredients.Remove(selectedIngredient);
                IngredientsList.Items.Remove(selectedIngredient);

            }
        }
    }
}
