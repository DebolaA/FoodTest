using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace QAFoods
{
    public class FoodController : Controller
    {
        string uri = "https://localhost:44388/";
        // GET: Food
        public ViewResult Index()
        {
            var foodlist = new foodlist();
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
                        foodlist.AllFood = JsonConvert.DeserializeObject<List<Food>>(response);                        
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
                return View(foodlist);                
            }
        }

        public ViewResult CreateFood()
        {
            return View();
        }

        public ActionResult Create(Food food)
        {
            if (!ModelState.IsValid)
                return View();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);

                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var result = client.PostAsJsonAsync($"food/create", food).Result;

                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        food = null;
                        ModelState.AddModelError(String.Empty, "Server error. Unable to retrieve food details.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                return View(food);
            }
        }
        
        public ViewResult Edit(int id)
        {
            Food food = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                HttpResponseMessage Res = null;

                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Res = client.GetAsync($"food/{id}").Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var UserResponse = Res.Content.ReadAsStringAsync().Result;
                        food = JsonConvert.DeserializeObject<Food>(UserResponse);
                    }
                    else
                    {
                        food = null;
                        ModelState.AddModelError(String.Empty, "Server error. Unable to retrieve food details.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                return View(food);
            }
        }        

        public ActionResult SaveChanges(Food food)
        {
            if (!ModelState.IsValid)
                return View();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{uri}food/update");

                try
                {
                    //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var putTask = client.PutAsJsonAsync<Food>("update", food);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        food = null;
                        ModelState.AddModelError(String.Empty, "Server error. Unable to save food details.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                return View(food);
            }
        }

        public ActionResult Delete(int id)
        {
            if (id <= 0)
                return View();

            Food food = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(uri);
                HttpResponseMessage Res = null;

                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Res = client.DeleteAsync($"food/{id}").Result;

                    if (Res.IsSuccessStatusCode)
                    {
                        var UserResponse = Res.Content.ReadAsStringAsync().Result;
                        food = JsonConvert.DeserializeObject<Food>(UserResponse);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        food = null;
                        ModelState.AddModelError(String.Empty, "Server error. Unable to delete food.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                }
                return View(food);
            }
        }
    }
}