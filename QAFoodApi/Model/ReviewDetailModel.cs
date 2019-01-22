using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAFoodApi
{
    public class ReviewDetailModel
    {
        public int ReviewDetailsId { get; set; }
        public int FlavourScore { get; set; }
        public int PresentationScore { get; set; }
        public int AromaScore { get; set; }
        public int TextureScore { get; set; }
        //public int ReviewId { get; set; }
        //public ReviewModel Review { get; set; }
    }
}
