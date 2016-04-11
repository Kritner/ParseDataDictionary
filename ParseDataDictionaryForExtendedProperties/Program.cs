using System;
using System.Collections.Generic;
using System.IO;
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

            LoadParseExportDataDictionaryService service = new LoadParseExportDataDictionaryService();
            var results = service.Execute(dataDictionaryFileNameAndPath);

            Console.WriteLine("");
        }
    }
}
