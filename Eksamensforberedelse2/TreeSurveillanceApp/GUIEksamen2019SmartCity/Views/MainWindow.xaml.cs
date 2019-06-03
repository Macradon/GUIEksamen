using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;
using GUIEksamen2019SmartCity.Models;
using GUIEksamen2019SmartCity.Views;
using Application = System.Windows.Application;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace GUIEksamen2019SmartCity
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Menu actions

        private void _menuItemOpenLocationList(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName.ToString();
                LocationList LocationList = getItemLocationList();
                LocationList.filename = filename;
            }
        }

        private void _menuItemSaveLocationListAs(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string filename = saveFileDialog.FileName.ToString();
                LocationList LocationList = getItemLocationList();
                LocationList.filename = filename;

            }
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Menu_NewItem_Click(object sender, RoutedEventArgs e)
        {
            //create Window to prompt user for data.
            var createItemWindow = new AddWindow();
            //if 'OK' pressed
            if (createItemWindow.ShowDialog() == true)
            {
                //Get data from create-window.
                string Name = createItemWindow.Name;
                string Street = createItemWindow.Street;
                int StreetNum = createItemWindow.StreetNum;
                int ZipCode = createItemWindow.ZipCode;
                string City = createItemWindow.City;
                string TreeMonitorList = createItemWindow.TreeMonitorList;


                Location item = new Location(Name, Street, StreetNum, ZipCode, City, TreeMonitorList);

                LocationList items = (LocationList)Resources["LocationList"];
                items.NewItemCommand.Execute(item);
            }
        }

        private void Menu_EditItem_Click(object sender, RoutedEventArgs e)
        {
            LocationList items = (LocationList)Resources["LocationList"];

            int index = items.CurrentIndex;

            try
            {
                //create Window to prompt user for data.
                var editItemWindow = new EditWindow(items[index]);

                //if 'OK' pressed
                if (editItemWindow.ShowDialog() == true)
                {
                    //Get data from create-window.
                    string Name = editItemWindow.Name;
                    string Street = editItemWindow.Street;
                    int StreetNum = editItemWindow.StreetNum;
                    int ZipCode = editItemWindow.ZipCode;
                    string City = editItemWindow.City;
                    string TreeMonitorList = editItemWindow.TreeMonitorList;

                    //Create new item with data.
                    Location item = new Location(Name, Street, StreetNum, ZipCode, City, TreeMonitorList);

                    //Replace item with edited item.
                    items[index] = item;
                }
            }
            catch
            {
                MessageBox.Show("Please choose an item from the List to edit.");
            }
        }

        #endregion

        LocationList getItemLocationList()
        {
            return (LocationList)Resources["LocationList"];
        }

        public void txtName_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox textBoxName = tbSearch;
            string filterText = textBoxName.Text;
            ICollectionView cv = CollectionViewSource.GetDefaultView(LocationGrid.ItemsSource);

            if (!string.IsNullOrEmpty(filterText))
            {
                cv.Filter = o => {
                    //change to get data row value
                    Location p = o as Location;
                    return (p.Name.ToUpper().StartsWith(filterText.ToUpper()));
                    //end change to get data row value 
                };
            }
        }
    }
}
