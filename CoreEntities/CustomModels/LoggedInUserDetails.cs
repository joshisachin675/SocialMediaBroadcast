using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreEntities.CustomModels
{
    public class LoggedInUserDetails
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int UserType { get; set; }
        public bool ProfileImage { get; set; }
        public bool IsSuperAdmin { get; set; }
        public int IndustryId { get; set; }
        public int ImpersonateUserId { get; set; }
    }
}
