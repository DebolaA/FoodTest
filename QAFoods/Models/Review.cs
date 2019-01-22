using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAFoods
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int FoodId { get; set; }
        public string FoodName { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public DateTime ReviewedOn { get; set; }
        public int FlavourScore { get; set; }
        public int PresentationScore { get; set; }
        public int AromaScore { get; set; }
        public int TextureScore { get; set; }
    }


    public class reviewlist
    {
        public List<Review> AllReviews { get; set; }
    }

    public enum ReviewCategory
    {
        Presentation, Texture, Aroma, Flavour
    }
}
