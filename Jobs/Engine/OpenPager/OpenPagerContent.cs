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

namespace AlarmWorkflow.Job.OpenPager
{
    /// <summary>
    /// Content of the eAlarm message.
    /// The vars have to be lowercase!
    /// </summary>
    internal class OpenPagerContent : Operation
    {
        public OpenPagerContent(Operation operation, string accessKey)
        {
            OperationGuid = operation.OperationGuid;
            IsAcknowledged = operation.IsAcknowledged;
            OperationNumber = operation.OperationNumber;
            TimestampIncome = operation.TimestampIncome;
            Timestamp = operation.Timestamp;
            Comment = operation.Comment;
            Messenger = operation.Messenger;
            OperationPlan = operation.OperationPlan;
            Picture = operation.Picture;
            Priority = operation.Priority;
            Loops = operation.Loops;
            Einsatzort = operation.Einsatzort;
            Zielort = operation.Zielort;
            Keywords = operation.Keywords;
            CustomData =operation.CustomData;

            AccessKey = accessKey;
        }

        /// <summary>
        /// Representation of the access key.
        /// </summary>
        public string AccessKey;
    }
}

