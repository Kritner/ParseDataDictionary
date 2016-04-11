using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ParseDataDictionaryForExtendedProperties.Business.Services;

namespace ParseDataDictionaryForExtendedProperties
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path and filename to data dictionary and press <ENTER>");
            string dataDictionaryFileNameAndPath = Console.ReadLine();

            // Load the document
            LoadExcelFileService excelService = 
                new LoadExcelFileService(
                    new FileExistsService(), 
                    dataDictionaryFileNameAndPath
                );
            XLWorkbook excelFile = excelService.Execute();

            // Parse the document
            ExcelDataDictionaryParserService parserService = new ExcelDataDictionaryParserService();
            var tables = parserService.ParseDocumentIntoModel(excelFile);

            // Generate the SQL scripts to insert the table, table description, columns, and column description.


            Console.WriteLine("");
        }
    }
}
