using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
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
    public class HomeControllerTests
    {
        private HomeController _controller;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private List<Animal> _animals;
        private List<Comment> _comments;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _controller = new HomeController(null, _mockUnitOfWork.Object);

            _animals = new List<Animal>
            {
                new Animal { AnimalId = 1, Name = "Cat" },
                new Animal { AnimalId = 2, Name = "Dog" },
                new Animal { AnimalId = 3, Name = "Horse" }
            };

            _comments = new List<Comment>
            {
                new Comment { CommentId = 1, AnimalId = 1, CommentText = "Great animal!" },
                new Comment { CommentId = 2, AnimalId = 1, CommentText = "I love cats!" },
                new Comment { CommentId = 3, AnimalId = 2, CommentText = "Dogs are the best!" }
            };
        }

        [TestMethod]
        public void Index_ReturnsAViewResult_WithPopularAnimals()
        {
            // Arrange
            var mockAnimalRepository = new Mock<IAnimalRepository>();
            mockAnimalRepository.Setup(x => x.AnimalsMostCommented(2)).Returns((_animals.Take(2)).ToList());
            _mockUnitOfWork.Setup(x => x.Animals).Returns(mockAnimalRepository.Object);

            // Act
            var result = _controller.Index();

            // Assert
            var viewResult = result as ViewResult;
            var model = viewResult.Model as IEnumerable<Animal>;

            Assert.IsNotNull(viewResult);
            Assert.IsInstanceOfType(model, typeof(IEnumerable<Animal>));
            Assert.AreEqual(2, model.Count());
        }

        [TestMethod]
        public void Details_ReturnsAViewResult_WithAnimalAndComments()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockAnimalRepository = new Mock<IAnimalRepository>();
            var mockCommentRepository = new Mock<ICommentRepository>();

            mockUnitOfWork.SetupGet(uow => uow.Animals).Returns(mockAnimalRepository.Object);
            mockUnitOfWork.SetupGet(uow => uow.Comments).Returns(mockCommentRepository.Object);

            var animal = new Animal { AnimalId = 1, Name = "Lion", Description = "King of Jungle" };
            var comments = new List<Comment>
            {
                new Comment {CommentId = 1, CommentText = "Great animal", AnimalId = 1},
                new Comment {CommentId = 2, CommentText = "Awesome", AnimalId = 1},
                new Comment {CommentId = 3, CommentText = "Amazing", AnimalId = 1}
            };

            mockAnimalRepository.Setup(x => x.IncludeComments(It.IsAny<Expression<Func<Animal, bool>>>()))
                .Returns(animal);

            var controller = new HomeController(null, mockUnitOfWork.Object);

            // Act
            var result = controller.Details(1) as ViewResult;
            var model = result.Model as Animal;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Animal));
            Assert.AreEqual(animal, model);
        }


        [TestMethod]
        public void AddComment_ReturnsRedirectToActionResult_WhenModelStateIsValid()
        {
            // Arrange
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockLogger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(mockLogger.Object, mockUnitOfWork.Object);
            var animal = new Animal { AnimalId = 1, Name = "Lion", Description = "King of Jungle" };
            var comment = new Comment { AnimalId = 1, CommentText = "Great animal!"};

            // Act
            var result = controller.AddComment(comment) as RedirectToActionResult;

            // Assert
            //Assert.IsNotNull(result);
            Assert.AreEqual("Details", result.ActionName);
            Assert.AreEqual("Home", result.ControllerName);
            Assert.AreEqual(comment.AnimalId, result.RouteValues["id"]);
        }

    }
}
