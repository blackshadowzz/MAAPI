using MAAPI.Data;
using MAAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MAAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<CategoriesController> logger;

        public CategoriesController(AppDbContext context,ILogger<CategoriesController> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> Gets()
        {
            var results= await context.Categories.ToListAsync();
            if(results.Any())
            {
                return Ok(results);
            }
            return BadRequest();
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateCate(Category category)
        {
            try
            {
                context.Categories.Add(category);
                if(await context.SaveChangesAsync()>0)
                    return Ok(true);
                return BadRequest(false);
            }catch(Exception ex)
            {
                logger.LogInformation(ex.Message);
                return Problem(ex.Message, statusCode: 500);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> UpdateCate(int id,Category category)
        {
            try
            {
                if (id > 0)
                {
                    context.Categories.Update(category);
                    if (await context.SaveChangesAsync() > 0)
                        return Ok(true);
                    return BadRequest(false);
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
        public async Task<ActionResult<bool>> DeleteCate(int id)
        {
            try
            {
                if (id > 0)
                {
                    var result= await context.Categories.FirstOrDefaultAsync(x=>x.Id == id);
                    if(result != null)
                    {
                        context.Categories.Remove(result);
                        if (await context.SaveChangesAsync() > 0)
                            return Ok(true);
                    }
                    return BadRequest(false);
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
