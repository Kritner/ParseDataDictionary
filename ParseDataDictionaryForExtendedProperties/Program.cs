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

            LoadExcelFileService excelService = 
                new LoadExcelFileService(
                    new FileExistsService(), 
                    dataDictionaryFileNameAndPath
                );

            XLWorkbook excelFile = excelService.Execute();

            Console.WriteLine("");
        }
    }
}
