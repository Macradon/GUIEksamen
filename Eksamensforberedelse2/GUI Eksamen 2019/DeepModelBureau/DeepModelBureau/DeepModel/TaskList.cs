using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using MvvmFoundation.Wpf;

namespace DeepModelBureau
{
    public class TaskList : ObservableCollection<Task>, INotifyPropertyChanged
    {
        public List<Task> Items = new List<Task>();

        public string filename;

        public TaskList()
        {
            //if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            //{
            //    //Add(new Task(001, "hotdog", 10));

            //}
        }

        #region Properties

        int currentIndex = -1;

        public int CurrentIndex
        {
            get { return currentIndex; }
            set
            {
                if (currentIndex != value)
                {
                    currentIndex = value;
                    NotifyPropertyChanged();
                }
            }
        }

        Task currentItem = null;

        public Task CurrentItem
        {
            get { return currentItem; }
            set
            {
                if (currentItem != value)
                {
                    currentItem = value;
                    NotifyPropertyChanged();
                }
            }
        }

        #endregion

        #region Commands

        #region NewItemCommand
        ICommand _newItemCommand;
        public ICommand NewItemCommand
        {
            get
            {
                return _newItemCommand ?? (_newItemCommand = new RelayCommand<Task>(AddItemExecute));
            }
        }

        private void AddItemExecute(Task item)
        {
            if (item == null)
            {
                //Add(new Task(0, "N/A", 0));
            }
            else
            {
                Add(item);
            }
            NotifyPropertyChanged("Count");
            CurrentIndex = Count - 1;
        }
        #endregion

        #region DeleteItemCommand
        ICommand _deleteItemCommand;
        public ICommand DeleteItemCommand
        {
            get { return _deleteItemCommand ?? (_deleteItemCommand = new RelayCommand(DeleteItem, DeleteItem_CanExecute)); }
        }

        private void DeleteItem()
        {
            RemoveAt(CurrentIndex);
            NotifyPropertyChanged("Count");
        }

        private bool DeleteItem_CanExecute()
        {
            if (Count > 0 && CurrentIndex >= 0)
                return true;
            else
                return false;
        }
        #endregion

        #region EditItemCommand
        ICommand _editItemCommand;
        public ICommand EditItemCommand
        {
            get
            {
                return _editItemCommand ?? (_editItemCommand = new RelayCommand<Task>(EditItemExecute));
            }
        }

        private void EditItemExecute(Task item)
        {
            if (item == null)
            {
                //Add(new Task(0, "N/A", 0));
            }
            else
            {
                Add(item);
            }
            NotifyPropertyChanged("Count");
            CurrentIndex = Count - 1;
        }
        #endregion

        #region OpenTaskListCommand
        ICommand _openTaskListCommand;
        public ICommand OpenTaskListCommand
        {
            get { return _openTaskListCommand ?? (_openTaskListCommand = new RelayCommand<string>(OpenTaskListCommand_Execute)); }
        }

        private void OpenTaskListCommand_Execute(string argFilename)
        {
            if (argFilename == "")
            {

                MessageBox.Show("You must enter a file name in the File Name textbox!", "Unable to save file",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                TaskList tempCatalogue = new TaskList();
                tempCatalogue.Clear();

                // Create an instance of the XmlSerializer class and specify the type of object to deserialize.
                XmlSerializer serializer = new XmlSerializer(typeof(TaskList));
                try
                {
                    TextReader reader = new StreamReader(filename);
                    // Deserialize all items.
                    tempCatalogue = (TaskList)serializer.Deserialize(reader);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // We have to insert the items in the existing collection. 
                Clear();
                foreach (var Item in tempCatalogue)
                    Add(Item);

                NotifyPropertyChanged("Count");
            }
        }
        #endregion

        #region SaveTaskListAsCommand
        ICommand _saveTaskListAsCommand;
        public ICommand SaveTaskListAsCommand
        {
            get { return _saveTaskListAsCommand ?? (_saveTaskListAsCommand = new RelayCommand<string>(SaveAsCommand_Execute)); }
        }

        private void SaveAsCommand_Execute(string argFilename)
        {
            if (argFilename == "")
            {
                MessageBox.Show("You must enter a file name in the File Name textbox!", "Unable to save file",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                //filename = argFilename;
                SaveFileCommand_Execute();
            }
        }

        #endregion

        #region NewTaskListCommand
        ICommand _newTaskListCommand;
        public ICommand NewTaskListCommand
        {
            get
            {
                return _newTaskListCommand ?? (_newTaskListCommand = new RelayCommand(NewTaskListExecute, NewTaskList_CanExecute));
            }

        }

        private void NewTaskListExecute()
        {
            Clear();

            NotifyPropertyChanged("Count");
            NotifyPropertyChanged("totalPrice");
            CurrentIndex = Count - 1;
        }

        private bool NewTaskList_CanExecute()
        {
            return true;
        }
        #endregion

        public void SaveFileCommand_Execute()
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer?view=netframework-4.7.2
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(TaskList));
            TextWriter writer = new StreamWriter(filename);
            // Serialize all items.
            serializer.Serialize(writer, this);
            writer.Close();
        }

        private bool SaveFileCommand_CanExecute()
        {
            return (filename != "") && (Count > 0);
        }


        #endregion

        #region INotifyPropertyChanged implementation

        public new event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }

}

