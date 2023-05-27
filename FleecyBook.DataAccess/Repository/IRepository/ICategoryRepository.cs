using FleecyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleecyBook.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Add(Category obj);
        IEnumerable<Category> GetAll();
        void Update(Category obj);

    }
}
