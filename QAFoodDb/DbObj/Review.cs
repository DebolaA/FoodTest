using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFoodDb
{
    [Table("Review")]
    public class Review
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int ReviewId { get; set; }
        public int FoodId { get; set; }
        public int UserId { get; set; }
        public DateTime ReviewedOn { get; set; }
        public Food Food { get; set; }
        public User User { get; set; }
        public ReviewDetails Details { get; set; }

    }
    public enum ReviewCategory
    {
        Presentation, Texture, Aroma, Flavour
    }
}

