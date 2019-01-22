using System.Collections.Generic;

namespace QAFoodApi
{
    public class FoodModel
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public string FoodDesc { get; set; }
        public IEnumerable<ReviewModel> Reviews { get; set; }
    }
}
