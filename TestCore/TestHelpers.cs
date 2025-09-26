using Grocery.Core.Helpers;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using Grocery.Core.Services;

namespace TestCore
{
    public class TestHelpers
    {
        [SetUp]
        public void Setup()
        { 
        }

        //Happy flow
        [Test]
        public void TestPasswordHelperReturnsTrue()
        {
            string password = "user3";
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            Assert.IsTrue(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        public void TestPasswordHelperReturnsTrue(string password, string passwordHash)
        {
            Assert.IsTrue(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        //Unhappy flow
        [Test]
        public void TestPasswordHelperReturnsFalse()
        {
            string password = "user2";
            string passwordHash = "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=";
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        [TestCase("user2", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=")]
        [TestCase("user2", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=")]
        public void TestPasswordHelperReturnsFalse(string password, string passwordHash)
        {
            Assert.IsFalse(PasswordHelper.VerifyPassword(password, passwordHash));
        }

        // Mock ClientService for testing
        public class TestClientService : IClientService
        {
            private readonly List<Client> _clients = new();

            public Client? Get(string email) => _clients.FirstOrDefault(c => c.EmailAddress == email);
            public Client? Get(int id) => _clients.FirstOrDefault(c => c.Id == id);
            public List<Client> GetAll() => _clients;
            public Client? Add(Client client) 
            {
                _clients.Add(client);
                return client;
            }

            // Helper method for tests
            public void AddTestClient(Client client) => _clients.Add(client);
        }


        [Test]

        // Happy flow
        public void TestRegistration_ExistingEmail_ReturnsNotNull()
        {
            string name = "Existing User";
            string email = "user3@mail.com";
            string password = "Password123!";

            var testService = new TestClientService();
            // Add an existing user to simulate the scenario
            testService.AddTestClient(new Client(1, "Existing", email, "existingPassword"));

            email = email + "unique";

            var registerHelper = new RegisterHelper(testService);
            Assert.That(registerHelper.RegisterClient(name, email, password), Is.Not.Null);
        }
        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=", "user1@mail.com")]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=", "user3@mail.com")]
        public void TestRegistration_ExistingEmail_ReturnsNotNull(string name, string password, string email)
        {
            var testService = new TestClientService();
            // Add an existing user to simulate the scenario
            testService.AddTestClient(new Client(1, name, email, password));

            email = email + "unique";

            var registerHelper = new RegisterHelper(testService);
            Assert.That(registerHelper.RegisterClient(name, email, password), Is.Not.Null);
        }


        [Test]

        // Unhappy flow
        public void TestRegistration_ExistingEmail_ReturnsNull()
        {
            string name = "Existing User";
            string email = "user3@mail.com";
            string password = "Password123!";

            var testService = new TestClientService();
            // Add an existing user to simulate the scenario
            testService.AddTestClient(new Client(1, "Existing", email, "existingPassword"));

            var registerHelper = new RegisterHelper(testService);
            Assert.That(registerHelper.RegisterClient(name, email, password), Is.Null);
        }
        [TestCase("user1", "IunRhDKa+fWo8+4/Qfj7Pg==.kDxZnUQHCZun6gLIE6d9oeULLRIuRmxmH2QKJv2IM08=", "user1@mail.com")]
        [TestCase("user3", "sxnIcZdYt8wC8MYWcQVQjQ==.FKd5Z/jwxPv3a63lX+uvQ0+P7EuNYZybvkmdhbnkIHA=", "user3@mail.com")]
        public void TestRegistration_ExistingEmail_ReturnsNull(string name, string password, string email)
        {
            var testService = new TestClientService();
            // Add an existing user to simulate the scenario
            testService.AddTestClient(new Client(1, name, email, password));

            var registerHelper = new RegisterHelper(testService);
            Assert.That(registerHelper.RegisterClient(name, email, password), Is.Null);
        }
    }
}