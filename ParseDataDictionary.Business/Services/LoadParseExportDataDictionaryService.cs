using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ParseDataDictionary.Business.Interfaces;

namespace ParseDataDictionary.Business.Services
{

    /// <summary>
    /// Loads, parses, and generates SQL scripts for specified Excel data dictionary.
    /// </summary>
    public class LoadParseExportDataDictionaryService
    {

        #region private
        ILoadExcelFile _iLoadExcelFile;
        IExcelDataDictionaryParser _iExcelDataDictionaryParser;
        IGenerateSqlScriptsForExtendedProperties _iGenerateSqlScriptsForExtendedProperties;
        IFileWriter _iFileWriter;
        #endregion private

        #region ctor
        /// <summary>
        /// Constructor - takes in dependencies
        /// </summary>
        /// <param name="iLoadExcelFile">The implementation of the ILoadExcelFile</param>
        /// <param name="iExcelDataDictionaryParser">The implementation of the IExcelDataDictionaryParser</param>
        /// <param name="iGenerateSqlScriptsForExtendedProperties">The implementation of the IGenerateSqlScriptsForExtendedProperties</param>
        /// <param name="iFileWriter">The implementation of the IFileWriter</param>
        public LoadParseExportDataDictionaryService(ILoadExcelFile iLoadExcelFile, IExcelDataDictionaryParser iExcelDataDictionaryParser, IGenerateSqlScriptsForExtendedProperties iGenerateSqlScriptsForExtendedProperties, IFileWriter iFileWriter)
        {
            if (iLoadExcelFile == null)
                throw new ArgumentNullException(nameof(iLoadExcelFile));
            if (iExcelDataDictionaryParser == null)
                throw new ArgumentNullException(nameof(iExcelDataDictionaryParser));
            if (iGenerateSqlScriptsForExtendedProperties == null)
                throw new ArgumentNullException(nameof(iGenerateSqlScriptsForExtendedProperties));
            if (iFileWriter == null)
                throw new ArgumentNullException(nameof(iFileWriter));

            _iLoadExcelFile = iLoadExcelFile;
            _iExcelDataDictionaryParser = iExcelDataDictionaryParser;
            _iGenerateSqlScriptsForExtendedProperties = iGenerateSqlScriptsForExtendedProperties;
            _iFileWriter = iFileWriter;
        }
        #endregion ctor

        /// <summary>
        /// Loads, parses, and generates SQL scripts
        /// </summary>
        /// <param name="fileNameAndPath">The list of sql scripts to execute</param>
        /// <returns></returns>
        public List<string> Execute()
        {
            // Load the document
            XLWorkbook excelFile = _iLoadExcelFile.Execute();

            // Parse the document
            var tables = _iExcelDataDictionaryParser.ParseDocumentIntoModel(excelFile);

            // Generate the SQL scripts to insert the table, table description, columns, and column description.
            var sqlScripts = _iGenerateSqlScriptsForExtendedProperties.GetSqlScripts(tables);

            // Write the sqlScripts to a file
            _iFileWriter.WriteFile("SqlScript.txt", sqlScripts);

            return sqlScripts;
        }

    }
}
