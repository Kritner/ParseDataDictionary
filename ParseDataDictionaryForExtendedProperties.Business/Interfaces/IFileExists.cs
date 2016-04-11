using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParseDataDictionaryForExtendedProperties.Business.Interfaces
{
    public interface IFileExists
    {
        bool CheckFileExists(string fullFileNameAndPath);
    }
}
