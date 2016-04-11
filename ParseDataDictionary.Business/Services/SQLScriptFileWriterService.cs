using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParseDataDictionary.Business.Interfaces;

namespace ParseDataDictionary.Business.Services
{
    /// <summary>
    /// Writes SQL scripts to a file
    /// </summary>
    public class SQLScriptFileWriterService : IFileWriter
    {

        /// <summary>
        /// Write the file
        /// </summary>
        /// <param name="fileName">The filename to create</param>
        /// <param name="sqlStatements">The sql statements to insert into the file</param>
        public void WriteFile(string fileName, IEnumerable<string> sqlStatements)
        {
            TextWriter tw = new StreamWriter(fileName);
            foreach (string s in sqlStatements)
                tw.WriteLine(s);
            tw.Close();
        }
    }
}
