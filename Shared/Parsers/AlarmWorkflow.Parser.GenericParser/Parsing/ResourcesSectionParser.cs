﻿using AlarmWorkflow.Shared.Core;

namespace AlarmWorkflow.Parser.GenericParser.Parsing
{
    /// <summary>
    /// Provides a section parser that parses a section describing a location.
    /// </summary>
    [Export("ResourcesSectionParser", typeof(ISectionParser))]
    public class ResourcesSectionParser : ISectionParser
    {
        #region Fields

        private OperationResource _currentResource;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcesSectionParser"/> class.
        /// </summary>
        public ResourcesSectionParser()
        {

        }

        #endregion

        #region ISectionParser Members

        System.Collections.Generic.IEnumerable<string> ISectionParser.GetTokens()
        {
            yield break;
        }

        void ISectionParser.OnLoad(System.Xml.Linq.XElement sectionElement)
        {

        }

        void ISectionParser.OnSave(System.Xml.Linq.XElement sectionElement)
        {

        }

        void ISectionParser.OnEnterSection(Operation operation)
        {
            _currentResource = new OperationResource();
        }

        void ISectionParser.OnLeaveSection(Operation operation)
        {
            if (_currentResource != null)
            {
                operation.Resources.Add(_currentResource);
            }
        }

        void ISectionParser.Populate(AreaToken token, Operation operation)
        {
        }

        #endregion
    }
}
