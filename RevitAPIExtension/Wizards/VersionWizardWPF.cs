using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RevitAPIExtension.Wizards
{
    /// <summary>
    /// Interaction logic for VersionWizardWPF.xaml
    /// </summary>
    public partial class VersionWizardWPF : Window
    {
        private static string revitapiver;
        public VersionWizardWPF()
        {
            InitializeComponent();
        }

        public string revitAPIVersion
        {
            get
            {
                return RevitAPIVerCB.Text;
            }
            set
            {
                revitapiver = value;
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            revitapiver = RevitAPIVerCB.Text;
            this.Close();
        }

        private void UseLatest_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
