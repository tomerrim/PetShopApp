using PetShop.DataAccess.Data.Repository;
using PetShop.DataAccess;
using PetShop.DataAccess.Data.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShopApp.Test.Repositories
{
    public class MockUnitOfWork : IUnitOfWork
    {
        IAnimalRepository IUnitOfWork.Animals => throw new NotImplementedException();

        ICategoryRepository IUnitOfWork.Categories => throw new NotImplementedException();

        ICommentRepository IUnitOfWork.Comments => throw new NotImplementedException();

        void IUnitOfWork.Save()
        {
            throw new NotImplementedException();
        }
    }
}
