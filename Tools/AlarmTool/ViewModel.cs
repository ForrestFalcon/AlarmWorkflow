// This file is part of AlarmWorkflow.
// 
// AlarmWorkflow is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// AlarmWorkflow is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with AlarmWorkflow.  If not, see <http://www.gnu.org/licenses/>.


using System;
using System.Windows;
using System.Windows.Input;
using AlarmWorkflow.Windows.UIContracts.ViewModels;
using AlarmWorkflow.Shared.Core;
using Microsoft.Win32;
using AlarmWorkflow.Backend.ServiceContracts.Communication;
using AlarmWorkflow.BackendService.SettingsContracts;
using AlarmWorkflow.Shared.Settings;
using System.ServiceModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AlarmWorkflow.Tools.Alarm.Helper;
using AlarmWorkflow.Tools.Alarm.Model;
using System.Collections.ObjectModel;

namespace AlarmWorkflow.Tools.Alarm
{
    internal class ViewModel : ViewModelBase, ISettingsServiceCallback
    {
        #region Fields
        
        private WrappedService<ISettingsService> _settingsService;
        private Operation _currentOperation;
        private string _server;

        #endregion

        #region Properties

        public int DefaultPort => _settingsService.Instance.GetSetting(Constants.OutputPortKey).GetValue<int>();

        public ObservableCollection<Vehicle> VehicleCollection { get; set; }

        /// <summary>
        /// Gets or sets the current <see cref="Operation"/>.
        /// </summary>
        public Operation CurrentOperation
        {
            get
            {
                return _currentOperation;
            }
            set
            {
                _currentOperation = value;
                OnPropertyChanged("CurrentOperation");
            }
        }

        /// <summary>
        /// Gets or sets the remote Server
        /// </summary>
        public string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
                OnPropertyChanged("Server");
            }
        }

        #endregion

        #region Commands

        #region MenuCommands

        /// <summary>
        /// The command assigned to the new button in the menu.
        /// </summary>
        public ICommand MenuNewCommand { get; set; }

        /// <summary>
        /// The command assigned to the open button in the menu.
        /// </summary>
        public ICommand MenuOpenCommand { get; set; }

        /// <summary>
        /// The command assigned to the save button in the menu.
        /// </summary>
        public ICommand MenuSaveCommand { get; set; }

        /// <summary>
        /// The command assigned to the exit button in the menu.
        /// </summary>
        public ICommand MenuExitCommand { get; set; }


        private void MenuNewCommand_Execute(object param)
        {
            CurrentOperation = new Operation();
        }

        private void MenuSaveCommand_Execute(object param)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "AlarmWorkflow JSON file (*.json)|*.json";
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (saveFileDialog.ShowDialog() == true)
            {
                if (!OperationHelper.Serialize(createOperation(), saveFileDialog.FileName))
                {
                    MessageBox.Show("Error saving file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void MenuOpenCommand_Execute(object param)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "AlarmWorkflow JSON (*.json)|*.json";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                CurrentOperation = OperationHelper.Deserialize(openFileDialog.FileName);
                if(CurrentOperation == null)
                {
                    MessageBox.Show("Error opening file", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    CurrentOperation = new Operation();
                }

                VehicleCollection.Clear();
                foreach (OperationResource resource in CurrentOperation.Resources)
                {
                    VehicleCollection.Add(Vehicle.FromOperationResource(resource));
                }
            }
        }

        private void MenuExitCommand_Execute(object param)
        {
            Application.Current.Shutdown();
        }

        #endregion

        #region MainCommands

        /// <summary>
        /// The command assigned to the send button.
        /// </summary>
        public ICommand SendCommand { get; set; }

        /// <summary>
        /// The command assigned to the create pdf button.
        /// </summary>
        public ICommand CreatePDFCommand { get; set; }

        /// <summary>
        /// The command assigned to the add vehicle button.
        /// </summary>
        public ICommand AddResourceCommand { get; set; }

        private void SendCommand_Execute(object param)
        {
            if(String.IsNullOrEmpty(CurrentOperation.Comment)
                || String.IsNullOrEmpty(CurrentOperation.Keywords.EmergencyKeyword)
                || String.IsNullOrEmpty(CurrentOperation.Keywords.Keyword))
            {
                MessageBox.Show("Invalid arguments. No EmergencyKeyword, Keyword or comment.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    Network.SendOperation(Server, createOperation());
                    MessageBox.Show("Alarm wurde gesendet!", "Send", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void CreatePDFCommand_Execute(object param)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == true)
            {
                string file = saveFileDialog.FileName;

                if (PdfCreator.Create(createOperation(), VehicleCollection, file))
                {
                    Process.Start(file);
                }
                else
                {
                    MessageBox.Show("Error creating pdf", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddResourceCommand_Execute(object param)
        {
            Vehicle resource = ResourceDialog.Prompt();
            if(resource != null)
            {
                VehicleCollection.Add(resource);
            }
        }

        #endregion

        #endregion


        internal ViewModel()
            : base()
        {
            CurrentOperation = new Operation();
            VehicleCollection = new ObservableCollection<Vehicle>();

            try
            {
                _settingsService = ServiceFactory.GetCallbackServiceWrapper<ISettingsService>(this);
            }
            catch (EndpointNotFoundException)
            {
                MessageBox.Show("Error connecting service", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            Server = String.Format("{0}:{1}", Constants.DefaultServer, DefaultPort);
        }

        public void OnSettingChanged(IList<SettingKey> keys)
        {
            //nothing to do here at the moment
        }

        private Operation createOperation()
        {
            Random rnd = new Random();
            string date = DateTime.Now.ToString("yyMMdd");
            int number = rnd.Next(1000, 9999);
            string randomOperation = string.Format("T 1.1 {0} {1}", date, number);

            OperationResourceCollection resourceCollection = new OperationResourceCollection();
            foreach (Vehicle vehicle in VehicleCollection)
                resourceCollection.Add(vehicle.ToOperationResource());

            CurrentOperation.Timestamp = DateTime.Now;
            CurrentOperation.OperationNumber = randomOperation;
            CurrentOperation.Resources = resourceCollection;

            return CurrentOperation;
        }
    }
}
