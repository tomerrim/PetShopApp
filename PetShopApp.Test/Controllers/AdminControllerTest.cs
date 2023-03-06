using Castle.Core.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PetShop.DataAccess.Data.Repository.IRepository;
using PetShop.Models;
using PetShopApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetShopApp.Test.Controllers
{
    [TestClass]
    public class AdminControllerTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IWebHostEnvironment> _mockWebHostEnvironment;
        private readonly Mock<ILogger<AdminController>> _mockLogger;
        private readonly AdminController _controller;

        public AdminControllerTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            _mockLogger = new Mock<ILogger<AdminController>>();
            _controller = new AdminController(_mockUnitOfWork.Object, _mockWebHostEnvironment.Object,_mockLogger.Object);
        }

        [TestMethod]
        public void Index_ReturnsViewResult()
        {
            // Arrange
            var expectedAnimals = new List<Animal>();
            _mockUnitOfWork.Setup(u => u.Animals.GetAll()).Returns(expectedAnimals);

            // Act
            var result = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedAnimals, result.Model);
        }

        [TestMethod]
        public void Delete_WithValidId_ReturnsViewResult()
        {
            // Arrange
            var animals = new List<Animal>
            {
                new Animal { AnimalId = 1 },
                new Animal { AnimalId = 2 },
                new Animal { AnimalId = 3 }
            };
            var expectedAnimal = animals[1];
            _mockUnitOfWork.Setup(u => u.Animals.GetAll()).Returns(animals.AsQueryable().ToList());
            _mockUnitOfWork.Setup(u => u.Animals.GetFirstOrDefault(a => a.AnimalId == expectedAnimal.AnimalId)).Returns(expectedAnimal);

            // Act
            var result = _controller.Delete(expectedAnimal.AnimalId) as ViewResult;
            var index = _controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(animals);
            Assert.AreEqual(animals, index.Model);
        }

        [TestMethod]
        public void Delete_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidId = 0;
            _mockUnitOfWork.Setup(u => u.Animals.GetFirstOrDefault(a => a.AnimalId == invalidId)).Returns((Animal)null);

            // Act
            var result = _controller.Delete(invalidId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeletePOST_WithValidId_DeletesAnimalAndRedirectsToIndex()
        {
            // Arrange
            var expectedAnimal = new Animal { AnimalId = 1 };
            _mockUnitOfWork.Setup(u => u.Animals.GetFirstOrDefault(a => a.AnimalId == expectedAnimal.AnimalId)).Returns(expectedAnimal);

            // Act
            var result = _controller.DeletePOST(expectedAnimal.AnimalId) as RedirectToActionResult;

            // Assert
            _mockUnitOfWork.Verify(u => u.Animals.Remove(expectedAnimal), Times.Once);
            _mockUnitOfWork.Verify(u => u.Save(), Times.Once);
            Assert.AreEqual("Index", result?.ActionName);
        }

        [TestMethod]
        public void DeletePOST_WithInvalidId_ReturnsNotFoundResult()
        {
            // Arrange
            var invalidId = 0;
            _mockUnitOfWork.Setup(u => u.Animals.GetFirstOrDefault(a => a.AnimalId == invalidId)).Returns((Animal)null);

            // Act
            var result = _controller.DeletePOST(invalidId);

            // Assert
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void Upsert_WithoutId_ReturnsViewResult()
        {
            // Arrange
            var expectedCategories = new List<Category>();
            _mockUnitOfWork.Setup(u => u.Categories.GetAll()).Returns(expectedCategories);

            // Act
            var result = _controller.Upsert(null) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            var model = result.Model as AnimalVM;
            Assert.IsNotNull(model);
            Assert.IsNotNull(model.Animal);
            Assert.AreEqual(expectedCategories.Count, model.CategoryList.Count());
        }

        [TestMethod]
        public void Upsert_WithValidId_ReturnsViewResult()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockWebHostEnvironment = new Mock<IWebHostEnvironment>();
            var mockLogger = new Mock<ILogger<AdminController>>();
            var controller = new AdminController(mockUnitOfWork.Object, mockWebHostEnvironment.Object,mockLogger.Object);
            var animal = new Animal { AnimalId = 1, Name = "Test Animal", CategoryId = 1 };
            mockUnitOfWork.Setup(uow => uow.Animals.GetFirstOrDefault(It.IsAny<Expression<Func<Animal, bool>>>())).Returns(animal);

            // Act
            var result = controller.Upsert(1) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(animal, result.Model);
        }
    }
}
