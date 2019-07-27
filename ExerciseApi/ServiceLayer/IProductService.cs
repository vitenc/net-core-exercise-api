using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ExerciseApi.Models;
using System.Threading.Tasks;

namespace ExerciseApi.ServiceLayer
{
    public interface IProductService
    {
        Task<ActionResult<IEnumerable<Product>>> GetAll();
        Task<ActionResult<IEnumerable<Product>>> GetActive();
        Task<ActionResult<Product>> GetById(int id);
        Task Add(Product product);
        Task Update(int id, Product product);
        Task Delete(int id);
    }
}