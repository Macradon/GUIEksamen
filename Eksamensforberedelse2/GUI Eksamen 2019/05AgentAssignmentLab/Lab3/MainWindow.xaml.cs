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
        public MainWindow()
        {
            InitializeComponent();
            btnForward.Click += new RoutedEventHandler(btnForward_Click);
            btnBack.Click += new RoutedEventHandler(btnBack_Click);
            btnAddNew.Click += new RoutedEventHandler(btnAddNew_Click);
        }

        void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            Agents agents = (Agents)this.FindResource("agents");
            agents.Add(new Agent());
            lbxAgents.SelectedIndex = lbxAgents.Items.Count - 1;
            tbxId.Focus();
        }

        void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (lbxAgents.SelectedIndex > 0)
                lbxAgents.SelectedIndex = --lbxAgents.SelectedIndex;
        }

        void btnForward_Click(object sender, RoutedEventArgs e)
        {
            if (lbxAgents.SelectedIndex < lbxAgents.Items.Count - 1)
                lbxAgents .SelectedIndex = ++lbxAgents.SelectedIndex;
        }
    }
}
