using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ClosedXML.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParseDataDictionary.Business.Interfaces;
using ParseDataDictionary.Business.Models;
using ParseDataDictionary.Business.Services;

namespace ParseDataDictionary.Business.Tests.Services
{

    /// <summary>
    /// Tests for LoadParseExportDataDictionaryService
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class LoadParseExportDataDictionaryServiceTests
    {

        #region Private
        Mock<ILoadExcelFile> _mockILoadExcelFile;
        Mock<IExcelDataDictionaryParser> _mockIExcelDataDictionaryParser;
        Mock<IGenerateSqlScriptsForExtendedProperties> _mockIGenerateSqlScriptsForExtendedProperties;
        Mock<IFileWriter> _mockIFileWriter;
        LoadParseExportDataDictionaryService _biz;
        #endregion Private

        #region Setup
        /// <summary>
        /// Test initliazer
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _mockILoadExcelFile = new Mock<ILoadExcelFile>();
            _mockIExcelDataDictionaryParser = new Mock<IExcelDataDictionaryParser>();
            _mockIGenerateSqlScriptsForExtendedProperties = new Mock<IGenerateSqlScriptsForExtendedProperties>();
            _mockIFileWriter = new Mock<IFileWriter>();

            _biz = new LoadParseExportDataDictionaryService(
                _mockILoadExcelFile.Object,
                _mockIExcelDataDictionaryParser.Object,
                _mockIGenerateSqlScriptsForExtendedProperties.Object,
                _mockIFileWriter.Object
            );
        }
        #endregion Setup

        #region Public methods/tests
        /// <summary>
        /// Tests successful construction
        /// </summary>
        [TestMethod]
        public void LoadParseExportDataDictionaryService_ctor_ConstructedProperly()
        {
            // Assert
            Assert.IsInstanceOfType(_biz, typeof(LoadParseExportDataDictionaryService));
        }

        /// <summary>
        /// ArgumentNullException is thrown with no provided ILoadExcelFile
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadParseExportDataDictionaryService_ctor_ArgumentNullExceptionThrownWhenILoadExcelFileNotProvided()
        {
            // Arrange / Act / Assert
            _biz = new LoadParseExportDataDictionaryService(
                null,
                _mockIExcelDataDictionaryParser.Object,
                _mockIGenerateSqlScriptsForExtendedProperties.Object,
                _mockIFileWriter.Object
            );
        }

        /// <summary>
        /// ArgumentNullException is thrown with no provided IExcelDataDictionaryParser
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadParseExportDataDictionaryService_ctor_ArgumentNullExceptionThrownWhenIExcelDataDictionaryParserNotProvided()
        {
            // Arrange / Act / Assert
            _biz = new LoadParseExportDataDictionaryService(
                _mockILoadExcelFile.Object,
                null,
                _mockIGenerateSqlScriptsForExtendedProperties.Object,
                _mockIFileWriter.Object
            );
        }

        /// <summary>
        /// ArgumentNullException is thrown with no provided IGenerateSqlScriptsForExtendedProperties
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadParseExportDataDictionaryService_ctor_ArgumentNullExceptionThrownWhenIGenerateSqlScriptsForExtendedPropertiesNotProvided()
        {
            // Arrange / Act / Assert
            _biz = new LoadParseExportDataDictionaryService(
                _mockILoadExcelFile.Object,
                _mockIExcelDataDictionaryParser.Object,
                null,
                _mockIFileWriter.Object
            );
        }

        /// <summary>
        /// ArgumentNullException is thrown with no provided IFileWriter
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void LoadParseExportDataDictionaryService_ctor_ArgumentNullExceptionThrownWhenIFileWriterNotProvided()
        {
            // Arrange / Act / Assert
            _biz = new LoadParseExportDataDictionaryService(
                _mockILoadExcelFile.Object,
                _mockIExcelDataDictionaryParser.Object,
                _mockIGenerateSqlScriptsForExtendedProperties.Object,
                null
            );
        }

        /// <summary>
        /// Execute calls all dependencies
        /// </summary>
        [TestMethod]
        public void LoadParseExportDataDictionaryService_Execute_CallsDependencies()
        {
            // Arrange
            _mockILoadExcelFile
                .Setup(s => s.Execute())
                .Returns(It.IsAny<XLWorkbook>());
            _mockIExcelDataDictionaryParser
                .Setup(s => s.ParseDocumentIntoModel(It.IsAny<XLWorkbook>()))
                .Returns(It.IsAny<IEnumerable<Table>>());
            _mockIGenerateSqlScriptsForExtendedProperties
                .Setup(s => s.GetSqlScripts(It.IsAny<IEnumerable<Table>>()))
                .Returns(new List<string>());

            // Act
            var results = _biz.Execute();

            // Assert
            Assert.IsInstanceOfType(results, typeof(List<string>), "results type");
            _mockILoadExcelFile.Verify(v => v.Execute(), Times.Once, "ILoadExcelFile Execute");
            _mockIExcelDataDictionaryParser.Verify(v => v.ParseDocumentIntoModel(It.IsAny<XLWorkbook>()), Times.Once, "IExcelDataDictionaryParser ParseDocumentIntoModel");
            _mockIGenerateSqlScriptsForExtendedProperties.Verify(v => v.GetSqlScripts(It.IsAny<IEnumerable<Table>>()), Times.Once, "IGenerateSqlScriptsForExtendedProperties GetSqlScripts");
            _mockIFileWriter.Verify(v => v.WriteFile(It.IsAny<string>(), It.IsAny<IEnumerable<string>>()), Times.Once, "IFileWriter WriteFile");
        }
        #endregion Public methods/tests
    }
}
