using MongoDbGenericDaoTests.Business;
using MongoDbGenericDaoTests.Models;
using NUnit.Framework;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace MongoDbGenericDaoTests
{
    [TestFixture]
    public class MongoDbGenericDao_Search_Tests
    {
        //private string mongodbserver = "mongodb://localhost/tests_mongodb";
        private string mongodbserver = "mongodb://admin:qB4EZRVw@SG-condomundo-4035.servers.mongodirector.com/admin";

        public IBUser IBUser = null;

        public MongoDbGenericDao_Search_Tests()
        {
            IBUser = new BUser(mongodbserver);
        }

        [SetUp]
        public void AddUsers()
        {
            foreach (var item in IBUser.GetAll())
                IBUser.Delete(item);

            IBUser.Save(new User { Type = 1, Name = "Mercia Rocky", Email = "merciarocky@email.com" });
            IBUser.Save(new User { Type = 1, Name = "Herberto Marina", Email = "herbertomarina@email.com" });
            IBUser.Save(new User { Type = 1, Name = "Vicky Peers", Email = "vickypeers@email.com" });
            IBUser.Save(new User { Type = 1, Name = "Luanna Anjelica", Email = "luannaanjelica@email.com" });
            IBUser.Save(new User { Type = 1, Name = "Luanna Serveros", Email = "luannaserveros@email.com" });
            IBUser.Save(new User { Type = 1, Name = "Fernão Doroteia", Email = "fernãodoroteia@email.com" });
            IBUser.Save(new User { Type = 2, Name = "Chica Ciríaco", Email = "chicaciríaco@email.com" });
            IBUser.Save(new User { Type = 2, Name = "Bristol Judite", Email = "bristoljudite@email.com" });
            IBUser.Save(new User { Type = 2, Name = "Tammie Duane", Email = "tammieduane@email.com" });
            IBUser.Save(new User { Type = 2, Name = "Francisco Duda", Email = "franciscoduda@email.com" });
            IBUser.Save(new User { Type = 2, Name = "Zita Jemmy", Email = "zitajemmy@email.com" });
            IBUser.Save(new User { Type = 2, Name = "Charisma Cydney", Email = "charismacydney@email.com" });
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
        public void Search_Email_and_Name_BristolJudite_Test()
        {
            long totalrecords;
            var results = IBUser.Search("Email,Name", "judite@email", 1, 10, out totalrecords);

            Assert.AreEqual(0, totalrecords);
        }


        [Test]
        public void Search_And_Clause_Test()
        {
            long totalrecords;

            IDictionary<string, string> search = new Dictionary<string, string>();
            search.Add("Name", "Mercia Rocky");
            search.Add("Email", "merciarocky@email.com");

            var results = IBUser.Search_And<User>(search, 1, 10, out totalrecords, null, false);

            Assert.AreEqual(1, totalrecords);
        }

        [Test]
        public void Search_And_Clause_WithMongoQueriesTest()
        {
            long totalrecords;

            var search = new List<IMongoQuery>();
            search.Add(Query.EQ("Type", new BsonInt32(1)));
            search.Add(Query.Matches("Name", new BsonRegularExpression("Mercia Rocky", "i")));
            search.Add(Query.Matches("Email", new BsonRegularExpression("merciarocky@email.com", "i")));

            var results = IBUser.Search_And<User>(search, 1, 10, out totalrecords);

            Assert.AreEqual(1, totalrecords);
        }

        [Test]
        public void Search_Or_Clause_Test()
        {
            long totalrecords;

            var search = new Dictionary<string, string>();
            search.Add("Name", "Bristol Judite");
            search.Add("Email", "merciarocky@email.com");

            var results = IBUser.Search_Or<User>(search, 1, 10, out totalrecords);

            Assert.AreEqual(2, totalrecords);
        }

    }
}
