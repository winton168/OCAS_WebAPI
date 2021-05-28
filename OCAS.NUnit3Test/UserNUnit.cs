using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using NUnit.Framework;
using OCAS.Core;
using OCAS.DataAccess;
using OCAS.WebAPI;

namespace OCAS.NUnit3Test
{
    public class UserNUnit
    {
        private static DbContextOptions<OCASContext> dbContextOptions = new DbContextOptionsBuilder<OCASContext>().UseSqlServer(@"Server=.;Database=OCAS;Trusted_Connection=True;MultipleActiveResultSets=true").Options;

        OCASContext context;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new OCASContext(dbContextOptions);
        }



        [Test]
        public void TestUser()
        {
            User item = context.Users.Find(1);

            // First Record in the Database Table Activity
            Assert.AreEqual(item.EmailAddress, "winton_wang@yahoo.com");
        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Dispose();
        }

    }
}