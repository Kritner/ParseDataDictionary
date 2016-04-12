using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParseDataDictionary.Business.Services;

namespace ParseDataDictionary.Business.Tests.Services
{

    /// <summary>
    /// Tests for SQLScriptFileWriterService
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SQLScriptFileWriterServiceTests
    {
        #region const
        const string _TEST_STRING = "this is a test";
        #endregion const

        #region Private
        SQLScriptFileWriterService _biz;
        #endregion Private

        #region Setup
        [TestInitialize]
        public void Setup()
        {
            _biz = new SQLScriptFileWriterService();
        }
        #endregion Setup

        #region Public methods/tests
        /// <summary>
        /// WriteFile creates a file with the specified filename, and populates it with the data in the list of string.
        /// </summary>
        [TestMethod]
        public void SQLScriptFileWriterService_WriteFile_CreatesFile()
        {
            // Arrange
            Guid fileGuid = Guid.NewGuid();
            string fileName = string.Format("{0}.txt", fileGuid);
            List<string> list = new List<string>();
            list.Add(_TEST_STRING);
            List<string> textFileList = new List<string>();
            string line;

            // Act
            _biz.WriteFile(fileName, list);
            using (StreamReader sr = new StreamReader(fileName))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    textFileList.Add(line);
                }
            }

            // Assert
            Assert.IsTrue(File.Exists(fileName), "File exists");
            Assert.AreEqual(list.Count, textFileList.Count, "File line count");
            Assert.AreEqual(list.First(), textFileList.First(), "File list contents");            
            
            // Cleanup
            File.Delete(fileName);
        }
        #endregion Public methods/tests
    }
}
