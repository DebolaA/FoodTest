using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAFoodApi
{
    public class FoodReviewModel
    {
        public int FoodId { get; set; }
        public int UserId { get; set; }
        public DateTime ReviewedOn { get; set; }
    }
}
