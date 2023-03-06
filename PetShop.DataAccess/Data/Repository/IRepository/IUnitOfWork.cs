using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.DataAccess.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IAnimalRepository Animals { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        void Save();
    }
}
