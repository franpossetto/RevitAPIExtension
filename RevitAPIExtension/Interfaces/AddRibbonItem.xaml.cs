using Microsoft.VisualStudio.PlatformUI;
using RevitAPIExtension.Models;
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

namespace RevitAPIExtension.Interfaces
{
    /// <summary>
    /// Interaction logic for AddRibbonItem.xaml
    /// </summary>
    public partial class AddRibbonItem : Window, DialogWindow
    {
        private bool IsCancel = false;
        private readonly UIDefaultData _defaultData;
        private readonly CodeGenData _data;
        public AddRibbonItem(UIDefaultData defaultData)
        {
            InitializeComponent();
            _data = defaultData.Default;
            _defaultData = defaultData;
            cbPanels.ItemsSource = defaultData.Panels.Select(p => p.Name);
            cbPanels.SelectedIndex = 0;
            cbType.SelectedIndex = 0;
            tbText.Text = _data.Text;
        }

        public new CodeGenData ShowModal()
        {
            base.ShowModal();
            return IsCancel ? null : _data;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _data.Text = tbText.Text;
            _data.Panel = cbPanels.SelectedItem as String;
            var pullSelected = cbPullDows.SelectedItem;
            _data.Parent = pullSelected as ButtonReference;
            _data.Type = (ButtonType)(cbType.SelectedIndex as int? ?? 0);
            base.Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IsCancel = true;
            base.Hide();
        }

        private void cbPanels_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbPullDows.IsEnabled = true;
            string panelName = cbPanels.SelectedItem as String;
            var panel = _defaultData.Panels.Find(p => p.Name == panelName);
            var pullDowns = panel.PullDowns.Select(p =>
            {
                var uniqueName = p.Name.Value;
                var name = uniqueName.Split('.')[0];
                return new ButtonReference()
                {
                    Name = name,
                    UniqueName = uniqueName
                };
            });
            if (pullDowns.Count() > 0)
                cbPullDows.ItemsSource = pullDowns;
            else
            {
                cbPullDows.ItemsSource = null;
                cbPullDows.IsEnabled = false;
            }
        }

    }
}


// op1 pushbutton       is not            parent: pulldownbutton que esten en el panel seleccionado.
// op2 pulldown         is not            parent: nada

// si no se selecciona parent ->se crea push/pulldown button data.

// op3 pushbutton       is stacked        parent: x-buttondata que esten en el panel seleccionado.
// op4 pulldown         is stacked        parent: x-buttondata que esten en el panel seleccionado.