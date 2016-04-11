using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ParseDataDictionary.Business.Models;

namespace ParseDataDictionary.Business.Interfaces
{
    /// <summary>
    /// Interface for parsing the IExcelDataDictionary
    /// </summary>
    public interface IExcelDataDictionaryParser
    {

        /// <summary>
        /// The column and row for the table name
        /// </summary>
        /// <remarks>
        /// In the form of (example) "A1"
        /// </remarks>
        string TableNameColumnRow { get; }

        /// <summary>
        /// The column and row for the table description
        /// </summary>
        /// <remarks>
        /// In the form of (example) "A2"
        /// </remarks>
        string TableDescriptionColumnRow { get; }

        /// <summary>
        /// The row the Table's columns begin on
        /// </summary>
        int TableColumnsStartRow { get; }

        /// <summary>
        /// The TableColumn's name position
        /// </summary>
        /// <remarks>
        /// In the form of "A"
        /// </remarks>
        string TableColumnNameColumnPosition { get; }

        /// <summary>
        /// The TableColumn's description position
        /// </summary>
        /// <remarks>
        /// In the form of "B"
        /// </remarks>
        string TableColumnDescriptionColumnPosition { get; }

        /// <summary>
        /// Parses the provided XLWorkbook into an IEnumerable of Table
        /// </summary>
        /// <param name="workbook">The workbook to parse</param>
        /// <returns>IEnumerable of Table</returns>
        IEnumerable<Table> ParseDocumentIntoModel(XLWorkbook workbook);

    }
}
