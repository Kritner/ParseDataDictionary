using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParseDataDictionary.Business.Models;

namespace ParseDataDictionary.Business.Tests.Models
{

    /// <summary>
    /// Tests for Table
    /// </summary>
    [TestClass]
    public class TableTests
    {
        #region const
        const string _TABLE_NAME = "TableA";
        const string _TABLE_DESCRIPTION = "Table description";
        #endregion const

        #region private
        private List<TableColumn> _tableColumns;
        private Table _biz;
        #endregion private

        #region Setup
        /// <summary>
        /// Test initializer
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _tableColumns = new List<TableColumn>()
            {
                new TableColumn(_TABLE_NAME, _TABLE_DESCRIPTION)
            };
            _biz = new Table(_TABLE_NAME, _TABLE_DESCRIPTION, _tableColumns);
        }
        #endregion Setup

        #region Public methods/tests
        /// <summary>
        /// ArgumentNullException thrown when TableName not provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Table_ctor_ArgumentNullExceptionThrownWhenTableNameNotProvided()
        {
            // Arrange / Act / Assert
            _biz = new Table(null, _TABLE_DESCRIPTION, _tableColumns);
        }

        /// <summary>
        /// ArgumentNullException thrown when TableDescription not provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Table_ctor_ArgumentNullExceptionThrownWhenTableDescriptionNotProvided()
        {
            // Arrange / Act / Assert
            _biz = new Table(_TABLE_NAME, null, _tableColumns);
        }

        /// <summary>
        /// ArgumentNullException thrown when TableDescription not provided
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Table_ctor_ArgumentNullExceptionThrownWhenTableColumnsCountIsZero()
        {
            // Arrange / Act / Assert
            _biz = new Table(_TABLE_NAME, _TABLE_DESCRIPTION, new List<TableColumn>());
        }

        /// <summary>
        /// Tests parameters become properties
        /// </summary>
        [TestMethod]
        public void Table_ctor_ParametersBecomeProperties()
        {
            // Assert
            Assert.IsInstanceOfType(_biz, typeof(Table), nameof(_biz));
            Assert.AreEqual(_TABLE_NAME, _biz.TableName, nameof(_biz.TableName));
            Assert.AreEqual(_TABLE_DESCRIPTION, _biz.TableDescription, nameof(_biz.TableDescription));
            Assert.AreEqual(_tableColumns, _biz.TableColumns, nameof(_biz.TableColumns));
        }
        #endregion Public methods/tests
    }
}
