﻿using System;
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
    /// Interaction logic for AddRecipePage.xaml
    /// </summary>
    public partial class AddRecipePage : Page
    {
        public Recipe recipe { get; set; }
        public static List<int> numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };



        public AddRecipePage()
        {
            InitializeComponent();
            portionscombobox.SelectedIndex = 0;
            portionscombobox.ItemsSource = numbers;
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

        private void SaveRecipe_btn_Click(object sender, RoutedEventArgs e)
        {
            if (recipeName_txtBox.Text != "")
            {
                recipe.Name = recipeName_txtBox.Text;
                recipe.Instructions = instructions_textbox.Text;
                recipe.Portions = (int)portionscombobox.SelectedItem;
                Global.myRecipes.Add(recipe);
                Global.theRecipeManager.saveRecipe(recipe);

                Window.GetWindow(this).Content = new startPage();
            }
            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Content = new startPage();
        }
    }
}
