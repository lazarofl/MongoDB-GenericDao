using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

namespace MongoDbGenericDao.Search
{
    public class TextSearchCommandResult<T> : CommandResult
    {
        public TextSearchCommandResult(BsonDocument response) : base(response) { }

        public IEnumerable<TextSearchResult<T>> Results
        {
            get
            {
                var results = this.Response["results"].AsBsonArray.Select(row => row.AsBsonDocument);
                var resultObjects = results.Select(item => item.AsBsonDocument);

                return resultObjects.Select(row => BsonSerializer.Deserialize<TextSearchResult<T>>(row));
            }
        }

    }

}
