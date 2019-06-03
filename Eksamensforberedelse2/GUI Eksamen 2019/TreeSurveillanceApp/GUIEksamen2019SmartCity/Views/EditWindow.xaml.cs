using System.Windows;
using GUIEksamen2019SmartCity.Models;
using System.Text.RegularExpressions;

namespace GUIEksamen2019SmartCity.Views
{
    /// <summary>
    /// Interaction logic for EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        Location itemToEdit;

        public EditWindow(Location item)
        {
            itemToEdit = item;
            InitializeComponent();

            tbName.Text = item.Name;
            tbStreet.Text = item.Street;
            tbStreetNum.Text = item.StreetNum.ToString();
            tbZipCode.Text = item.ZipCode.ToString();
            tbCity.Text = item.City;
            tbListOfTrees.Text = item.TreeMonitorList;
        }

        #region Button pushes

        private void Done_Click(object sender, RoutedEventArgs e)
        {
            if (!isNumeric(tbName.Text) && !isNumeric(tbStreet.Text) && isNumeric(tbStreetNum.Text) && isNumeric(tbZipCode.Text) && !isNumeric(tbCity.Text) && !isNumeric(tbListOfTrees.Text))
            {
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Something went wrong.\nPlease check that all values\nhave been inserted correctly.");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Location edit canceled.");
        }

        #endregion

        #region Properties

        public string Name
        {
            get { return tbName.Text; }
        }
        public string Street
        {
            get { return tbStreet.Text; }
        }
        public int StreetNum
        {
            get { return int.Parse(tbStreetNum.Text); }
        }
        public int ZipCode
        {
            get { return int.Parse(tbZipCode.Text); }
        }
        public string City
        {
            get { return tbCity.Text; }
        }
        public string TreeMonitorList
        {
            get { return tbListOfTrees.Text; }
        }

        #endregion

        private bool isNumeric(string s)
        {
            return Regex.IsMatch(s, @"^\d+$");
        }
    }
}
