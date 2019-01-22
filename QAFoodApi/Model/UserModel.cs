using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QAFoodApi
{
    public class UserModel
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
