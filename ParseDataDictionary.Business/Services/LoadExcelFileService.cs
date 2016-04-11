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
            if (string.IsNullOrEmpty(fileNameAndPath))
                throw new ArgumentNullException(nameof(fileNameAndPath));

            _iFileExists = iFileExists;
            _fileNameAndPath = fileNameAndPath;
        }

        /// <summary>
        /// Loads the file
        /// </summary>
        public XLWorkbook Execute()
        {
            if (!_iFileExists.CheckFileExists(_fileNameAndPath))
                throw new FileNotFoundException(nameof(_fileNameAndPath));

            return new XLWorkbook(_fileNameAndPath);
        }

    }
}
