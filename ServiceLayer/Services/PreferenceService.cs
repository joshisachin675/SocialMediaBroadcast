using AppInterfaces.Repository;
using Core.Domain;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ServiceLayer.Services
{
    public class PreferenceService : IPreferenceService
    {
        IPreferenceRepository _preferenceRepository = null;
        #region ctor
        public PreferenceService(IPreferenceRepository preferenceRepository)
        {
            _preferenceRepository = preferenceRepository;
        }
        #endregion
        public List<smPreference> GetPreference(int userId)
        {
            return _preferenceRepository.GetPreference(userId);
        }

        public bool AddPreference(string Preference, int userId, int ImpersonateUserId)
        {
            return _preferenceRepository.AddPreference(Preference, userId, ImpersonateUserId);
        }

        public bool AddAllPreference(List<smSubIndustry> list, int userId, int ImpersonateUserId)
        {
            return _preferenceRepository.AddAllPreference(list, userId, ImpersonateUserId);
        }

        public List<smPreference> GetAllSocialPreference(int limit, int offset, string order, string sort, string Preference, int currentUserId, out int total)
        {
            return _preferenceRepository.GetAllSocialPreference(limit, offset, order, sort, Preference, currentUserId, out total);

        }
        public smPreference EditPreference(int id)
        {
            return _preferenceRepository.EditPreference(id);
        }

        public bool UpdatePreference(int PreferenceId, string Preference, int userId, int ImpersonateUserId)
        {
            return _preferenceRepository.UpdatePreference(PreferenceId, Preference, userId, ImpersonateUserId);
        }
        public bool DeletePreference(string name, int userId, int ImpersonateUserId)
        {
            return _preferenceRepository.DeletePreference(name, userId, ImpersonateUserId);
        }
        public bool DeleteAllPreference(List<smSubIndustry> list, int userId, int ImpersonateUserId)
        {
            return _preferenceRepository.DeleteAllPreference(list, userId, ImpersonateUserId);
        }

    }
}
