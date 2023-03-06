using Microsoft.EntityFrameworkCore;
using PetShop.DataAccess;
using PetShop.DataAccess.Data.Repository;
using PetShop.DataAccess.Data.Repository.IRepository;
using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetShopApp.Test.Repositories
{
    public class MockAnimalRepository : Repository<Animal>, IAnimalRepository
    {
        private readonly PetDbContext _db;
        public MockAnimalRepository(PetDbContext db) : base(db)
        {
            _db = db;
        }

        IEnumerable<Animal> IAnimalRepository.AnimalsByCategory(int categoryId)
        {
            var animals = _db.Animals.Where(a => a.CategoryId == categoryId).ToList();
            return animals;
        }

        List<Animal> IAnimalRepository.AnimalsMostCommented(int count)
        {
            var list = _db.Animals.Include(a => a.Category).OrderByDescending(a => a.Comments.Count()).Take(count).ToList();
            return list;
        }

        Animal IAnimalRepository.IncludeComments(Expression<Func<Animal, bool>> filter)
        {
            var animal = _db.Animals.Include(x => x.Comments).Where(filter).FirstOrDefault();
            return animal!;
        }
    }
}
