using Bulky.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository.IRepository
{
    public class UnitOfWork : IUnitofOfWork
    {
        ApplicationDbContext _db;
        public ICategoryRepository categoryRepo { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            categoryRepo = new CategoryRepository(_db);
        }


        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
