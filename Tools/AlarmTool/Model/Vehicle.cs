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
using System.Linq;

namespace AlarmWorkflow.Tools.Alarm.Model
{
    public class Vehicle
    {
        public string Name { get; set; }
        public string Equipment { get; set; }

        public Vehicle(string name, string equipment)
        {
            Name = name;
            Equipment = equipment;
        }

        public OperationResource ToOperationResource()
        {
            return new OperationResource()
            {
                FullName = Name,
                RequestedEquipment = Equipment.Split(';').ToList()
            };
        }

        public static Vehicle FromOperationResource(OperationResource resource)
        {
            return new Vehicle(resource.FullName, string.Join(";", resource.RequestedEquipment));
        }

        public override string ToString()
        {
            string output = Name;
            if(!string.IsNullOrEmpty(Equipment))
            {
                output += string.Format(" ({0})", Equipment);
            }

            return output;
        }
    }
}
