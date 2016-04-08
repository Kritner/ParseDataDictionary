using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ParseDataDictionaryForExtendedProperties.Interfaces;

namespace ParseDataDictionaryForExtendedProperties.Services
{

    /// <summary>
    /// Load an excel file
    /// </summary>
    public class LoadExcelFileService
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
            XLWorkbook wb = new XLWorkbook(_fileNameAndPath);

            return wb;
        }

    }
}
