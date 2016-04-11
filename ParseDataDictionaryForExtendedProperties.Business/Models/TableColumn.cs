using System;

namespace ParseDataDictionaryForExtendedProperties.Business.Models
{

    /// <summary>
    /// Represents a column within a table
    /// </summary>
    public class TableColumn
    {

        #region Properties
        /// <summary>
        /// The TableColumn's name
        /// </summary>
        public string TableColumnName { get; private set; }

        /// <summary>
        /// The TableColumn's description
        /// </summary>
        public string TableColumnDescription { get; private set; }
        #endregion Properties

        #region ctor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tableColumnName">The table column name</param>
        /// <param name="tableColumnDescription">The table column description</param>
        public TableColumn(string tableColumnName, string tableColumnDescription)
        {
            if (string.IsNullOrEmpty(tableColumnName))
                throw new ArgumentNullException(nameof(tableColumnName));
            if (string.IsNullOrEmpty(tableColumnDescription))
                throw new ArgumentNullException(nameof(tableColumnDescription));

            TableColumnName = tableColumnName;
            TableColumnDescription = tableColumnDescription;
        }
        #endregion ctor
    }
}