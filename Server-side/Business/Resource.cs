using System;
using System.Collections.Generic;
using System.Linq;

namespace Business
{
    public class Resource : IResource<IEnumerable<string>>
    {
        public IEnumerable<string> FecthResource()
        {
            return Dictionaries.ghostDict.Split('\n', StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
