using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExerciseApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace ExerciseApi.ServiceLayer
{
    public class ProductService : IProductService
    {
        private readonly NorthwindContext _context;

        public ProductService(NorthwindContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {            
            return await _context.Products.FromSql("sp_Products @p0","SelectAll").ToListAsync();
        }
        
        public async Task<ActionResult<IEnumerable<Product>>> GetActive()
        {
            return await _context.Products.FromSql("sp_Products @p0","SelectActive").ToListAsync();
        }
        
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var pAction = new SqlParameter("@Action", "SelectOne");
            var pId = new SqlParameter("@ProductId", id);
            return await _context.Products.FromSql("sp_Products @Action, @ProductId", pAction, pId).FirstAsync();
        }

        public async Task Add(Product product)
        {
            await _context.Database.ExecuteSqlCommandAsync(
                "sp_Products @Action, @ProductId, @ProductName,@SupplierID,@CategoryID,@QuantityPerUnit,@UnitPrice,@UnitsInStock,@UnitsOnOrder,@ReorderLevel,@Discontinued",
                parameters: GetSqlParameters("AddNew", product));
        }

        public async Task Update(int id, Product product)
        {
            product.ProductId = id; //in case it was not passed in the object
            await _context.Database.ExecuteSqlCommandAsync(
                "sp_Products @Action, @ProductId, @ProductName,@SupplierID,@CategoryID,@QuantityPerUnit,@UnitPrice,@UnitsInStock,@UnitsOnOrder,@ReorderLevel,@Discontinued",
                parameters: GetSqlParameters("Update", product));
        }

        public async Task Delete(int id)
        {
            var pAction = new SqlParameter("@Action", "Delete");
             var pId = new SqlParameter("@ProductId", id);
            await _context.Database.ExecuteSqlCommandAsync("sp_Products @Action, @ProductId", parameters: new[] { pAction, pId });
        }

        private SqlParameter[] GetSqlParameters(string actionName, Product product)
        {
            var plist = new List<SqlParameter>();    
            plist.Add(new SqlParameter("@Action", actionName));
            plist.Add(new SqlParameter("@ProductId", product.ProductId));
            plist.Add(new SqlParameter("@ProductName", product.ProductName));
            plist.Add(new SqlParameter("@SupplierID", product.SupplierId));
            plist.Add(new SqlParameter("@CategoryID", product.CategoryId));
            plist.Add(new SqlParameter("@QuantityPerUnit", product.QuantityPerUnit));
            plist.Add(new SqlParameter("@UnitPrice", product.UnitPrice));
            plist.Add(new SqlParameter("@UnitsInStock", product.UnitsInStock));
            plist.Add(new SqlParameter("@UnitsOnOrder", product.UnitsOnOrder));
            plist.Add(new SqlParameter("@ReorderLevel", product.ReorderLevel));
            plist.Add(new SqlParameter("@Discontinued", product.Discontinued));
            return plist.ToArray();
        }
    }
}