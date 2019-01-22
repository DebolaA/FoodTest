using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAFoods
{
    public class User
    {
        public int UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public int? RoleId { get; set; }
    }
}
