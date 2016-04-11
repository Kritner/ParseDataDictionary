using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParseDataDictionary.Business.Services;

namespace ParseDataDictionary.Business.Tests.Services
{

    /// <summary>
    /// Tests for FileExistsService
    /// </summary>
    [TestClass]
    public class FileExistsServiceTests
    {
        #region Private
        private string _fileName;
        private FileExistsService _biz;
        #endregion Private

        #region Setup
        /// <summary>
        /// Test initializer
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            _fileName = @"testDataDictionary.xlsx";
            _biz = new FileExistsService();
        }
        #endregion Setup

        #region Public methods/tests
        /// <summary>
        /// CheckFileExists returns true when the file exists
        /// </summary>
        [TestMethod]
        public void FileExistsService_CheckFileExists_True()
        {
            // Arrange / Act
            var results = _biz.CheckFileExists(_fileName);

            // Assert
            Assert.IsTrue(results);
        }

        /// <summary>
        /// CheckFileExists returns false when the file does not exist
        /// </summary>
        [TestMethod]
        public void FileExistsService_CheckFileExists_False()
        {
            // Arrange / Act
            var results = _biz.CheckFileExists(_fileName+"notValid");

            // Assert
            Assert.IsFalse(results);
        }
        #endregion Public methods/tests
    }
}
