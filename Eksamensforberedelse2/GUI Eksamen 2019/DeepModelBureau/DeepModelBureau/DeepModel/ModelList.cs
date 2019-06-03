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
        public class ModelList : ObservableCollection<Model>, INotifyPropertyChanged
        {
            public List<Model> Items = new List<Model>();

            public string filename;

            public ModelList()
            {
                if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
                {
                    Add(new Model("Gyt", 10, "Pølse", 101, 61, "black", "Stop"  ));
                    //Add(new Model(002, "Hotdog", 20));
                    //Add(new Model(003, "Fransk Hotdog", 15));
                    //Add(new Model(004, "Cocio", 15));
                    //Add(new Model(005, "Sodavand", 15));
                    //Add(new Model(006, "100g rent guld", 10000));
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

            Model currentItem = null;

            public Model CurrentItem
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
                    return _newItemCommand ?? (_newItemCommand = new RelayCommand<Model>(AddItemExecute));
                }
            }

            private void AddItemExecute(Model item)
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
                    return _editItemCommand ?? (_editItemCommand = new RelayCommand<Model>(EditItemExecute));
                }
            }

            private void EditItemExecute(Model item)
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

            #region OpenModelListCommand
            ICommand _openModelListCommand;
            public ICommand OpenModelListCommand
            {
                get { return _openModelListCommand ?? (_openModelListCommand = new RelayCommand<string>(OpenModelListCommand_Execute)); }
            }

            private void OpenModelListCommand_Execute(string argFilename)
            {
                if (argFilename == "")
                {

                    MessageBox.Show("You must enter a file name in the File Name textbox!", "Unable to save file",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    ModelList tempCatalogue = new ModelList();
                    tempCatalogue.Clear();

                    // Create an instance of the XmlSerializer class and specify the type of object to deserialize.
                    XmlSerializer serializer = new XmlSerializer(typeof(ModelList));
                    try
                    {
                        TextReader reader = new StreamReader(filename);
                        // Deserialize all items.
                        tempCatalogue = (ModelList)serializer.Deserialize(reader);
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

            #region SaveModelListAsCommand
            ICommand _saveModelListAsCommand;
            public ICommand SaveModelListAsCommand
            {
                get { return _saveModelListAsCommand ?? (_saveModelListAsCommand = new RelayCommand<string>(SaveAsCommand_Execute)); }
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

            #region NewModelListCommand
            ICommand _newModelListCommand;
            public ICommand NewModelListCommand
            {
                get
                {
                    return _newModelListCommand ?? (_newModelListCommand = new RelayCommand(NewModelListExecute, NewModelList_CanExecute));
                }

            }

            private void NewModelListExecute()
            {
                Clear();

                NotifyPropertyChanged("Count");
                NotifyPropertyChanged("totalPrice");
                CurrentIndex = Count - 1;
            }

            private bool NewModelList_CanExecute()
            {
                return true;
            }
            #endregion

            public void SaveFileCommand_Execute()
            {
                // https://docs.microsoft.com/en-us/dotnet/api/system.xml.serialization.xmlserializer?view=netframework-4.7.2
                // Create an instance of the XmlSerializer class and specify the type of object to serialize.
                XmlSerializer serializer = new XmlSerializer(typeof(ModelList));
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
