using FleecyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleecyBook.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Add(Product obj);
        void Update(Product obj);

    }
}
