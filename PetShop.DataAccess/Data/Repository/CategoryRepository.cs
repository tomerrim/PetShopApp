using PetShop.DataAccess.Data.Repository.IRepository;
using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.DataAccess.Data.Repository
{
    public class CategoryRepository : Repository<Category>,ICategoryRepository
    {
        private readonly PetDbContext _db;
        public CategoryRepository(PetDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category category)
        {
            _db.Update(category);
        }
    }
}
