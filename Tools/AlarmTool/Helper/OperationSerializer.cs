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

namespace AlarmWorkflow.Tools.Alarm.Helper
{
    internal static class OperationHelper
    {
        /// <summary>
        /// Serialize a operation to a file
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="file"></param>
        /// <returns>True when file saved correctly</returns>
        public static bool Serialize(Operation operation, string file)
        {
            try
            {
                string json = Json.Serialize(operation, true);
                using (var stream = File.CreateText(file))
                {
                    stream.Write(json);
                }
                return true;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(typeof(OperationHelper), ex);
                return false;
            }
        }

        /// <summary>
        /// Deserialize from a file
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Operation Deserialize(string file)
        {
            string text = File.ReadAllText(file);

            try
            {
                Operation operation = Json.Deserialize<Operation>(text, true);
                return operation;
            }
            catch (Exception ex)
            {
                Logger.Instance.LogException(typeof(OperationHelper), ex);
                return null;
            }
        }

    }
}
