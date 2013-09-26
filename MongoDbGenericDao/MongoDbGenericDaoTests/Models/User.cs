using System;
using MongoDbGenericDao;

namespace MongoDbGenericDaoTests.Models
{
    public class User : MongoDBEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
