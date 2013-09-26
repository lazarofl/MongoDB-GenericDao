using System;

namespace MongoDbGenericDao.Tests.Models
{
    public class User : MongoDBEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
