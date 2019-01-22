using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QAFoodDb
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [MaxLength(20)]
        public string RoleName { get; set; }
    }
}
