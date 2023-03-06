using Microsoft.AspNetCore.Mvc;
using PetShop.Models;
using PetShop.DataAccess;
using PetShop.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;

namespace PetShopApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ILogger<AdminController> _logger;
        public AdminController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, ILogger<AdminController> logger)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                return View(_unitOfWork.Animals.GetAll());

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Admin Index exception: {ex.GetBaseException().Message}");
                throw;
            }
        }

        //GET
        public IActionResult Delete(int? id)
        {
            try
            {
                if (id == null || id == 0)
                {
                    return NotFound();
                }
                var categoryFromDb = _unitOfWork.Animals.GetFirstOrDefault(x => x.AnimalId == id);
                if (categoryFromDb == null)
                {
                    return NotFound();
                }
                return View(categoryFromDb);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Admin Delete GET exception: {ex.GetBaseException().Message}");
                throw;
            }
        }

        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {

            try
            {
                var animalFromDb = _unitOfWork.Animals.GetFirstOrDefault(x => x.AnimalId == id);
                if (animalFromDb == null)
                {
                    return NotFound();
                }
                ViewBag.animalName = animalFromDb.Name;
                _unitOfWork.Animals.Remove(animalFromDb);
                _unitOfWork.Save();
                _logger.LogInformation($"{ViewBag.animalName} deleted on: {DateTime.Now}");
                TempData["success"] = "Animal deleted successfully";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Admin Delete POST exception: {ex.GetBaseException().Message}");
                throw;
            }
        }


        //GET
        public IActionResult Upsert(int? id) 
        {
            try
            {
                var categories = _unitOfWork.Categories.GetAll();
                AnimalVM animalVM = new()
                {
                    Animal = new(),
                    CategoryList = categories.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.CategoryId.ToString()
                    })
                };
                if (id == null || id == 0)
                {
                    //create animal
                    return View(animalVM);
                }
                else
                {
                    //update animal
                    animalVM.Animal = _unitOfWork.Animals.GetFirstOrDefault(p => p.AnimalId == id);
                    return View(animalVM);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Admin Upsert GET exception: {ex.GetBaseException().Message}");
                throw;
            }   
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(AnimalVM animalVM, IFormFile? file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    if (file != null)
                    {
                        string fileName = animalVM.Animal.Name;
                        var uploads = Path.Combine(wwwRootPath, @"images\animals");
                        var extension = Path.GetExtension(file.FileName);

                        if (animalVM.Animal.PictureName != null)
                        {
                            var oldImage = Path.Combine(wwwRootPath, animalVM.Animal.PictureName.TrimStart('\\'));
                            if (System.IO.File.Exists(oldImage))
                            {
                                System.IO.File.Delete(oldImage);
                            }
                        }

                        using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            file.CopyTo(fileStreams);
                        }
                        animalVM.Animal.PictureName = @"\images\animals\" + fileName + extension;
                    }
                    if (animalVM.Animal.AnimalId == 0)
                    {
                        _unitOfWork.Animals.Add(animalVM.Animal);
                        ViewBag.animalName = animalVM.Animal.Name;
                    }
                    else
                    {
                        _unitOfWork.Animals.Update(animalVM.Animal);
                        ViewBag.animalName = animalVM.Animal.Name;
                    }
                    _unitOfWork.Save();
                    TempData["success"] = animalVM.Animal.AnimalId == 0 ? "Animal created successfully" : "Animal updated successfully";
                    var act = (animalVM.Animal.AnimalId == 0) ? "created" : "updated";
                    _logger.LogInformation($"{ViewBag.animalName} was {act} on: {DateTime.Now}");
                    return RedirectToAction("Index");
                }
                return View(animalVM);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Admin Upsert POST exception: {ex.GetBaseException().Message}");
                throw;
            }
        }
    }
}
