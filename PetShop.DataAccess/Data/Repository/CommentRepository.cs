using PetShop.DataAccess.Data.Repository.IRepository;
using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.DataAccess.Data.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly PetDbContext _db;
        public CommentRepository(PetDbContext db) : base(db)
        {
            _db = db;
        }

        void ICommentRepository.CommentAdd(Comment comment)
        {
            _db.Add(comment);
        }
    }
}
