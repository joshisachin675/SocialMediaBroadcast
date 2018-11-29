using smartData.Infrastructure;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using smartData.Filter;
using ServiceLayer.Interfaces;
using System.Web.Security;

namespace smartData
{
    public class UserListFilters : GridFilter
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class UserReturnVal
    {
        public IEnumerable<Users> data;
        public int total;
    }
   

    [HandleException]
    public class UsersAPIController : ApiController, IUsersAPIController
    {


        IUserService _userService;


        public UsersAPIController(IUserService _UserService)
        {
            _userService = _UserService;
            //System.Net.Http.Headers.contContentType = new MediaTypeHeaderValue("application/json");
        }


        [HttpPost]
        [ActionName("GetUsers")]
        [AntiforgeryValidate]
        public dynamic GetUsers(JObject Obj)
        {
            UserListFilters filter = Obj.ToObject<UserListFilters>();
            int total;
            UserReturnVal re = new UserReturnVal();
            re.data = _userService.GetAllUsers(filter.limit, filter.offset, filter.order, filter.sort, filter.FirstName, filter.LastName, filter.Email, out total);

            var result = new
            {
                total = total,
                rows = re.data,
            };
            return result;
        }


        [HttpPost]
        [HandleAPIExceptionAttribute]
        public Users Create(UserInsert user)
        {

            Users _usersObject = _userService.CreateUser(user);
            return _usersObject;
        }

        [HttpGet]
        public Users GetUserByID(int? id)
        {
            return _userService.GetUserById(id);
        }

        [HttpPost]
        [ActionName("EditUser")]
        [AntiforgeryValidate]
        public String EditUser(Users user)
        {
        
            return _userService.EditUser(user);
        }

        [HttpPost]
        [ActionName("DeleteUser")]
        [AntiforgeryValidate]
        public bool DeleteUser(int id)
        {
            bool deleteAcntRes;
            Core.Domain.Users user = _userService.GetUserById(id);
            var res = _userService.DeleteUser(id);
            // Delete user account from webpages_Membership table
            if (res)
            {
                if (user != null)
                {
                    if (((WebMatrix.WebData.SimpleMembershipProvider)Membership.Provider).HasLocalAccount(user.UserId))
                    {
                        deleteAcntRes = ((WebMatrix.WebData.SimpleMembershipProvider)Membership.Provider).DeleteAccount(user.Email);
                    }
                }
            }
            return res;
        }

       
    }
}
