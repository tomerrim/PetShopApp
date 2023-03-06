using PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.DataAccess.Data.Repository.IRepository
{
    public interface IAnimalRepository : IRepository<Animal>
    {
        Animal IncludeComments(Expression<Func<Animal,bool>> filter);

        IEnumerable<Animal> AnimalsByCategory(int categoryId);
        List<Animal> AnimalsMostCommented(int count);
    }
}
