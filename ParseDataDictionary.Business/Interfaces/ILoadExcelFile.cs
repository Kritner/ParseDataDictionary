using ClosedXML.Excel;

namespace ParseDataDictionary.Business.Interfaces
{

    /// <summary>
    /// Interface for loading an <seealso cref="XLWorkbook"/>
    /// </summary>
    public interface ILoadExcelFile
    {
        XLWorkbook Execute();
    }
}