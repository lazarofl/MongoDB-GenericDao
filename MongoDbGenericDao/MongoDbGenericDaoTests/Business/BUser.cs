using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDbGenericDaoTests.Models;
using MongoDbGenericDao;

namespace MongoDbGenericDaoTests.Business
{
    public class BUser : MongoDBGenericDao<User>, IBUser
    {
        public BUser(string connectionstring)
            : base(connectionstring)
        {

        }
    }
}
