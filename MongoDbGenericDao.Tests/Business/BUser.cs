using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDbGenericDao.Tests.Models;

namespace MongoDbGenericDao.Tests.Business
{
    public class BUser : MongoDBGenericDao<User>, IBUser
    {
        public BUser(string connectionstring)
            : base(connectionstring)
        {

        }
    }
}
