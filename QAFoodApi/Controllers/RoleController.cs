using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QAFoodApi;
using QAFoodDb;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QAFoodApi
{
    public class RoleController : BaseController<RoleController>
    {
        public RoleController(IQAFoodDb dbContext,
                                ILogger<RoleController> logger) :
                base(dbContext, logger)
        { }

        [Route("/role/getall")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    var roles = context.Roles
                        .OrderBy(r => r.RoleName)
                        .ToList();
                    return Ok(roles);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }

        }

        [Route("/role/{id}")]
        [HttpGet]
        public IActionResult GetRoleById(int id)
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    var role = context.Roles
                        .FirstOrDefault(r => r.RoleId == id);

                    if (role == null)
                        return NotFound();

                    return Ok(role);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [Route("/role/create")]
        [HttpPost]
        public IActionResult Create(RoleModel role)
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    if (role.RoleName != null)
                    {
                        var result = context.Roles.Add(
                            new Role { RoleName = role.RoleName });

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

        [Route("/role/update")]
        [HttpPut]
        public IActionResult Update(RoleModel role)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            try
            {
                using (var context = new QAFoodContext())
                {
                    if (role != null)
                    {
                        var existingRole = context.Roles
                        .FirstOrDefault(r => r.RoleId == role.RoleId);

                        if (existingRole != null)
                        {
                            existingRole.RoleName = role.RoleName;

                            context.SaveChanges();
                        }
                        else
                        {
                            return NotFound();
                        }
                    }
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Route("/role/{id}")]
        [HttpDelete]
        public IActionResult DeleteRole(int id)
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    var role = context.Roles
                        .SingleOrDefault(r => r.RoleId == id);

                    context.Roles.Remove(role);
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
