using Core.Domain;
using smartData.Areas.Users.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace smartData.Areas.Users.ApiControllers
{
    public interface IPreferenceAPIController
    {
        List<smPreference> GetPreference(int userId);
        List<smPreference> GetAllSocialPreference(int limit, int offset, string order, string sort, string Preference, int currentUserId, out int total);
        smPreference EditPreference(int id);
        bool UpdatePreference(int PreferenceId, string Preference, int userId, int ImpersonateUserId);
        bool DeletePreference(string name, int userId, int ImpersonateUserId);
        bool AddPreference(string Preference, int userId, int ImpersonateUserId);
    }


}
