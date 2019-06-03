using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DeepModelBureau
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddProdWindow : Window
    {
        public AddProdWindow()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (!isNumeric(txtName.Text) && isNumeric(txtPhoneNo.Text) && !isNumeric(txtAddress.Text) && isNumeric(txtHeight.Text) && isNumeric(txtWeight.Text) && !isNumeric(txtHairColor.Text) && !isNumeric(txtComments.Text))
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("FAILED! \nPlease enter valid information.");
            }
        }

        public string Name
        {
            get { return txtName.Text; }
        }
        public int PhoneNo
        {
            get { return int.Parse(txtPhoneNo.Text); }
        }
        public string Address
        {
            get { return txtAddress.Text; }
        }
        public int ModelHeight
        {
            get { return int.Parse(txtHeight.Text); }
        }
        public int ModelWeight
        {
            get { return int.Parse(txtWeight.Text); }
        }
        public string HairColor
        {
            get { return txtHairColor.Text; }
        }
        public string Comments
        {
            get { return txtComments.Text; }
        }

        private bool isNumeric(string s)
        {
            return Regex.IsMatch(s, @"^\d+$");
        }
    }
}

