using AlarmWorkflow.Shared.Core;
using AlarmWorkflow.Tools.Alarm.Model;
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
using System.Windows.Shapes;

namespace AlarmWorkflow.Tools.Alarm
{
    /// <summary>
    /// Interaction logic for ResourceDialog.xaml
    /// </summary>
    public partial class ResourceDialog : Window
    {
        public ResourceDialog()
        {
            InitializeComponent();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtVehicle.SelectAll();
            txtVehicle.Focus();
        }

        public Vehicle Resource
        {
            get
            {
                return new Vehicle(txtVehicle.Text, txtEquipment.Text);
            }
        }

        public static Vehicle Prompt()
        {
            ResourceDialog dialog = new ResourceDialog();
            dialog.ShowDialog();
            if (dialog.DialogResult == true)
                return dialog.Resource;
            return null;
        }
    }
}
