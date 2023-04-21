using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;
using System.Diagnostics;
using PetShop.DataAccess;
using PetShop.DataAccess.Data.Repository.IRepository;

namespace PetShopApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            try
            {
                var popularAnimals = _unitOfWork.Animals.AnimalsMostCommented(2);
                return View(popularAnimals);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Home Index exception: {ex.GetBaseException().Message}");
                throw;
            }
        }

        public IActionResult Details(int id)
        {
            try
            {
                var animal = _unitOfWork.Animals.IncludeCategoryAndComments(a => a.AnimalId == id);
                return View(animal);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Details exception: {ex.GetBaseException().Message}");
                throw;
            }
        }

        public IActionResult AddComment(Comment comment) 
        {
            try
            {
                _unitOfWork.Comments.CommentAdd(comment);
                _unitOfWork.Save();
                return RedirectToAction("Details", "Home", new {id = comment.AnimalId});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AddComment POST exception: {ex.GetBaseException().Message}");
                throw;
            }
        }

        public IActionResult Logo()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}