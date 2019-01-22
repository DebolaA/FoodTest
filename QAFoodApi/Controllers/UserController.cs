using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QAFoodDb;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QAFoodApi
{
    [Route("/user")]
    public class UserController : BaseController<UserController>
    {
        public UserController(IQAFoodDb dbContext,
                                ILogger<UserController> logger) :
                base(dbContext, logger)
        { }

        [Route("/user/getall")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    var users = context.Users
                        .OrderBy(u => u.FName)
                        .ToList();
                    return Ok(users);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }

        }

        [Route("/user/{id}")]
        [HttpGet]
        public IActionResult GetUserById(int id)
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    var user = context.Users
                        .FirstOrDefault(u => u.UserId == id);

                    if (user == null)
                        return NotFound();

                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [Route("/user/create")]
        [HttpPost]
        public IActionResult Create(UserModel user)
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    if (String.IsNullOrWhiteSpace(user.Email))
                    {
                        var result = context.Users.Add(
                            new User { FName = user.FName, LName = user.LName, Email = user.Email, CreatedBy = user.CreatedBy, CreatedOn = user.CreatedOn, RoleId = user.RoleId });

                        context.SaveChanges();
                        return Ok(result);
                    }
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        // PUT api/products
        //[HttpPut]
        //public async Task<ActionResult> Put(FoodModel food)
        //{
        //    _dbContext.Entry(food).State = EntityState.Modified;
        //    await _dbContext.SaveChangesAsync();
        //    return Ok(food);
        //}

        [Route("/user/{id}")]
        [HttpDelete]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    var user = context.Users
                        .SingleOrDefault(u => u.UserId == id);

                    context.Users.Remove(user);
                    context.SaveChanges();

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
