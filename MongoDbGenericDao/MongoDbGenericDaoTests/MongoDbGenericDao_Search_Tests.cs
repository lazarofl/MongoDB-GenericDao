using MongoDbGenericDaoTests.Business;
using MongoDbGenericDaoTests.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace MongoDbGenericDaoTests
{
    [TestFixture]
    public class MongoDbGenericDao_Search_Tests
    {
        private string mongodbserver = "server=192.168.1.15";
        private string mongodbdatabase = "database=tests_mongodb";
        public IBUser IBUser = null;

        public MongoDbGenericDao_Search_Tests()
        {
            IBUser = new BUser(string.Concat(mongodbserver, ";", mongodbdatabase));
        }

        [SetUp]
        public void AddUsers()
        {
            foreach (var item in IBUser.GetAll())
                IBUser.Delete(item);

            IBUser.Save(new User { Name = "Mercia Rocky", Email = "merciarocky@email.com" });
            IBUser.Save(new User { Name = "Herberto Marina", Email = "herbertomarina@email.com" });
            IBUser.Save(new User { Name = "Vicky Peers", Email = "vickypeers@email.com" });
            IBUser.Save(new User { Name = "Luanna Anjelica", Email = "luannaanjelica@email.com" });
            IBUser.Save(new User { Name = "Luanna Serveros", Email = "luannaserveros@email.com" });
            IBUser.Save(new User { Name = "Fernão Doroteia", Email = "fernãodoroteia@email.com" });
            IBUser.Save(new User { Name = "Chica Ciríaco", Email = "chicaciríaco@email.com" });
            IBUser.Save(new User { Name = "Bristol Judite", Email = "bristoljudite@email.com" });
            IBUser.Save(new User { Name = "Tammie Duane", Email = "tammieduane@email.com" });
            IBUser.Save(new User { Name = "Francisco Duda", Email = "franciscoduda@email.com" });
            IBUser.Save(new User { Name = "Zita Jemmy", Email = "zitajemmy@email.com" });
            IBUser.Save(new User { Name = "Charisma Cydney", Email = "charismacydney@email.com" });
        }

        [Test]
        public void Search_FirstName_Chica_Test()
        {
            long totalrecords;
            var results = IBUser.Search("Name", "Chica", 1, 10, out totalrecords);

            Assert.AreEqual(1, totalrecords);
        }

        [Test]
        public void Search_LastName_TammieDuane_Test()
        {
            long totalrecords;
            var results = IBUser.Search("Name", "Duane", 1, 10, out totalrecords);

            Assert.AreEqual(1, totalrecords);
        }

        [Test]
        public void Search_FullName_BristolJudite_Test()
        {
            long totalrecords;
            var results = IBUser.Search("Name", "Bristol Judite", 1, 10, out totalrecords);

            Assert.AreEqual(1, totalrecords);
        }

        [Test]
        public void Search_FirstName_Accent_Test()
        {
            long totalrecords;
            var results = IBUser.Search("Name", "Fernão", 1, 10, out totalrecords);

            Assert.AreEqual(1, totalrecords);
        }

        [Test]
        public void Search_LastName_Accent_Test()
        {
            long totalrecords;
            var results = IBUser.Search("Name", "Ciríaco", 1, 10, out totalrecords);

            Assert.AreEqual(1, totalrecords);
        }

        [Test]
        public void Search_Luanna_UpperCase_Test()
        {
            long totalrecords;
            var results = IBUser.Search("Name", "Luanna", 1, 10, out totalrecords);

            Assert.AreEqual(2, totalrecords);
        }

        [Test]
        public void Search_Luanna_DownCase_Test()
        {
            long totalrecords;
            var results = IBUser.Search("Name", "luanna", 1, 10, out totalrecords);

            Assert.AreEqual(2, totalrecords);
        }

        [Test]
        public void Search_NonExistent_User_Test()
        {
            long totalrecords;
            var results = IBUser.Search("Name", "Lazaro", 1, 10, out totalrecords);

            Assert.AreEqual(0, totalrecords);
        }

        [Test]
        public void Search_Email_Test()
        {
            long totalrecords;
            var results = IBUser.Search("Email", "zitajemmy@email.com", 1, 10, out totalrecords);

            Assert.AreEqual(1, totalrecords);
        }

        [Test]
        public void Search_Email_Total_Test()
        {
            long totalrecords;
            var results = IBUser.Search("Email", "@email.com", 1, 10, out totalrecords);

            Assert.AreEqual(12, totalrecords);
        }

        [Test]
        [ExpectedException]
        public void Search_Email_and_Name_BristolJudite_Exception_Test()
        {
            long totalrecords;
            var results = IBUser.Search("Email,Name", "judite@email", 1, 10, out totalrecords);

            Assert.AreEqual(12, totalrecords);
        }


        [Test]
        public void Search_And_Clause_Test()
        {
            long totalrecords;

            var search = new Dictionary<string, string>();
            search.Add("Name", "Mercia Rocky");
            search.Add("Email", "merciarocky@email.com");

            var results = IBUser.Search_And(search, 1, 10, out totalrecords);

            Assert.AreEqual(1, totalrecords);
        }

        [Test]
        public void Search_Or_Clause_Test()
        {
            long totalrecords;

            var search = new Dictionary<string, string>();
            search.Add("Name", "Bristol Judite");
            search.Add("Email", "merciarocky@email.com");

            var results = IBUser.Search_Or(search, 1, 10, out totalrecords);

            Assert.AreEqual(2, totalrecords);
        }

    }
}
