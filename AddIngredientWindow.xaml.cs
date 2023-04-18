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
using System.Xml.Linq;

namespace ProjInda_Recipe_Manager
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class AddIngredientWindow : Window
    {
        public AddIngredientWindow()
        {
            InitializeComponent();
        }
        public string IngredientName { get; set; }
        public decimal IngredientAmount { get; set; }
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            IngredientName = txtName.Text;
            IngredientAmount = decimal.Parse(txtAmount.Text);
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }

    
}
