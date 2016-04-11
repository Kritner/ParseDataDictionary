using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ParseDataDictionaryForExtendedProperties.Business.Interfaces;
using ParseDataDictionaryForExtendedProperties.Business.Models;

namespace ParseDataDictionaryForExtendedProperties.Business.Services
{
    /// <summary>
    /// Used to parse an excel file
    /// </summary>
    public class ExcelDataDictionaryParserService : IExcelDataDictionaryParser
    {

        /// <summary>
        /// The column and row for the table name
        /// </summary>
        /// <remarks>
        /// In the form of (example) "A1"
        /// </remarks>
        public string TableNameColumnRow
        {
            get
            {
                return "B4";
            }
        }

        /// <summary>
        /// The column and row for the table description
        /// </summary>
        /// <remarks>
        /// In the form of (example) "A2"
        /// </remarks>
        public string TableDescriptionColumnRow
        {
            get
            {
                return "B5";
            }
        }

        /// <summary>
        /// The row the Table's columns begin on
        /// </summary>
        public int TableColumnsStartRow
        {
            get
            {
                return 7;
            }
        }

        /// <summary>
        /// The TableColumn's name position
        /// </summary>
        /// <remarks>
        /// In the form of "A"
        /// </remarks>
        public string TableColumnNameColumnPosition
        {
            get
            {
                return "A";
            }
        }

        /// <summary>
        /// The TableColumn's description position
        /// </summary>
        /// <remarks>
        /// In the form of "B"
        /// </remarks>
        public string TableColumnDescriptionColumnPosition
        {
            get
            {
                return "B";
            }
        }

        /// <summary>
        /// Parses the provided XLWorkbook into an IEnumerable of Table
        /// </summary>
        /// <param name="workbook">The workbook to parse</param>
        /// <returns>IEnumerable of Table</returns>
        public IEnumerable<Table> ParseDocumentIntoModel(XLWorkbook workbook)
        {
            List<Table> list = new List<Table>();

            foreach(IXLWorksheet sheet in workbook.Worksheets)
                list.Add(CreateTableFromSheet(sheet));

            return list;
        }

        /// <summary>
        /// Generate a Table object based on a sheet
        /// </summary>
        /// <param name="sheet">The sheet to parse</param>
        /// <returns>The Table object</returns>
        private Table CreateTableFromSheet(IXLWorksheet sheet)
        {
            List<TableColumn> tableColumns = CreateTableColumnsFromSheet(sheet);
            string tableName = sheet.Cell(TableNameColumnRow).Value.ToString();
            string tableDescription = sheet.Cell(TableDescriptionColumnRow).Value.ToString();

            return new Table(tableName, tableDescription, tableColumns);
        }

        private List<TableColumn> CreateTableColumnsFromSheet(IXLWorksheet sheet)
        {
            List<TableColumn> list = new List<TableColumn>();

            // For every row (starting at TableColumnsStartRow) until the last row used in the sheet
            for (int rowIterator = TableColumnsStartRow; rowIterator < sheet.LastRowUsed().RowNumber(); rowIterator++)
            {
                IXLRow row = sheet.Row(rowIterator);
                string columnName = row.Cell(TableColumnNameColumnPosition).Value.ToString();
                string columnDescription = row.Cell(TableColumnDescriptionColumnPosition).Value.ToString();

                // Generate a TableColumn
                list.Add(new TableColumn(columnName, columnDescription));
            }

            return list;
        }
    }
}
