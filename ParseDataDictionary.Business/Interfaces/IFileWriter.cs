using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDataDictionary.Business.Interfaces
{
    /// <summary>
    /// Interface for writing files
    /// </summary>
    public interface IFileWriter
    {
        /// <summary>
        /// Write the file
        /// </summary>
        /// <param name="fileName">The filename to create</param>
        /// <param name="sqlStatements">The sql statements to insert into the file</param>
        void WriteFile(string fileName, IEnumerable<string> sqlStatements);
    }
}
