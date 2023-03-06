using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PetShop.DataAccess.Data.Repository.IRepository;
using PetShop.Models;
using PetShopApp.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetShopApp.Test.Repositories;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;

namespace PetShopApp.Test.Controllers
{
    [TestClass]
    public class CatalogControllerTests
    {
        private CatalogController? _controller;
        private Mock<IUnitOfWork>? _unitOfWorkMock;
        private Mock<ILogger<CatalogController>>? _loggerMock;
        private List<Animal>? _animals;
        private List<Category>? _categories;

        [TestInitialize]
        public void TestInitialize()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILogger<CatalogController>>();
            _controller = new CatalogController(_unitOfWorkMock.Object,_loggerMock.Object);

            _animals = new List<Animal>
            {
                new Animal { AnimalId = 1, CategoryId = 1, Name = "Cat1" },
                new Animal { AnimalId = 2, CategoryId = 1, Name = "Cat2" },
                new Animal { AnimalId = 3, CategoryId = 2, Name = "Dog1" },
                new Animal { AnimalId = 4, CategoryId = 2, Name = "Dog2" }
            };

            _categories = new List<Category>
            {
                new Category { CategoryId = 1, Name = "Cats" },
                new Category { CategoryId = 2, Name = "Dogs" }
            };
        }

        [TestMethod]
        public void Index_ReturnsAViewResult_WithListOfAnimals()
        {
            // Arrange
            _unitOfWorkMock.Setup(x => x.Animals.GetAll()).Returns(_animals);
            _unitOfWorkMock.Setup(x => x.Categories.GetAll()).Returns(_categories);

            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            var viewResult = (ViewResult)result;
            var model = (IEnumerable<Animal>)viewResult.Model;
            Assert.AreEqual(4, model.Count());
        }

        //[TestMethod]
        //public void Index_Post_ReturnsAViewResult_WithListOfAnimalsByCategory()
        //{
        //    // Arrange
        //    _unitOfWorkMock.Setup(x => x.Animals.GetAll()).Returns(_animals);
        //    _unitOfWorkMock.Setup(x => x.Categories.GetAll()).Returns(_categories);
        //    _unitOfWorkMock.Setup(x => x.Animals.AnimalsByCategory(1)).Returns(_animals.Where(a => a.CategoryId == 1));

        //    // Act
        //    var result = _controller.Index(1);

        //    // Assert
        //    Assert.IsInstanceOfType(result, typeof(ViewResult));
        //    var viewResult = (ViewResult)result;
        //    var model = (IEnumerable<Animal>)viewResult.Model;
        //    Assert.Equals(2, model.Count());
        //    Assert.Equals(1, model.First().CategoryId);
        //}
    }
}
    

