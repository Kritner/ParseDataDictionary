using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParseDataDictionaryForExtendedProperties.Interfaces;

namespace ParseDataDictionaryForExtendedProperties.Services
{
    public class FileExistsService : IFileExists
    {
        public bool CheckFileExists(string fullFileNameAndPath)
        {
            if (string.IsNullOrEmpty(fullFileNameAndPath))
                throw new ArgumentException(nameof(fullFileNameAndPath));

            return File.Exists(fullFileNameAndPath);
        }
    }
}
