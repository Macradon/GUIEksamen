using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace DeepModelBureau
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Assignment> ListAssignment = new ObservableCollection<Assignment>();

        Assignment asg = new Assignment("d","d");

        public MainWindow()
        {
            InitializeComponent();
            taskGrid.SelectionChanged += new SelectionChangedEventHandler(TaskSelecter);
            taskGrid.SelectionChanged += new SelectionChangedEventHandler(ModelSelecter);

            AsgGrid.ItemsSource = ListAssignment;
        }

        //Metode som sætter valgte item til Model i en assignment.
        private void ModelSelecter(object sender, EventArgs e)
        {
            if (ModelGrid.SelectedItem != null)
            {
                var selectedModel = (Model)ModelGrid.SelectedItem;
                asg.ModelName = selectedModel.Name;
            }
        }

        //Metode som sætter valgte item til Task i en assignment.
        private void TaskSelecter(object sender, EventArgs e)
        {
            if (taskGrid.SelectedItem != null)
            {
                var selectedTask = (Task)taskGrid.SelectedItem;
                asg.TaskName = selectedTask.CosName; 
            }
        }

        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            Window win = new Window();
            win.Title = "Added task to model";
            win.Show();
            ListAssignment.Add(asg);
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {

            ListAssignment.Remove(asg);

        }

        private void Menu_NewItem_Click(object sender, RoutedEventArgs e)
        {
            //create Window to prompt user for data.
            var createItemWindow = new AddProdWindow();
            //if 'OK' pressed
            if (createItemWindow.ShowDialog() == true)
            {
                //Get data from create-window.
                string Name = createItemWindow.Name;
                int PhoneNo = createItemWindow.PhoneNo;
                string Address = createItemWindow.Address;
                int ModelHeight = createItemWindow.ModelHeight;
                int ModelWeight = createItemWindow.ModelWeight;
                string HairColor = createItemWindow.HairColor;
                string Comments = createItemWindow.Comments; 


                Model item = new Model(Name, PhoneNo, Address, ModelHeight, ModelWeight, HairColor, Comments);

                ModelList items = (ModelList)Resources["ModelList"];
                items.NewItemCommand.Execute(item);
            }
        }

        private void Menu_NewTask_Click(object sender, RoutedEventArgs e)
        {
            //create Window to prompt user for data.
            var createItemWindow = new AddTaskWindow();

            //if 'OK' pressed
            if (createItemWindow.ShowDialog() == true)
            {
                //Get data from create-window.
                string CosName = createItemWindow.CosName;
                int Date = createItemWindow.Date;
                int NoOfDays = createItemWindow.NoOfDays;
                string Location = createItemWindow.Location;
                int NoOfModels = createItemWindow.NoOfModels;
                string Comments = createItemWindow.Comments;


                Task item = new Task(CosName, Date, NoOfDays, Location, NoOfModels, Comments);

                //Get item ModelList from XAML and add new item.
                TaskList items = (TaskList)Resources["TaskList"];
                items.NewItemCommand.Execute(item);
            }
        }

        private void Menu_EditItem_Click(object sender, RoutedEventArgs e)
        {
            ModelList items = (ModelList)Resources["ModelList"];

            int index = items.CurrentIndex;

            try
            {
                //create Window to prompt user for data.
                var editItemWindow = new EditProdWindow(items[index]);

                //if 'OK' pressed
                if (editItemWindow.ShowDialog() == true)
                {
                    //Get data from create-window.
                    string Name = editItemWindow.Name;
                    int PhoneNo = editItemWindow.PhoneNo;
                    string Address = editItemWindow.Address;
                    int ModelHeight = editItemWindow.ModelHeight;
                    int ModelWeight = editItemWindow.ModelWeight;
                    string HairColor = editItemWindow.HairColor;
                    string Comments = editItemWindow.Comments;

                    //Create new item with data.
                    Model item = new Model(Name, PhoneNo, Address, ModelHeight, ModelWeight, HairColor, Comments);

                    //Replace item with edited item.
                    items[index] = item;
                }
            }
            catch
            {
                MessageBox.Show("Please choose an item from the List to edit.");
            }

        }

        private void Menu_EditTask_Click(object sender, RoutedEventArgs e)
        {
            TaskList items = (TaskList)Resources["TaskList"];

            int index = items.CurrentIndex;

            try
            {
                //create Window to prompt user for data.
                var editItemWindow = new EditTaskWindow(items[index]);

                //if 'OK' pressed
                if (editItemWindow.ShowDialog() == true)
                {
                    //Get data from create-window.
                    string CosName = editItemWindow.CosName;
                    int Date = editItemWindow.Date;
                    int NoOfDays = editItemWindow.NoOfDays;
                    string Location = editItemWindow.Location;
                    int NoOfModels = editItemWindow.NoOfModels;
                    string Comments = editItemWindow.Comments;

                    //Create new item with data.
                    Task item = new Task(CosName, Date, NoOfDays, Location, NoOfModels, Comments);

                    //Replace item with edited item.
                    items[index] = item;
                }
            }
            catch
            {
                MessageBox.Show("Please choose an item from the List to edit.");
            }

        }

        private void _menuItemSaveModelListAs(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string filename = saveFileDialog.FileName.ToString();
                ModelList ModelList = getItemModelList();
                ModelList.filename = filename;

            }

        }

        private void _menuItemOpenModelList(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filename = openFileDialog.FileName.ToString();
                ModelList ModelList = getItemModelList();
                ModelList.filename = filename;
            }
        }

        private void _menuItemSaveTaskListAs(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                string filename = saveFileDialog.FileName.ToString();
                TaskList TaskList = getItemTaskList();
                TaskList.filename = filename;

            }
        }

        private void _menuItemOpenTaskList(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string Filename = openFileDialog.FileName.ToString();
                TaskList TaskList = getItemTaskList();
                TaskList.filename = Filename;
            }
        }

        ModelList getItemModelList()
        {
            return (ModelList)Resources["ModelList"];
        }
        TaskList getItemTaskList()
        {
            return (TaskList)Resources["TaskList"];
        }

        Task getCurrentTask()
        {
            return (Task)Resources["currentTask"];
        }

        private void ExitClicked(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


    }
}
