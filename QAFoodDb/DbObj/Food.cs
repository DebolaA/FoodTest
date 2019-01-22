using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QAFoodDb
{
    [Table("Food")]
    public class Food
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int FoodId { get; set; }

        [MaxLength(50)]
        [Required]
        public string FoodName { get; set; }
        [MaxLength(500)]
        public string FoodDesc { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
    }
}
