using FleecyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FleecyBook.DataAccess.Repository.IRepository
{
    public class CategoryRespository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRespository(ApplicationDbContext db) : base(db)
        {
            _db = db;  
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
