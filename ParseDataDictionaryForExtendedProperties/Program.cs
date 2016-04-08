using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParseDataDictionaryForExtendedProperties.Services;

namespace ParseDataDictionaryForExtendedProperties
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path and filename to data dictionary and press <ENTER>");
            string dataDictionaryFileNameAndPath = Console.ReadLine();

            LoadExcelFileService service = 
                new LoadExcelFileService(
                    new FileExistsService(), 
                    dataDictionaryFileNameAndPath
                );

            var results = service.Execute();
            Console.WriteLine("");
        }
    }
}
