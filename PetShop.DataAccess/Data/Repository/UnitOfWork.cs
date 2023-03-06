using PetShop.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.DataAccess.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PetDbContext _db;
        public UnitOfWork(PetDbContext db)
        {
            _db = db;
            Animals = new AnimalRepository(_db);
            Categories= new CategoryRepository(_db);
            Comments= new CommentRepository(_db);
        }

        public IAnimalRepository Animals { get; private set; }
        public ICategoryRepository Categories { get; private set; }
        public ICommentRepository Comments { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
