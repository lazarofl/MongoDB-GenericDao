using MongoDbGenericDaoTests.Models;

namespace MongoDbGenericDaoTests.Business
{
    public interface IBUser : MongoDbGenericDao.Interfaces.IDao<User, string>
    {
    }
}
