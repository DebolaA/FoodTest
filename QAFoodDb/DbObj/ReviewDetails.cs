using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFoodDb
{
    [Table("ReviewDetails")]
    public class ReviewDetails
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ReviewDetailsId { get; set; }
        public int FlavourScore { get; set; }
        public int PresentationScore { get; set; }
        public int AromaScore { get; set; }
        public int TextureScore { get; set; }
        public int ReviewId { get; set; }
        public Review Review { get; set; }
    }
}
