using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RevitAPIExtension.Wizards
{
    public partial class VersionWizardForm : Form
    {
        private static string revitapiver;
        public VersionWizardForm()
        {
            InitializeComponent();
        }

        public string revitAPIVersion
        {
            get
            {
                return RevitAPIVerCB.SelectedItem.ToString();
            }
            set
            {
                revitapiver = value;
            }
        }

        private void VersionWizardForm_Load(object sender, EventArgs e)
        {
            //Loading the Revit API versions in a combo box 
            RevitAPIVerCB.Items.Add("2017");
            RevitAPIVerCB.Items.Add("2018");
            RevitAPIVerCB.Items.Add("2019");
            RevitAPIVerCB.Items.Add("2020");
            RevitAPIVerCB.Items.Add("2021");
        }

        private void RevitAPIVerCB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void accept_Click(object sender, EventArgs e)
        {
            revitapiver = RevitAPIVerCB.SelectedItem.ToString();
            this.Close();
        }
    }
}
