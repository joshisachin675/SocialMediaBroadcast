using Core.Domain;
using Newtonsoft.Json.Linq;
using ServiceLayer.Interfaces;
using smartData.Common;
using smartData.Filter;
using smartData.Infrastructure;
using smartData.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
namespace smartData.Areas.Users.ApiControllers
{

    class SocialAccountListFiltersPreference : smartData.Common.GridFilter
    {

        public string Preference { get; set; }
        public string CreatedDate { get; set; }

        public int currentUserId { get; set; }

    }
    class SocialPreference
    {
        public IEnumerable<smPreference> PreferenceData;
    }
    [HandleException]
    public class PreferenceAPIController : ApiController, IPreferenceAPIController
    {

        IPreferenceService _preferenceService;


        #region constructor
        public PreferenceAPIController(IPreferenceService _PreferenceService)
        {
            _preferenceService = _PreferenceService;

        }
        #endregion

        public List<smPreference> GetPreference(int userId)
        {
            return _preferenceService.GetPreference(userId);
        }








        public List<smPreference> GetAllSocialPreference(int limit, int offset, string order, string sort, string Preference, int currentUserId, out int total)
        {
            return _preferenceService.GetAllSocialPreference(limit, offset, order, sort, Preference, currentUserId, out total);
        }
        [HttpPost]
        public smPreference EditPreference(int id)
        {
            return _preferenceService.EditPreference(id);
        }




        [HttpPost]
        [ActionName("GetAllPreference")]
        public dynamic GetAllPreference(JObject Obj)
        {
            SocialAccountListFiltersPreference filter = Obj.ToObject<SocialAccountListFiltersPreference>();
            int total;

            SocialPreference re = new SocialPreference();
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = filter.currentUserId;
            re.PreferenceData = _preferenceService.GetAllSocialPreference(filter.limit, filter.offset, filter.order, filter.sort, filter.Preference, filter.currentUserId, out total);

            foreach (var item in re.PreferenceData)
            {

                item.CreatedDate = item.CreatedDate.ToLocalTime();


            }
            var result = new
            {
                total = total,
                rows = re.PreferenceData
            };

            return result;
        }

        [HttpPost]
        public bool UpdatePreference(int PreferenceId, string Preference, int userId, int ImpersonateUserId)
        {
            return _preferenceService.UpdatePreference(PreferenceId, Preference, userId, ImpersonateUserId);
        }
        public bool DeletePreference(string name, int userId, int ImpersonateUserId)
        {
            return _preferenceService.DeletePreference(name, userId, ImpersonateUserId);
        }
        [HttpPost]
        public bool AddPreference(string Preference, int userId, int ImpersonateUserId)
        {
            return _preferenceService.AddPreference(Preference, userId, ImpersonateUserId);
        }

    }
}


