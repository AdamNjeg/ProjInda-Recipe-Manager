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
        public MainWindow()
        {
            Global.theRecipeManager = new RecipeManager();
            Global.myRecipes = Global.theRecipeManager.GetAllRecipes();
            InitializeComponent();
            startPage StartPage = new startPage();
            this.Content = StartPage;
            this.DataContext = this;

        }
    }
}
        