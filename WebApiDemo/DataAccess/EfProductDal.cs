using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApiDemo.Entities;
using WebApiDemo.Models;

namespace WebApiDemo.DataAccess
{
    public class EfProductDal : EfEntityRepositoryBase<Product, NorthwindContext>, IProductDal
    {
        public List<ProductModel> GetProductWithDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var query = from p in context.Products
                            join c in context.Categories
                            on p.CategoryId equals c.CategoryId
                            select new ProductModel
                            {
                                ProductId = p.ProductId,
                                ProductName = p.ProductName,
                                UnitPrice = p.UnitPrice,
                                CategoryName = c.CategoryName
                            };
                return query.ToList();
            }
        }
    }
}
