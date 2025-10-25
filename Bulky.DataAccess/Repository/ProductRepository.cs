using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository :Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        // In your repository
        public IEnumerable<Product> GetAllProductsWithCategory()
        {
            return _db.Products.Include(p => p.Category).ToList();
        }


        public void Update(Product product)
        {
            _db.Products.Update(product);
        }
    }
}
