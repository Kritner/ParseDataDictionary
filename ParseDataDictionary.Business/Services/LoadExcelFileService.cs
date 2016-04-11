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
    /// Load an excel file
    /// </summary>
    public class LoadExcelFileService : ILoadExcelFile
    {

        private readonly IFileExists _iFileExists;
        private readonly string _fileNameAndPath;

        /// <summary>
        /// Constructor - takes in dependencies.
        /// </summary>
        /// <param name="iFileExists">The file exists implementation</param>
        public LoadExcelFileService(IFileExists iFileExists, string fileNameAndPath)
        {
            if (iFileExists == null)
                throw new ArgumentNullException(nameof(iFileExists));

            _iFileExists = iFileExists;
            _fileNameAndPath = fileNameAndPath;

            if (!_iFileExists.CheckFileExists(fileNameAndPath))
            {
                throw new FileNotFoundException(nameof(fileNameAndPath));
            }
        }

        /// <summary>
        /// Loads the file
        /// </summary>
        public XLWorkbook Execute()
        {
            return new XLWorkbook(_fileNameAndPath);
        }

    }
}
