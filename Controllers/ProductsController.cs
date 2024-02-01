using MAAPI.Data;
using MAAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MAAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<ProductsController> logger;

        public ProductsController(AppDbContext context,ILogger<ProductsController> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> Gets(string? q=null)
        {
            IQueryable<Product> results= context.Products;
            if(q != null)
            {
                results=results.Where(x=>x.ProductName.ToLower().StartsWith(q.ToLower()));

            }
            return await results.AsNoTracking().ToListAsync();
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            if (id !=0)
            {
                var result = await context.Products.AsNoTracking().FirstOrDefaultAsync(x=>x.ProductId==id);
                if (result != null)
                {
                    return Ok(result);
                }
                
                return BadRequest();
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateProduct(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                context.Products.Add(product);
                if(await context.SaveChangesAsync()>0)
                {
                    return Ok(true);
                }
                return BadRequest(false);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return Problem(ex.Message, statusCode: 500);
            }
            
            
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> UpdateProduct(int id,Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await context.Products.AsNoTracking().FirstOrDefaultAsync(x=>x.ProductId == id);
                if(result==null)
                    return BadRequest(false);
                context.Products.Update(product);
                if (await context.SaveChangesAsync() > 0)
                {
                    return Ok(true);
                }
                return BadRequest(false);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return Problem(ex.Message, statusCode: 500);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<bool>> DeleteProduct(int id)
        {
            try
            {
                var result = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.ProductId == id);
                if (result == null)
                    return BadRequest(false);
                context.Products.Remove(result);
                if (await context.SaveChangesAsync() > 0)
                {
                    return Ok(true);
                }
                return BadRequest(false);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return Problem(ex.Message, statusCode: 500);
            }
        }
    }
}
