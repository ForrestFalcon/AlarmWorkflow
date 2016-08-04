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

using AlarmWorkflow.Shared.Core;
using AlarmWorkflow.Shared.Diagnostics;
using AlarmWorkflow.Tools.Alarm.Model;
using Mustache;
using PdfSharp;
using PdfSharp.Pdf;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace AlarmWorkflow.Tools.Alarm.Helper
{
    internal static class PdfCreator
    {
        /// <summary>
        /// Generate a alarmfax pdf
        /// </summary>
        /// <param name="operation">Actual operation</param>
        /// <param name="file">PDf file</param>
        /// <returns>True when pdf is created</returns>
        public static bool Create(Operation operation, Collection<Vehicle> vehicleCollection, string file)
        {
            try
            {
                string text = File.ReadAllText(Constants.PdfSource);
               
                FormatCompiler compiler = new FormatCompiler();
                Generator generator = compiler.Compile(text);
                string result = generator.Render(new { Operation = operation, Vehicles = vehicleCollection });

                // Store the whole rendered image and share it across multiple pages.

                using (PdfDocument pdf = PdfGenerator.GeneratePdf(result, PageSize.A4))
                {
                    pdf.Save(file);
                }

                return true;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(typeof(PdfGenerator), ex);
                return false;
            }
        }
    }
}
