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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AlarmWorkflow.BackendService.AddressingContracts;
using AlarmWorkflow.BackendService.AddressingContracts.EntryObjects;
using AlarmWorkflow.BackendService.EngineContracts;
using AlarmWorkflow.BackendService.SettingsContracts;
using AlarmWorkflow.Shared.Core;
using AlarmWorkflow.Shared.Diagnostics;

namespace AlarmWorkflow.Job.OpenPager
{ //
    /// <summary>
    /// Implements a Job that send notifications to the Android App eAlarm.
    /// </summary>
    [Export(nameof(OpenPager), typeof(IJob))]
    [Information(DisplayName = "ExportJobDisplayName", Description = "ExportJobDescription")]
    class OpenPager : IJob
    {
        #region Contstants

        private const string Path = "/api/v1/operations";

        #endregion

        #region Fields

        private ISettingsServiceInternal _settings;

        #endregion

        #region IJob Members

        bool IJob.Initialize(IServiceProvider serviceProvider)
        {
            _settings = serviceProvider.GetService<ISettingsServiceInternal>();

            return true;
        }

        void IJob.Execute(IJobContext context, Operation operation)
        {
            if (context.Phase != JobPhase.AfterOperationStored)
            {
                return;
            }

            string key = operation.ToString(_settings.GetSetting("OpenPager", "apiKey").GetValue<string>());
            string url = operation.ToString(_settings.GetSetting("OpenPager", "apiUrl").GetValue<string>());

            SendNotification(new OpenPagerContent(operation, key), url);
        }

        bool IJob.IsAsync => true;

        #endregion

        #region Methods

        private void SendNotification(OpenPagerContent operation, string url)
        {
            string message = Json.Serialize(operation, true, true);

            byte[] byteArray = Encoding.UTF8.GetBytes(message);

            ServicePointManager.ServerCertificateValidationCallback += ValidateServerCertificate;

            HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url + Path);
            request.Method = WebRequestMethods.Http.Post;
            request.KeepAlive = false;
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            using (var dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    HttpStatusCode responseCode = ((HttpWebResponse) response).StatusCode;

                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    String responseString = reader.ReadToEnd();

                    Logger.Instance.LogFormat(LogType.Debug, this, Properties.Resources.DebugGetResponse, responseCode, responseString);
                }
            }
            catch (Exception exception)
            {
                Logger.Instance.LogFormat(LogType.Error, this, Properties.Resources.ErrorSendingNotification, message);
                Logger.Instance.LogException(this, exception);
            }
        }

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
        }

        #endregion
    }
}