﻿// This file is part of AlarmWorkflow.
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
using System.IO;
using System.Windows;
using AlarmWorkflow.Shared.Core;
using AlarmWorkflow.Windows.CustomViewer.Extensibility;

namespace AlarmWorkflow.Windows.UIWidgets.OSM
{
    /// <summary>
    ///     Interaktionslogik für UserControl1.xaml
    /// </summary>
    [Export("OSMWidget", typeof(IUIWidget))]
    [Information(DisplayName = "ExportUIWidgetDisplayName", Description = "ExportUIWidgetDescription")]
    public partial class OSMWidget : IUIWidget
    {
        #region Fields

        private readonly string _osmFile;
        private Operation _operation;

        #endregion

        #region Constructors

        public OSMWidget()
        {
            InitializeComponent();
            _osmFile = Path.Combine(Path.GetTempPath(), "osm.html");
            BuildHtml();
        }

        #endregion

        #region IUIWidget Members

        void IUIWidget.Close()
        {
        }

        bool IUIWidget.Initialize()
        {
            return true;
        }

        void IUIWidget.OnOperationChange(Operation operation)
        {
            if (operation == null)
            {
                return;
            }
            _operation = operation;
            string html = BuildHtml();
            File.WriteAllText(_osmFile, html);
            _webBrowser.Navigate(_osmFile);
        }

        UIElement IUIWidget.UIElement => this;

        string IUIWidget.ContentGuid => "E70E128B-6A6C-4B9F-9D5A-83360BC52F8C";

        string IUIWidget.Title => "OSM-MAP";

        #endregion

        #region Methods

        private string BuildHtml()
        {
            if (_operation != null)
            {
                if (!_operation.Einsatzort.HasGeoCoordinates)
                {
                    return "<h2>Konnte Geocodes fuer Zielort nicht bestimmen! Ggf. ist der Geocoding Job nicht aktiv?</h2>";
                }
                return Properties.Resources.HTMLTemplate.Replace("{0}", _operation.Einsatzort.GeoLatitudeString).Replace("{1}", _operation.Einsatzort.GeoLongitudeString);
            }
            return string.Empty;

        }

        #endregion
    }
}