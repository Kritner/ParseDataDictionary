using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDataDictionaryForExtendedProperties.Business.Models
{
    /// <summary>
    /// Represents a table
    /// </summary>
    public class Table
    {

        #region Properties
        /// <summary>
        /// The table's name
        /// </summary>
        public string TableName { get; private set; }
        
        /// <summary>
        /// The table's description
        /// </summary>
        public string TableDescription { get; private set; }

        /// <summary>
        /// The columns that belong to a table
        /// </summary>
        public IEnumerable<TableColumn> TableColumns { get; private set; }
        #endregion Properties

        #region ctor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="tableDescription">The table description</param>
        /// <param name="tableColumns">The table columns</param>
        public Table(string tableName, string tableDescription, IEnumerable<TableColumn> tableColumns)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException(nameof(tableName));
            if (string.IsNullOrEmpty(tableDescription))
                throw new ArgumentNullException(nameof(tableDescription));

            TableName = tableName;
            TableDescription = tableDescription;
            TableColumns = tableColumns;
        }
        #endregion ctor

    }
}
