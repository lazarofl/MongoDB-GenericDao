using MongoDbGenericDao.Tests.Models;

namespace MongoDbGenericDao.Tests.Business
{
    public interface IBUser : MongoDbGenericDao.Interfaces.IDao<User, string>
    {
    }
}
