using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MongoDbGenericDao.Tests.Business;

namespace MongoDbGenericDao.Tests
{
    [TestFixture]
    public class MongoDbGenericDaoTests
    {
        private string mongodbserver = "server=192.168.1.15";
        private string mongodbdatabase = "database=tests_mongodb";
        public IBUser IBUser = null;

        public MongoDbGenericDaoTests()
        {
            IBUser = new BUser(string.Concat(mongodbserver, ";", mongodbdatabase));
        }

        [SetUp]
        public void AddUsers()
        {
            IBUser.Save(new Models.User { Name = "Mercia Rocky", Email = "merciarocky@email.com" });
            IBUser.Save(new Models.User { Name = "Herberto Marina", Email = "herbertomarina@email.com" });
            IBUser.Save(new Models.User { Name = "Vicky Peers", Email = "vickypeers@email.com" });
            IBUser.Save(new Models.User { Name = "Luanna Anjelica", Email = "luannaanjelica@email.com" });
            IBUser.Save(new Models.User { Name = "Luanna Serveros", Email = "luannaserveros@email.com" });
            IBUser.Save(new Models.User { Name = "Fernão Doroteia", Email = "fernãodoroteia@email.com" });
            IBUser.Save(new Models.User { Name = "Chica Ciríaco", Email = "chicaciríaco@email.com" });
            IBUser.Save(new Models.User { Name = "Bristol Judite", Email = "bristoljudite@email.com" });
            IBUser.Save(new Models.User { Name = "Tammie Duane", Email = "tammieduane@email.com" });
            IBUser.Save(new Models.User { Name = "Francisco Duda", Email = "franciscoduda@email.com" });
            IBUser.Save(new Models.User { Name = "Zita Jemmy", Email = "zitajemmy@email.com" });
            IBUser.Save(new Models.User { Name = "Charisma Cydney", Email = "charismacydney@email.com" });
        }

        [TearDown]
        public void Dispose()
        {
            foreach (var item in IBUser.GetAll())
                IBUser.Delete(item);

            if (IBUser != null)
                IBUser = null;
        }

        [Test]
        public void TestOne()
        {
            long totalrecords;
            var results = IBUser.Search("Chica", 1, 10, out totalrecords, "Name");

            Assert.AreEqual(1, totalrecords);
        }
    }
}
