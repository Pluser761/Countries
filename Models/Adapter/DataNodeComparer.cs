using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountryParse.Models.Adapter
{
    class DataNodeComparer : IEqualityComparer<DataNode>
    {
        public bool Equals(DataNode x, DataNode y)
        {
            return x.Name.Equals(y.Name);
        }

        public int GetHashCode(DataNode obj)
        {
            return obj.Name.GetHashCode();
        }
    }
}
