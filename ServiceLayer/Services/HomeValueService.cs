using Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using CoreEntities.CustomModels;
using AppInterfaces.Infrastructure;
using AppInterfaces.Interfaces;
using ServiceLayer.Interfaces;
using AppInterfaces.Repository;


namespace ServiceLayer.Services
{
    public class HomeValueService : IHomeValueService
    {

        IHomeValueRepository _homeValueRepository = null;
        
        public HomeValueService(IHomeValueRepository homeValueRepository)
        {
            _homeValueRepository = homeValueRepository;
        }

        public bool SaveHomeValue(smHomeValue model)
        {
           return _homeValueRepository.SaveHomeValue(model);
        }

        public smHomeValue GetHomeValue(int postId)
        {
            return _homeValueRepository.GetHomeValue(postId);
        }
    }
}
