using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleecyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork 
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverTypes { get; }
        IProductRepository Products { get; }

        void Save();
    }
}
