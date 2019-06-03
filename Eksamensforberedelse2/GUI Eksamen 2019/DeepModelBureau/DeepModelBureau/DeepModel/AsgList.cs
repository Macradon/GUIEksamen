using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using MvvmFoundation.Wpf;

namespace DeepModelBureau
{
    public class AsgList : ObservableCollection<Assignment>, INotifyPropertyChanged
    {

        public string filename;

        public AsgList()
        {
            if ((bool) (DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                Add(new Assignment("Kim", "Poop"));
            }
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

        Assignment currentItem = null;

        public Assignment CurrentItem
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
                return _newItemCommand ?? (_newItemCommand = new RelayCommand<Assignment>(AddItemExecute));
            }
        }

        private void AddItemExecute(Assignment item)
        {
            if (item == null)
            {
                //Add(new Assignment(0, "N/A", 0));
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
                return _editItemCommand ?? (_editItemCommand = new RelayCommand<Assignment>(EditItemExecute));
            }
        }

        private void EditItemExecute(Assignment item)
        {
            if (item == null)
            {
                //Add(new Assignment(0, "N/A", 0));
            }
            else
            {
                Add(item);
            }
            NotifyPropertyChanged("Count");
            CurrentIndex = Count - 1;
        }
        #endregion

        #region OpenAsgListCommand
        ICommand _openAsgListCommand;
        public ICommand OpenAsgListCommand
        {
            get { return _openAsgListCommand ?? (_openAsgListCommand = new RelayCommand<string>(OpenAsgListCommand_Execute)); }
        }

        private void OpenAsgListCommand_Execute(string argFilename)
        {
            if (argFilename == "")
            {

                MessageBox.Show("You must enter a file name in the File Name textbox!", "Unable to save file",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                AsgList tempCatalogue = new AsgList();
                tempCatalogue.Clear();

                // Create an instance of the XmlSerializer class and specify the type of object to deserialize.
                XmlSerializer serializer = new XmlSerializer(typeof(AsgList));
                try
                {
                    TextReader reader = new StreamReader(filename);
                    // Deserialize all items.
                    tempCatalogue = (AsgList)serializer.Deserialize(reader);
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

        #region SaveAsgListAsCommand
        ICommand _saveAsgListAsCommand;
        public ICommand SaveAsgListAsCommand
        {
            get { return _saveAsgListAsCommand ?? (_saveAsgListAsCommand = new RelayCommand<string>(SaveAsCommand_Execute)); }
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

        #region NewAsgListCommand
        ICommand _newAsgListCommand;
        public ICommand NewAsgListCommand
        {
            get
            {
                return _newAsgListCommand ?? (_newAsgListCommand = new RelayCommand(NewAsgListExecute, NewAsgList_CanExecute));
            }

        }

        private void NewAsgListExecute()
        {
            Clear();

            NotifyPropertyChanged("Count");
            NotifyPropertyChanged("totalPrice");
            CurrentIndex = Count - 1;
        }

        private bool NewAsgList_CanExecute()
        {
            return true;
        }
        #endregion

        public void SaveFileCommand_Execute()
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer?view=netframework-4.7.2
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(AsgList));
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
