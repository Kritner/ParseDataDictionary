using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using ClosedXML.Excel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParseDataDictionary.Business.Services;

namespace ParseDataDictionary.Business.Tests.Services
{

    /// <summary>
    /// Tests for ExcelDataDictionaryParserService
    /// </summary>
    /// <remarks>
    /// Tests rely on specific data within testDataDictionary.xlsx
    /// </remarks>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ExcelDataDictionaryParserServiceTests
    {

        #region const
        const string _TABLE_NAME_COLUMN_ROW = "B4";
        const string _TABLE_DESCRIPTION_COLUMN_ROW = "B5";
        const int _TABLE_COLUMNS_START_ROW = 7;
        const string _TABLE_COLUMN_NAME_COLUMN_POSITION = "A";
        const string _TABLE_COLUMN_DESCRIPTION_COLUMN_POSITION = "B";

        const string _TEST_FILE = @"testDataDictionary.xlsx";
        #endregion const

        #region Private
        XLWorkbook _workbook;
        ExcelDataDictionaryParserService _biz;
        #endregion Private

        #region Setup
        /// <summary>
        /// Test initializer
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _workbook = new XLWorkbook();

            _biz = new ExcelDataDictionaryParserService();
        }
        #endregion Setup

        #region Public methods/tests
        /// <summary>
        /// Confirms the property values as per the writing of the classes.
        /// If these values in the implementation change, it will likely break other tests.
        /// Asserting these values are as expected
        /// </summary>
        [TestMethod]
        public void ExcelDataDictionaryParserService_ConfirmPropertyValues()
        {
            // Assert
            Assert.AreEqual(_TABLE_COLUMNS_START_ROW, _biz.TableColumnsStartRow, nameof(_biz.TableColumnsStartRow));
            Assert.AreEqual(_TABLE_COLUMN_DESCRIPTION_COLUMN_POSITION, _biz.TableColumnDescriptionColumnPosition, nameof(_biz.TableColumnDescriptionColumnPosition));
            Assert.AreEqual(_TABLE_COLUMN_NAME_COLUMN_POSITION, _biz.TableColumnNameColumnPosition, nameof(_biz.TableColumnNameColumnPosition));
            Assert.AreEqual(_TABLE_DESCRIPTION_COLUMN_ROW, _biz.TableDescriptionColumnRow, nameof(_biz.TableDescriptionColumnRow));
            Assert.AreEqual(_TABLE_NAME_COLUMN_ROW, _biz.TableNameColumnRow, nameof(_biz.TableNameColumnRow));
        }

        /// <summary>
        /// Tests the <see cref="Business.Models.Table"/> and <see cref="Business.Models.TableColumn"/> creation based on a <see cref="XLWorkbook"/>.
        /// </summary>
        /// <remarks>I was having trouble successfully mocking rows in the XLWorkbook/IXLWorksheets, 
        /// so I'm using the literal implementation which was unit tested separately.</remarks>
        [TestMethod]
        public void ExcelDataDictionaryParserService_ValidateTableAndTableColumnObjectCreation()
        {
            // Arrange
            LoadExcelFileService fileService = new LoadExcelFileService(new FileExistsService(), _TEST_FILE);
            var testFile = fileService.Execute();
            int lastRowIndex = 0;

            // Act
            var results = _biz.ParseDocumentIntoModel(testFile);
            lastRowIndex = results.ToList()[1].TableColumns.Count()-1;

            // Assert
            Assert.AreEqual(2, results.Count(), "results count");

            // Table check
            Assert.AreEqual("Table_Definition", results.ToList()[1].TableName, "TableName");
            Assert.AreEqual(" Defines tables being audited", results.ToList()[1].TableDescription, "TableDescription");

            // Columns check
            // First column
            Assert.AreEqual("Table_ID", results.ToList()[1].TableColumns.ToList()[0].TableColumnName, "TableColumnName");
            Assert.AreEqual("Identifies the database table", results.ToList()[1].TableColumns.ToList()[0].TableColumnDescription, "TableColumnDescription");
            
            Assert.AreEqual("Table_Form", results.ToList()[1].TableColumns.ToList()[lastRowIndex].TableColumnName, "TableColumnName");
            Assert.AreEqual("Form in which table is related to", results.ToList()[1].TableColumns.ToList()[lastRowIndex].TableColumnDescription, "TableColumnDescription");
        }
        #endregion Public methods/tests

    }
}
