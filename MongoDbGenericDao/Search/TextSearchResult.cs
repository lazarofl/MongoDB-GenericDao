using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoDbGenericDao.Search
{
    public class TextSearchResult<T>
    {
        public T obj { get; set; }
        public double score { get; set; }
    }
}
