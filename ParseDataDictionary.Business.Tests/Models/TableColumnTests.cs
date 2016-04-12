using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParseDataDictionary.Business.Models;

namespace ParseDataDictionary.Business.Tests.Models
{

    /// <summary>
    /// Tests for TableColumn
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TableColumnTests
    {

        #region const
        const string _TABLE_COLUMN_NAME = "ColumnName";
        const string _TABLE_COLUMN_DESCRIPTION = "Column description";
        #endregion const

        #region private
        private TableColumn _biz;
        #endregion private

        #region Setup
        /// <summary>
        /// Test initializer
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _biz = new TableColumn(_TABLE_COLUMN_NAME, _TABLE_COLUMN_DESCRIPTION);
        }
        #endregion Setup

        #region Public methods/tests
        /// <summary>
        /// ArgumentNullException is thrown when the ColumnName is not provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableColumn_ctor_ArgumentNullExceptionThrownWhenColumnNameNotProvided()
        {
            // Arrange / Act / Assert
            _biz = new TableColumn(null, _TABLE_COLUMN_DESCRIPTION);
        }

        /// <summary>
        /// ArgumentNullException is thrown when the ColumnDescription is not provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TableColumn_ctor_ArgumentNullExceptionThrownWhenColumnDescriptionNotProvided()
        {
            // Arrange / Act / Assert
            _biz = new TableColumn(_TABLE_COLUMN_NAME, null);
        }

        /// <summary>
        /// Tests that parameters in the ctor become properties
        /// </summary>
        [TestMethod]
        public void TableColumn_ctor_ParametersBecomeProperties()
        {
            // Assert
            Assert.AreEqual(_TABLE_COLUMN_NAME, _biz.TableColumnName, nameof(_biz.TableColumnName));
            Assert.AreEqual(_TABLE_COLUMN_DESCRIPTION, _biz.TableColumnDescription, nameof(_biz.TableColumnDescription));
        }
        #endregion Public methods/tests
    }
}
