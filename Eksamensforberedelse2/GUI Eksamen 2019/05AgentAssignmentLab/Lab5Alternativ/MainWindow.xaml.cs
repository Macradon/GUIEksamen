using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using System.IO;

namespace AgentAssignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filename = "";
        Agents agents = new Agents();

        public MainWindow()
        {
            InitializeComponent();
            //agents.Add(new Agent() { ID = "001", CodeName = "Nina", Speciality = "Assassination", Assignment = "UpperVolta" });
            //agents.Add(new Agent("007", "James Bond", "Martinis", "North Korea"));
            DataContext = agents;
        }

        #region Command handlers
        private void CloseCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void SaveAsCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (tbxFileName.Text != "")
            {
                filename = tbxFileName.Text;
                SaveFileCommand_Executed(null, null);
            }
            else
                MessageBox.Show("You must enter a file name in the File Name textbox!", "Unable to save file", 
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);

        }

        private void SaveFileCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(Agents));
            TextWriter writer = new StreamWriter(filename);
            // Serialize all the agents.
            serializer.Serialize(writer, agents);
            writer.Close();
        }

        private void SaveFileCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (filename != "") && (agents.Count > 0);
        }


        private void NewFileCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Any unsaved data will be lost. Are you sure you want to initiate a new file?", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                agents.Clear();
                filename = "";
            }
        }

        private void OpenFileCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            filename = tbxFileName.Text;
            Agents tempAgents = new Agents();

            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(Agents));
            try
            {
                TextReader reader = new StreamReader(filename);
                // Deserialize all the agents.
                tempAgents = (Agents)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // We have to insert the agents in the existing collection. If we just assign tempAgents to agents then the bindings to agents will brake!
            agents.Clear();
            foreach (var agent in tempAgents)
                agents.Add(agent);
        }

        private void OpenFileCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = tbxFileName.Text != "";
        }


        #endregion
    }
}
