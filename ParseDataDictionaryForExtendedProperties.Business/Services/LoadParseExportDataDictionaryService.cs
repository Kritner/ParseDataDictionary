using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;

namespace ParseDataDictionaryForExtendedProperties.Business.Services
{

    /// <summary>
    /// Loads, parses, and generates SQL scripts for specified Excel data dictionary.
    /// </summary>
    public class LoadParseExportDataDictionaryService
    {

        /// <summary>
        /// Loads, parses, and generates SQL scripts
        /// </summary>
        /// <param name="fileNameAndPath">The list of sql scripts to execute</param>
        /// <returns></returns>
        public List<string> Execute(string fileNameAndPath)
        {
            // Load the document
            LoadExcelFileService excelService =
                new LoadExcelFileService(
                    new FileExistsService(),
                    fileNameAndPath
                );
            XLWorkbook excelFile = excelService.Execute();

            // Parse the document
            ExcelDataDictionaryParserService parserService =
                new ExcelDataDictionaryParserService();
            var tables = parserService.ParseDocumentIntoModel(excelFile);

            // Generate the SQL scripts to insert the table, table description, columns, and column description.
            GenerateSqlScriptsForExtendedPropertiesService sqlScriptService =
                new GenerateSqlScriptsForExtendedPropertiesService();
            var sqlScripts = sqlScriptService.GetSqlScripts(tables);

            // Write the sqlScripts to a file
            TextWriter tw = new StreamWriter("SqlScript.txt");
            foreach (string s in sqlScripts)
                tw.WriteLine(s);
            tw.Close();

            return sqlScripts;
        }

    }
}
