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
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AlarmWorkflow.Tools.Alarm.Helper
{
    internal static class Network
    {
        #region Constants

        private const int BufferSize = 1024;

        #endregion

        public static void SendOperation(String server, Operation operation)
        {
            IPEndPoint endPoint = ParseHost(server);

            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(endPoint);
                    using (NetworkStream stream = client.GetStream())
                    {
                        string jsonData = Json.Serialize(operation, true);
                        byte[] buffer = Encoding.UTF8.GetBytes(jsonData);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(typeof(Network), ex);
                throw ex;
            }
        }

        #region Get IPEndPoint
        public static IPEndPoint ParseHost(string endpointstring)
        {
            return ParseHost(endpointstring, -1);
        }

        public static IPEndPoint ParseHost(string endpointstring, int defaultport)
        {
            if (string.IsNullOrEmpty(endpointstring)
                || endpointstring.Trim().Length == 0)
            {
                throw new ArgumentException("Endpoint descriptor may not be empty.");
            }

            if (defaultport != -1 &&
                (defaultport < IPEndPoint.MinPort
                || defaultport > IPEndPoint.MaxPort))
            {
                throw new ArgumentException(string.Format("Invalid default port '{0}'", defaultport));
            }

            string[] values = endpointstring.Split(new char[] { ':' });
            IPAddress ipaddy;
            int port = -1;

            //check if we have an IPv6 or ports
            if (values.Length <= 2) // ipv4 or hostname
            {
                if (values.Length == 1)
                    //no port is specified, default
                    port = defaultport;
                else
                    port = getPort(values[1]);

                //try to use the address as IPv4, otherwise get hostname
                if (!IPAddress.TryParse(values[0], out ipaddy))
                    ipaddy = getIPfromHost(values[0]);
            }
            else if (values.Length > 2) //ipv6
            {
                //could [a:b:c]:d
                if (values[0].StartsWith("[") && values[values.Length - 2].EndsWith("]"))
                {
                    string ipaddressstring = string.Join(":", values.Take(values.Length - 1).ToArray());
                    ipaddy = IPAddress.Parse(ipaddressstring);
                    port = getPort(values[values.Length - 1]);
                }
                else //[a:b:c] or a:b:c
                {
                    try
                    {
                        ipaddy = IPAddress.Parse(endpointstring);
                        port = defaultport;
                    }
                    catch (FormatException ex)
                    {
                        throw new ArgumentException("Invalid ipaddress", ex);
                    }
                }
            }
            else
            {
                throw new ArgumentException(string.Format("Invalid endpoint ipaddress '{0}'", endpointstring));
            }

            if (port == -1)
                throw new ArgumentException(string.Format("No port specified: '{0}'", endpointstring));

            return new IPEndPoint(ipaddy, port);
        }

        private static int getPort(string p)
        {
            int port;

            if (!int.TryParse(p, out port)
             || port < IPEndPoint.MinPort
             || port > IPEndPoint.MaxPort)
            {
                throw new FormatException(string.Format("Invalid end point port '{0}'", p));
            }

            return port;
        }

        private static IPAddress getIPfromHost(string p)
        {
            var hosts = Dns.GetHostAddresses(p);

            if (hosts == null || hosts.Length == 0)
                throw new ArgumentException(string.Format("Host not found: {0}", p));

            IPAddress adress = hosts.Where(_ => _.AddressFamily == AddressFamily.InterNetwork).SingleOrDefault();
            return adress == null ? hosts[0] : adress;
        }
        #endregion
    }
}
