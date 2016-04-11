using System.Collections.Generic;
using ParseDataDictionary.Business.Models;

namespace ParseDataDictionary.Business.Interfaces
{
    
    /// <summary>
    /// Interface for Generating SQL scripts based on an IEnumerable of Table
    /// </summary>
    public interface IGenerateSqlScriptsForExtendedProperties
    {

        /// <summary>
        /// Generates SQL scripts
        /// </summary>
        /// <param name="tables">The IEnumerable of Table to generate scripts for</param>
        /// <returns>List of string</returns>
        List<string> GetSqlScripts(IEnumerable<Table> tables);
    }
}