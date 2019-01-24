using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace QAFoods.Controllers
{
    public class ReviewController : Controller
    {
        string uri = "https://localhost:44388/";
        public IActionResult Index()
        {
            return View();
        }

        public IEnumerable<Food> GetAllFood()
        {
            IEnumerable<Food> foodlist = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                HttpResponseMessage Res = null;

                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Res = client.GetAsync("food/getall").Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var response = Res.Content.ReadAsStringAsync().Result;
                        foodlist = JsonConvert.DeserializeObject<List<Food>>(response);
                    }
                    else
                    {
                        foodlist = null;
                        ModelState.AddModelError(String.Empty, "Server error. Unable to retrieve food details.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                return foodlist;
            }
        }

        public ActionResult saveFoodReview([FromBody]Review review)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var postTask = client.PostAsJsonAsync($"review/create", review);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        RedirectToAction("FoodReview", "Food", review.FoodId);
                    }
                    else
                    {
                        review = null;
                        ModelState.AddModelError(String.Empty, "Server error. Unable to save food details.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                return View(review);
            }
        }

        public ActionResult updateFoodReview([FromBody]Review review)
        {
            if (!ModelState.IsValid)
                return View();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{uri}review/update");

                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var putTask = client.PutAsJsonAsync<Review>("update", review);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        RedirectToAction("FoodReview", "Review", review.FoodId);
                    }
                    else
                    {
                        review = null;
                        ModelState.AddModelError(String.Empty, "Server error. Unable to save food details.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                return View(review);
            }
        }

        public ViewResult FoodReview(int id)
        {
            var foodreview = GetFoodReview(id);
                return View(foodreview);
        }

        public reviewlist GetFoodReview(int id)
        {
            var foodreview = new reviewlist();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                HttpResponseMessage Res = null;
                var currentUserId = 2;
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Res = client.GetAsync($"review/getbyfooduserid/{id}/{currentUserId}").Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var UserResponse = Res.Content.ReadAsStringAsync().Result;
                        foodreview.AllReviews = JsonConvert.DeserializeObject<List<Review>>(UserResponse);
                    }
                    else
                    {
                        foodreview = null;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                return foodreview;
            }
        }
    }
}