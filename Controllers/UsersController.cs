using MAAPI.Data;
using MAAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MAAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly ILogger<UsersController> logger;

        public UsersController(AppDbContext context, ILogger<UsersController> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> Gets(string? q = null)
        {
            IQueryable<ApplicationUser> results = context.Users;
            if (q is not null)
            {
                results = results.Where(x => x.FullName.ToLower().StartsWith(q.ToLower())
                || x.UserName.ToLower().StartsWith(q.ToLower()));
            }
            return await results.AsNoTracking().ToListAsync();
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApplicationUser>> Get(int id)
        {
            if(id >0)
            {
                var result = await context.Users.FirstOrDefaultAsync(x=>x.UserId==id);
                if (result == null)
                    return Ok(result!);
                return NotFound();
            }
            return NotFound();
        }
        [HttpPost("Login")]
        public async Task<ActionResult<bool>> Login(LoginRequest user)
        {
            try
            {
                if (user != null)
                {
                    if(await context.Users.AnyAsync(x=>x.UserPassword==user.UserPassword && x.UserName == user.UserName))
                    {
                        return Ok(true);
                    }
                    else
                    {
                        return BadRequest(false);
                    }
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return Problem(ex.Message, statusCode: 500);
            }
        }
        [HttpPost]
        public async Task<ActionResult<bool>> CreateUser(ApplicationUser user)
        {
            try
            {
                context.Users.Add(user);
                return await context.SaveChangesAsync()>0;

            }catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return Problem(ex.Message, statusCode: 500);
            }
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult<bool>> UpdateUser(int id,ApplicationUser user)
        {
            try
            {
                if (id==0)
                {
                    return BadRequest();
                }
                var result = await context.Users.FirstOrDefaultAsync(x=>x.UserId==id);
                context.Users.Update(result!);
                if (await context.SaveChangesAsync() > 0)
                    return Ok(true);
                return BadRequest(false);

            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.Message);
                return Problem(ex.Message, statusCode: 500);
            }
        }
        [HttpPut("Password/{id:int}")]
        public async Task<ActionResult<bool>> ChangePassword(int id, UserChangePassword user)
        {
            try
            {
                if (id==0)
                {
                    return BadRequest();
                }
                var result = await context.Users.FirstOrDefaultAsync(x => x.UserId == id);
                result!.UserPassword = user.UserPassword;
                context.Users.Update(result);
                if(await context.SaveChangesAsync() > 0)
                    return Ok(true);
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
