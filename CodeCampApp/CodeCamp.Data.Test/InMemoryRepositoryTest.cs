using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Data = CodeCamp.Data;
using Domain = CodeCamp.Domain;

namespace CodeCamp.Data.Test
{
    [TestClass]
    public class InMemoryRepositoryTest
    {
        private Domain.Repositories.ICampRepository _repository; 

        [TestInitialize]
        public void Initialize()
        {
           _repository = new Data.Repositories.InMemoryCampRepository();
        }

        [TestCleanup]
        public void CleanUp()
        {
            // nothing
        }

        [TestMethod]
        public void GetAllCampsTest()
        {
            // Arrange
            // Crete a data repository (at initialization)

            // Act
            IEnumerable<Domain.Entities.Camp> camps = _repository.GetAllCamps();

            // Assert
            Assert.IsNotNull(camps);
            Assert.AreEqual(2, camps.ToList().Count);

        }
        [TestMethod]
        public void GetAllCampsByNameTest()
        {
            // Arrange
            // Crete a data repository (at initialization)

            // Act
            IEnumerable<Domain.Entities.Camp> camps = _repository.GetAllCamps("UCI");

            // Assert
            Assert.IsNotNull(camps);
            Assert.AreEqual(1, camps.ToList().Count);

        }






    }
}
