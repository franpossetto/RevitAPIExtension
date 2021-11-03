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
        private string _revitapiver;
        private bool _nugetpacksel;
        public VersionWizardWPF()
        {
            InitializeComponent();
            RevitAPIVerCB.Items.Add("2017");
            RevitAPIVerCB.Items.Add("2018");
            RevitAPIVerCB.Items.Add("2019");
            RevitAPIVerCB.Items.Add("2020");
            RevitAPIVerCB.Items.Add("2021");
        }

        public string revitAPIVersion
        {
            get
            {
                return _revitapiver;
            }
            set
            {
                _revitapiver = value;
            }
        }

        public bool IsNugetSel
        {
            get
            {
                return _nugetpacksel;
            }
            set
            {
                _nugetpacksel = value;
            }
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            _revitapiver = RevitAPIVerCB.Text;
            this.Close();
        }

        private void UseLatest_Click(object sender, RoutedEventArgs e)
        {
            _revitapiver = "2021";
            if (!IsNugetSel)
            {
                _nugetpacksel = true;
            }
            this.Close();
        }

        private void revitAvailableVersions_Checked(object sender, RoutedEventArgs e)
        {
            Path path = new Path();
            List<string> versions = path.unavalaibleVersions();
            {
                for (int i = 0; i < versions.Count; i++)
                {
                    RevitAPIVerCB.Items.Remove(versions[i]);

                }
            }
        }

        private void revitAvailableVersions_UnChecked(object sender, RoutedEventArgs e)
        {
            RevitAPIVerCB.Items.Clear();
            RevitAPIVerCB.Items.Add("2017");
            RevitAPIVerCB.Items.Add("2018");
            RevitAPIVerCB.Items.Add("2019");
            RevitAPIVerCB.Items.Add("2020");
            RevitAPIVerCB.Items.Add("2021");
        }

        private void nuGet_RatioButton_Checked(object sender, RoutedEventArgs e)
        {
            _nugetpacksel = true;
        }

        private void localRef_RatioButton_Checked(object sender, RoutedEventArgs e)
        {
            _nugetpacksel = false;
        }
    }
}
