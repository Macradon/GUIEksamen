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

namespace AgentAssignment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Agents agents = new Agents();

        public MainWindow()
        {
            InitializeComponent();
            agents.Add(new Agent() { ID = "001", CodeName = "Nina", Speciality = "Assassination", Assignment = "UpperVolta" });
            agents.Add(new Agent("007", "James Bond", "Martinis", "North Korea"));
            agentGrid.DataContext = agents;
        }
    }
}
