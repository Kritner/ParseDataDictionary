using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParseDataDictionary.Business.Services;

namespace ParseDataDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path and filename to data dictionary and press <ENTER>");
            string dataDictionaryFileNameAndPath = Console.ReadLine();

            // TODO - Consider an IOC container for getting references, this is getting complex
            LoadParseExportDataDictionaryService service = new LoadParseExportDataDictionaryService(
                new LoadExcelFileService(
                    new FileExistsService(),
                    dataDictionaryFileNameAndPath
                ),
                new ExcelDataDictionaryParserService(),
                new GenerateSqlScriptsForExtendedPropertiesService(),
                new SQLScriptFileWriterService()
            );
            var results = service.Execute();

            Console.WriteLine("");
        }
    }
}
