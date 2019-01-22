using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAFoodDb
{
    [Table("User")]
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int UserId { get; set; }
        [MaxLength(50)]
        public string FName { get; set; }
        [MaxLength(50)]
        public string LName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int? RoleId { get; set; }
        public ICollection<Review> Reviews { get; set; }        
    }
}
