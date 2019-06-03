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
    public partial class AddTaskWindow : Window
    {
        public AddTaskWindow()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            if (!isNumeric(txtCosName.Text) && isNumeric(txtDate.Text) && isNumeric(txtNoOfDays.Text) && !isNumeric(txtLocation.Text) && isNumeric(txtNoOfModels.Text) && !isNumeric(txtComments.Text))
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("FAILED! \nPlease enter valid information.");
            }
        }

        public string CosName
        {
            get { return txtCosName.Text; }
        }
        public int Date
        {
            get { return int.Parse(txtDate.Text); }
        }
        public int NoOfDays
        {
            get { return int.Parse(txtNoOfDays.Text); }
        }
        public string Location
        {
            get { return txtLocation.Text; }
        }
        public int NoOfModels
        {
            get { return int.Parse(txtNoOfModels.Text); }
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

