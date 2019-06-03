using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using MvvmFoundation.Wpf;
using System.IO;
using System.Xml.Serialization;

namespace GUIEksamen2019SmartCity.Models
{
    public class LocationList : ObservableCollection<Location>, INotifyPropertyChanged
    {
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

        public List<Location> Items = new List<Location>();

        public string filename;

        public LocationList()
        {
            //Dummy text while designing
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
            {
                Add(new Location( "Hjem",
                                        "Fjaeldevaenget", 
                                        10, 
                                        8210,
                                        "Aarhus V",
                                        "5Oak,2Birch"));
                Add(new Location( "GamleHjem",
                                        "Primulavej",
                                        76,
                                        9890,
                                        "Hjeorring",
                                        "1Apple,1Pear,3 Pine,1Plum,3Acacia"));
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

        Location currentItem = null;

        public Location CurrentItem
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
                return _newItemCommand ?? (_newItemCommand = new RelayCommand<Location>(AddItemExecute));
            }
        }

        private void AddItemExecute(Location item)
        {
            if (item == null)
            {
                //Add(new Model(0, "N/A", 0));
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
                return _editItemCommand ?? (_editItemCommand = new RelayCommand<Location>(EditItemExecute));
            }
        }

        private void EditItemExecute(Location item)
        {
            if (item == null)
            {
                //Add(new Model(0, "N/A", 0));
            }
            else
            {
                Add(item);
            }
            NotifyPropertyChanged("Count");
            CurrentIndex = Count - 1;
        }
        #endregion

        #region OpenLocationListCommand
        ICommand _openLocationListCommand;
        public ICommand OpenLocationListCommand
        {
            get { return _openLocationListCommand ?? (_openLocationListCommand = new RelayCommand<string>(OpenLocationListCommand_Execute)); }
        }

        private void OpenLocationListCommand_Execute(string argFilename)
        {
            if (argFilename == "")
            {

                MessageBox.Show("You must enter a file name in the File Name textbox!", "Unable to find file",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                LocationList tempCatalogue = new LocationList();
                tempCatalogue.Clear();

                // Create an instance of the XmlSerializer class and specify the type of object to deserialize.
                XmlSerializer serializer = new XmlSerializer(typeof(LocationList));
                try
                {
                    TextReader reader = new StreamReader(filename);
                    // Deserialize all items.
                    tempCatalogue = (LocationList)serializer.Deserialize(reader);
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

        #region SaveLocationListAsCommand
        ICommand _saveLocationListAsCommand;
        public ICommand SaveLocationListAsCommand
        {
            get { return _saveLocationListAsCommand ?? (_saveLocationListAsCommand = new RelayCommand<string>(SaveAsCommand_Execute)); }
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

        #region NewLocationListCommand
        ICommand _newLocationListCommand;
        public ICommand NewLocationListCommand
        {
            get
            {
                return _newLocationListCommand ?? (_newLocationListCommand = new RelayCommand(NewLocationListExecute, NewLocationList_CanExecute));
            }

        }

        private void NewLocationListExecute()
        {
            Clear();

            NotifyPropertyChanged("Count");
            NotifyPropertyChanged("totalPrice");
            CurrentIndex = Count - 1;
        }

        private bool NewLocationList_CanExecute()
        {
            return true;
        }
        #endregion

        public void SaveFileCommand_Execute()
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer?view=netframework-4.7.2
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(LocationList));
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
    }
}
