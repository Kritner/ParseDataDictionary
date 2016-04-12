using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParseDataDictionary.Business.Services;
using System.Collections.Generic;
using ParseDataDictionary.Business.Models;

namespace ParseDataDictionary.Business.Tests.Services
{

    /// <summary>
    /// Tests for GenerateSqlScriptsForExtendedPropertiesService
    /// </summary>
    [TestClass]
    public class GenerateSqlScriptsForExtendedPropertiesServiceTests
    {
        #region const
        const string _TABLE_NAME = "MyTable";
        const string _TABLE_DESCRIPTION = "Table description";
        const string _TABLE_COLUMN_NAME = "Column";
        const string _TABLE_COLUMN_DESCRIPTION = "Column descriptions";
        #endregion const

        #region Private
        private List<Table> _table;
        private GenerateSqlScriptsForExtendedPropertiesService _biz;
        #endregion Private

        #region Setup
        /// <summary>
        /// Test initializer
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _biz = new GenerateSqlScriptsForExtendedPropertiesService();
            _table = new List<Business.Models.Table>()
            {
                new Table(
                    _TABLE_NAME,
                    _TABLE_DESCRIPTION,
                    new List<TableColumn>()
                    {
                        new TableColumn(
                            _TABLE_COLUMN_NAME,
                            _TABLE_COLUMN_DESCRIPTION
                        )
                    }
                )
            };
        }
        #endregion Setup

        #region Public methods/tests
        /// <summary>
        /// Tests GetSqlScripts and ensures the tables, description, and column information gets creates correctly.
        /// </summary>
        [TestMethod]
        public void GenerateSqlScriptsForExtendedPropertiesService_GetSqlScripts_ExecutesSuccessfully()
        {
            // Arrange
            int expectedCount = 3; // One table, one column, one comment

            // Act
            var results = _biz.GetSqlScripts(_table);

            // Assert
            Assert.AreEqual(expectedCount, results.Count, "results count");
            Assert.IsTrue(results.Contains(
                string.Format(
                    GenerateSqlScriptsForExtendedPropertiesService._GENERATING_COMMENT_FOR_TABLE,
                    _TABLE_NAME
                )
            ), "comment");
            Assert.IsTrue(results.Contains(
                string.Format(
                    GenerateSqlScriptsForExtendedPropertiesService._SCRIPT_TEMPLATE_FOR_TABLE,
                    _TABLE_NAME,
                    _TABLE_DESCRIPTION
                )
            ), "table");
            Assert.IsTrue(results.Contains(
                string.Format(
                    GenerateSqlScriptsForExtendedPropertiesService._SCRIPT_TEMPLATE_FOR_TABLE_COLUMN,
                    _TABLE_NAME,
                    _TABLE_COLUMN_NAME,
                    _TABLE_COLUMN_DESCRIPTION
                )
            ), "column");
        }
        #endregion Public methods/tests
    }
}
