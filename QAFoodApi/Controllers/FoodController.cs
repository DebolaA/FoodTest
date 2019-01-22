using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QAFoodApi;
using QAFoodDb;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QAFoodApi
{
    public class FoodController : BaseController<FoodController>
    {
        public FoodController(IQAFoodDb dbContext
                              ,ILogger<FoodController> logger) :
                base(dbContext, logger)
        { }

        /// <summary>
        /// Retrieve Food from the database.
        /// </summary>
        /// <param></param>
        /// <returns>Returns IactionResult</returns>
        [Route("/food/getall")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    var food = context.Foods
                        .OrderBy(r => r.FoodName)
                        .ToList();

                    if (food == null)
                        return NotFound();

                    return Ok(food);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Route("/food/{id}")]
        [HttpGet]
        public IActionResult GetFoodById(int id)
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    var food = context.Foods
                        .FirstOrDefault(f => f.FoodId == id);

                    if (food == null)
                        return NotFound();
                                       
                    return Ok(food);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest();
            }
        }

        [Route("/food/create")]
        [HttpPost]
        public IActionResult Create(FoodModel food)
        {
            try
            {
                //int result = null;
                using (var context = new QAFoodContext())
                {
                    if(!String.IsNullOrWhiteSpace(food.FoodName))
                    {
                         var result = context.Foods.Add(
                            new Food { FoodName = food.FoodName, FoodDesc = food.FoodDesc });

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

        [Route("/food/update")]
        [HttpPut]
        public IActionResult Update(FoodModel food)
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    if (food != null)
                    {
                        var existingFood = context.Foods
                        .FirstOrDefault(f => f.FoodId == food.FoodId);

                        if(existingFood != null)
                        {
                            existingFood.FoodName = food.FoodName;
                            existingFood.FoodDesc = food.FoodDesc;

                            context.Entry(existingFood).State = EntityState.Modified;

                            context.SaveChanges();
                        } else
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

        [Route("/food/{id}")]
        [HttpDelete]
        public IActionResult DeleteFood(int id)
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    var result = context.Foods
                        .SingleOrDefault(f => f.FoodId == id);

                    if (result == null)
                        return NotFound();

                    context.Foods.Remove(result);
                    context.SaveChanges();

                    return Ok();
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }       
        }
    }
}
