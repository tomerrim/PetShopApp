using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.DataAccess;
using PetShop.DataAccess.Data.Repository;
using PetShop.DataAccess.Data.Repository.IRepository;
using PetShop.Models;
using System.Net.NetworkInformation;
using System.Threading;
using System;
using Microsoft.CodeAnalysis.CSharp;

namespace PetShopApp.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILogger<CatalogController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CatalogController(IUnitOfWork unitOfWork, ILogger<CatalogController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var categories = _unitOfWork.Categories.GetAll();
                IEnumerable<SelectListItem> CategoryList = categories.Select(
                   u => new SelectListItem
                   {
                       Text = u.Name,
                       Value = u.CategoryId.ToString()
                   });
                ViewBag.CategoryList = CategoryList;
                IEnumerable<Animal> animalList = _unitOfWork.Animals.GetAll();
                return View("Index", animalList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Catalog Index GET exception: {ex.GetBaseException().Message}");
                throw;
            }
        }

        [HttpPost]
        public IActionResult Index(int categoryId)
        {           
            try
            {
                var objCategory = _unitOfWork.Categories.GetFirstOrDefault(u => u.CategoryId == categoryId);
                var categories = _unitOfWork.Categories.GetAll();
                IEnumerable<SelectListItem> CategoryList = categories.Select(
                    u => new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.CategoryId.ToString()
                    });
                ViewBag.CategoryList = CategoryList;
                if (categoryId == null || categoryId == 0)
                {
                    IEnumerable<Animal> animalList = _unitOfWork.Animals.GetAll();
                    return View(animalList);
                }
                IEnumerable<Animal> animalByCategory = _unitOfWork.Animals.AnimalsByCategory(categoryId);
                return View("Index", animalByCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Catalog Index POST exception: {ex.GetBaseException().Message}");
                throw;
            }
        }
    }
}
