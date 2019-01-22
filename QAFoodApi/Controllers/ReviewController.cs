using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QAFoodDb;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QAFoodApi
{
    public class ReviewController : BaseController<ReviewController>
    {
        public ReviewController(IQAFoodDb dbContext,
                                ILogger<ReviewController> logger) :
                base(dbContext, logger)
        { }

        [Route("/review/getall")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    var reviews = context.Reviews
                        .OrderBy(r => r.ReviewedOn)
                        .ToList();

                    if (reviews == null)
                        return NotFound(); 

                    return Ok(reviews);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [Route("/review/{id}")]
        [HttpGet]
        public IActionResult GetReviewById(int id)
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    var item = context.Reviews.FirstOrDefault(r => r.ReviewId == id);

                    var review = context.Reviews.Join(
                        context.ReviewDetails,
                        r => r.ReviewId,
                        rd => rd.ReviewId,
                        (r, rd) => new { rev = r, revDet = rd })
                        .Where(reviewId => reviewId.rev.ReviewId == id)
                        .Join(context.Foods,
                        newrev => newrev.rev.FoodId,
                        f => f.FoodId,
                        (newrev, f) => new ReviewModel
                        {
                            ReviewId = newrev.rev.ReviewId,
                            ReviewedOn = newrev.rev.ReviewedOn,
                            AromaScore = newrev.revDet.AromaScore,
                            FlavourScore = newrev.revDet.FlavourScore,
                            PresentationScore = newrev.revDet.PresentationScore,
                            TextureScore = newrev.revDet.TextureScore,
                            FoodId = newrev.rev.FoodId,
                            UserId = newrev.rev.UserId,
                            FoodName = f.FoodName, 
                            UserName = context.Users.Where(u => u.UserId == item.UserId).Select(u => u.Email).ToString()
                        })
                        .Where(z => z.ReviewId == id);

                    if (review == null)
                        return NotFound();

                    return Ok(review);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Route("/review/getbyfooduserid/{fid}/{uid}")]
        [HttpGet]
        public IActionResult GetReviewByFoodUserId(int fid, int uid)
        {
            try
            {
                using (var context = new QAFoodContext())
                {
                    var food = context.Foods
                               .FirstOrDefault(f => f.FoodId == fid);
                    var user = context.Users
                        .FirstOrDefault(u => u.UserId == uid);

                    var review = context.Reviews.Join(
                        context.ReviewDetails,
                        r => r.ReviewId,
                        rd => rd.ReviewId,
                        (r, rd) => new ReviewModel
                        {
                            ReviewId = r.ReviewId,
                            ReviewedOn = r.ReviewedOn,
                            AromaScore = rd.AromaScore,
                            FlavourScore = rd.FlavourScore,
                            PresentationScore = rd.PresentationScore,
                            TextureScore = rd.TextureScore,
                            FoodId = r.FoodId,
                            UserId = r.UserId,
                            FoodName = food.FoodName,
                            UserName = user.Email
                        }).Where(f => f.FoodId == fid && f.UserId == uid).ToList();

                    if(review.Count() == 0)
                    {
                        review.Add(new ReviewModel
                        {
                            FoodId = food.FoodId,
                            FoodName = food.FoodName
                        });
                    }
                    return new JsonResult(review);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Route("/review/create")]
        [HttpPost]
        public IActionResult Create(ReviewModel review)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                using (var context = new QAFoodContext())
                {
                    if (review != null)
                    {
                        var detail = new ReviewDetails { AromaScore = review.AromaScore, FlavourScore = review.FlavourScore,
                            PresentationScore = review.PresentationScore, TextureScore = review.TextureScore };
                        var rev = new Review { FoodId = review.FoodId, UserId = review.UserId, ReviewedOn = review.ReviewedOn };
                        rev.Details = detail;
                        var result = context.Reviews.Add(rev);

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

        [Route("/review/update")]
        [HttpPut]
        public IActionResult Update(ReviewModel review)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");
            try
            {
                using (var context = new QAFoodContext())
                {
                    if (review != null)
                    {
                        var existingReview = context.Reviews
                        .FirstOrDefault(r => r.ReviewId == review.ReviewId);

                        var existingDetails = context.ReviewDetails
                            .FirstOrDefault(r => r.ReviewId == review.ReviewId);
                        
                        if(existingDetails != null)
                        {
                            existingDetails.AromaScore = review.AromaScore;
                            existingDetails.FlavourScore = review.FlavourScore;
                            existingDetails.PresentationScore = review.PresentationScore;
                            existingDetails.TextureScore = review.TextureScore;
                        }
                        
                        if (existingReview != null)
                        {
                            existingReview.ReviewedOn = review.ReviewedOn;
                            existingReview.UserId = review.UserId;
                            existingReview.FoodId = review.FoodId;
                        }
                        else
                        {
                            return NotFound();
                        }
                        context.Entry(existingReview).State = EntityState.Modified;
                        context.Entry(existingDetails).State = EntityState.Modified;
                        context.SaveChanges();
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

        [Route("/review/{id}")]
        [HttpDelete]
        public IActionResult DeleteReview(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid review id");

            try
            {
                using (var context = new QAFoodContext())
                {
                    var review = context.Reviews
                        .SingleOrDefault(r => r.ReviewId == id);


                    if (review == null)
                        return NotFound();

                    context.Reviews.Remove(review);
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
