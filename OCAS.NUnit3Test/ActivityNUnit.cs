using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using NUnit.Framework;
using OCAS.Core;
using OCAS.Core.IRepository;
using OCAS.Core.Repository;
using OCAS.DataAccess;
using OCAS.WebAPI;
using System.Collections.Generic;

namespace OCAS.NUnit3Test
{
    public class ActivityNUnit
    {
        private static DbContextOptions<OCASContext> dbContextOptions = new DbContextOptionsBuilder<OCASContext>().UseSqlServer(@"Server=.;Database=OCAS;Trusted_Connection=True;MultipleActiveResultSets=true").Options;

        OCASContext context;

     


        [OneTimeSetUp]
        public void Setup()
        {
            context = new OCASContext(dbContextOptions);

          
        }



        [Test]
        public  void TestActivity()
        {
            Activity item = context.Activities.Find(1);

            // First Record in the Database Table Activity
            Assert.AreEqual(item.ActivityName, "Christmas Party");
        }


        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Dispose();
        }

    }
}