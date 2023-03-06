using Microsoft.EntityFrameworkCore;
using PetShop.DataAccess.Data.Repository.IRepository;
using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.DataAccess.Data.Repository
{
    public class AnimalRepository : Repository<Animal>, IAnimalRepository
    {
        private readonly PetDbContext _db;
        public AnimalRepository(PetDbContext db) :base(db)
        {
            _db = db;
        }

        public IEnumerable<Animal> AnimalsByCategory(int categoryId)
        {
            var animals = _db.Animals.Where(a => a.CategoryId == categoryId).ToList();
            return animals;
        }

        public List<Animal> AnimalsMostCommented(int count)
        {
            var list = _db.Animals.Include(a => a.Category).OrderByDescending(a => a.Comments.Count()).Take(count).ToList();
            return list;
        }

        public Animal IncludeComments(Expression<Func<Animal, bool>> filter)
        {
            var animal = _db.Animals.Include(x => x.Comments).Where(filter).FirstOrDefault();
            return animal!;
        }
    }
}
