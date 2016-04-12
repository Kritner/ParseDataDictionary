using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParseDataDictionary.Business.Interfaces;
using ParseDataDictionary.Business.Models;

namespace ParseDataDictionary.Business.Services
{

    /// <summary>
    /// Generate the SQL scripts used to create the extended properties based on an IEnumerable of Table
    /// </summary>
    public class GenerateSqlScriptsForExtendedPropertiesService : IGenerateSqlScriptsForExtendedProperties
    {
        #region const
        /// <summary>
        /// Generate a comment for a table (section) start
        /// </summary>
        /// <remarks>
        /// {0} is the table name
        /// </remarks>
        /// <example>
        /// <code>
        ///     string.Format(_GENERATING_COMMENT_FOR_TABLE, "MyTable");
        /// </code>
        /// </example>
        public const string _GENERATING_COMMENT_FOR_TABLE = @"--Generating scripts for {0}";
        
        /// <summary>
        /// Create (or update) an extended property for a table
        /// </summary>
        /// <remarks>
        /// {0} is table name
        /// {1} is table description
        /// </remarks>
        /// <example>
        /// <code>
        ///     string.Format(_SCRIPT_TEMPLATE_FOR_TABLE, "MyTable", "My Table Description");
        /// </code>
        /// </example>
        public const string _SCRIPT_TEMPLATE_FOR_TABLE = 
            @"IF NOT EXISTS (
                SELECT NULL 
                FROM SYS.EXTENDED_PROPERTIES 
                WHERE [major_id] = OBJECT_ID('{0}') 
                    AND [name] = N'MS_Description' 
                    AND [minor_id] = 0
            )
                EXECUTE sp_addextendedproperty 
                    @name = N'MS_Description', 
                    @value = N'{1}', 
                    @level0type = N'SCHEMA', 
                    @level0name = N'dbo', 
                    @level1type = N'TABLE', 
                    @level1name = N'{0}';
            ELSE
                EXECUTE sp_updateextendedproperty 
                    @name = N'MS_Description', 
                    @value = N'{1}', 
                    @level0type = N'SCHEMA', 
                    @level0name = N'dbo', 
                    @level1type = N'TABLE', 
                    @level1name = N'{0}';
        ";

        /// <summary>
        /// Create (or update) an extended property for a table's column
        /// </summary>
        /// <remarks>
        /// {0} is table name
        /// {1} is column name
        /// {2} is column description
        /// </remarks>
        /// <example>
        /// <code>
        ///     string.Format(_SCRIPT_TEMPLATE_FOR_TABLE_COLUMN, "MyTable", "MyColumn", "My Column Description");
        /// </code>
        /// </example>
        public const string _SCRIPT_TEMPLATE_FOR_TABLE_COLUMN = 
            @"IF NOT EXISTS (
                SELECT NULL 
                FROM SYS.EXTENDED_PROPERTIES 
                WHERE [major_id] = OBJECT_ID('{0}') 
                    AND [name] = N'MS_Description' 
                    AND [minor_id] = (
                        SELECT [column_id] 
                        FROM SYS.COLUMNS 
                        WHERE [name] = '{1}' 
                            AND [object_id] = OBJECT_ID('{0}')
                    )
            )
                EXECUTE sp_addextendedproperty 
                    @name = N'MS_Description', 
                    @value = N'{2}', 
                    @level0type = N'SCHEMA', 
                    @level0name = N'dbo', 
                    @level1type = N'TABLE', 
                    @level1name = N'{0}', 
                    @level2type = N'COLUMN', 
                    @level2name = N'{1}';
            ELSE
                EXECUTE sp_updateextendedproperty 
                    @name = N'MS_Description', 
                    @value = N'{2}', 
                    @level0type = N'SCHEMA', 
                    @level0name = N'dbo', 
                    @level1type = N'TABLE', 
                    @level1name = N'{0}', 
                    @level2type = N'COLUMN', 
                    @level2name = N'{1}';
        ";
        #endregion const

        /// <summary>
        /// returns the list of SQL scripts to be used to create extended properties in the database.
        /// </summary>
        /// <param name="tables">The tables to create SQL scripts based on.</param>
        /// <returns>List of string</returns>
        public List<string> GetSqlScripts(IEnumerable<Models.Table> tables)
        {
            List<string> list = new List<string>();

            foreach (Models.Table table in tables)
                list.AddRange(GenerateTableScripts(table));

            return list;
        }

        /// <summary>
        /// Get table scripts
        /// </summary>
        /// <param name="table">The table to parse</param>
        /// <returns></returns>
        private IEnumerable<string> GenerateTableScripts(Table table)
        {
            List<string> list = new List<string>();

            // Replace "'" with "''" for escaping in a SQL script
            string escapedTableDescription = table.TableDescription;
            escapedTableDescription = escapedTableDescription.Replace(@"'", @"''");

            // Comment for start of table
            list.Add(
                string.Format(
                    _GENERATING_COMMENT_FOR_TABLE, 
                    table.TableName
                )
            );

            // Table description
            list.Add(
                string.Format(
                    _SCRIPT_TEMPLATE_FOR_TABLE, 
                    table.TableName,
                    escapedTableDescription
                )
            );

            // For every column in the table, create a script for that column's description
            foreach (TableColumn tableColumn in table.TableColumns)
                list.AddRange(GenerateTableColumnScripts(table.TableName, tableColumn));

            return list;
        }

        /// <summary>
        /// Get table column scripts
        /// </summary>
        /// <param name="tableName">The table name</param>
        /// <param name="tableColumn">The table column information</param>
        /// <returns></returns>
        private IEnumerable<string> GenerateTableColumnScripts(string tableName, TableColumn tableColumn)
        {
            List<string> list = new List<string>();

            // Replace "'" with "''" for escaping in a SQL script
            string escapedColumnDescription = tableColumn.TableColumnDescription;
            escapedColumnDescription = escapedColumnDescription.Replace(@"'", @"''");

            list.Add(
                string.Format(
                    _SCRIPT_TEMPLATE_FOR_TABLE_COLUMN, 
                    tableName, 
                    tableColumn.TableColumnName, 
                    escapedColumnDescription
                )
            );

            return list;
        }
    }
}
