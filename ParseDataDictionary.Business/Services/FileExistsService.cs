using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParseDataDictionary.Business.Interfaces;

namespace ParseDataDictionary.Business.Services
{

    /// <summary>
    /// Check if a file exists
    /// </summary>
    public class FileExistsService : IFileExists
    {

        /// <summary>
        /// Check if a file exists
        /// </summary>
        /// <param name="fullFileNameAndPath">The path to check</param>
        /// <returns></returns>
        public bool CheckFileExists(string fullFileNameAndPath)
        {
            if (string.IsNullOrEmpty(fullFileNameAndPath))
                throw new ArgumentNullException(nameof(fullFileNameAndPath));

            return File.Exists(fullFileNameAndPath);
        }
    }
}
