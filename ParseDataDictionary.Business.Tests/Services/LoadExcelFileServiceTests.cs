using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using ClosedXML.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParseDataDictionary.Business.Interfaces;
using ParseDataDictionary.Business.Services;

namespace ParseDataDictionary.Business.Tests.Services
{

    /// <summary>
    /// Tests for LoadExcelFileService
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LoadExcelFileServiceTests
    {
        #region Private
        private Mock<IFileExists> _mockIFileExists;
        private string _fileName;
        private LoadExcelFileService _biz;
        #endregion Private

        #region Setup
        /// <summary>
        /// Test initializer
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _mockIFileExists = new Mock<IFileExists>();
            _fileName = "testDataDictionary.xlsx";
            _biz = new LoadExcelFileService(_mockIFileExists.Object, _fileName);
        }
        #endregion Setup

        #region Public methods/tests
        /// <summary>
        /// ArgumentNullException thrown when IFileExists not provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadExcelFileService_ctor_ArgumentNullExceptionThrownWhenIFileExistsNotProvided()
        {
            // Arrange / Act / Assert
            _biz = new LoadExcelFileService(null, _fileName);
        }

        /// <summary>
        /// ArgumentNullException thrown when IFileExists not provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadExcelFileService_ctor_ArgumentNullExceptionThrownWhenIFileNameNotProvided()
        {
            // Arrange / Act / Assert
            _biz = new LoadExcelFileService(_mockIFileExists.Object, null);
        }

        /// <summary>
        /// Execute throws FileNotFoundException when file does not exist
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void LoadExcelFileService_Execute_ThrowsFileNotFoundExceptionWhenFileDoesNotExist()
        {
            // Arrange 
            _mockIFileExists
                .Setup(s => s.CheckFileExists(It.IsAny<string>()))
                .Returns(false);

            // Act / Assert
            var results = _biz.Execute();
            _mockIFileExists.Verify(v => v.CheckFileExists(It.IsAny<string>()), Times.Once, "CheckFileExists");
        }

        /// <summary>
        /// Execute loads and returns the excel document.  
        /// Checking against total number of sheets as per the current "testDataDictionary.xlsx"
        /// </summary>
        [TestMethod]
        public void LoadExcelFileService_Execute_LoadsAndReturnsExcelDocument()
        {
            // Arrange 
            _mockIFileExists
                .Setup(s => s.CheckFileExists(It.IsAny<string>()))
                .Returns(true);

            // Act
            var results = _biz.Execute();

            // Assert
            Assert.IsInstanceOfType(results, typeof(XLWorkbook), nameof(results));
            Assert.AreEqual(2, results.Worksheets.Count, "worksheet count");
            _mockIFileExists.Verify(v => v.CheckFileExists(It.IsAny<string>()), Times.Once, "CheckFileExists");
        }
        #endregion Public methods/tests
    }
}
