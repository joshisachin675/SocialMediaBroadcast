using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
    public interface IPreferenceService
    {
        List<smPreference> GetPreference(int userId);
        List<smPreference> GetAllSocialPreference(int limit, int offset, string order, string sort, string Preference, int currentUserId, out int total);
        smPreference EditPreference(int id);
        bool UpdatePreference(int PreferenceId, string Preference, int userId, int ImpersonateUserId);
        bool DeletePreference(string name, int userId, int ImpersonateUserId);
        bool AddPreference(string Preference, int userId, int ImpersonateUserId);
        bool AddAllPreference(List<smSubIndustry> list, int userId, int ImpersonateUserId);
        bool DeleteAllPreference(List<smSubIndustry> list, int userId, int ImpersonateUserId);
    }
}
