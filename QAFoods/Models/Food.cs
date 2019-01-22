using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QAFoods
{
    public class Food
    {
        public int FoodId { get; set; }
        
        public string FoodName { get; set; }
        
        public string FoodDesc { get; set; }
        public IEnumerable<Review> GetFoodReviews { get; set; }
    }



    public class foodlist
    {
        public List<Food> AllFood { get; set; }
    }
}
