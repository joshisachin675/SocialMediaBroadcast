using Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.Entity;
using CoreEntities.CustomModels;
using AppInterfaces.Infrastructure;
using AppInterfaces.Interfaces;
using AppInterfaces.Repository;


namespace RepositoryLayer.Repositories
{
    public class HomeValueRepository:BaseRepository<smHomeValue>,  IHomeValueRepository
    {

        public HomeValueRepository(IAppUnitOfWork uow)
            : base(uow)
        {


        }

        public bool SaveHomeValue(smHomeValue model)
        {
            var homeValue=    Context.Set<smHomeValue>().FirstOrDefault(x => x.PostId == model.PostId);
            if (homeValue == null)
            {
                Context.Set<smHomeValue>().Add(model);
                
            }
            else
            {

                homeValue.FirstName = model.FirstName;
                homeValue.LastName = model.LastName;
                homeValue.Province = model.Province;
                homeValue.Unit = model.Unit;
                homeValue.EmailAddress = model.EmailAddress;
                homeValue.PhoneNumber = model.PhoneNumber;
                homeValue.Address = model.Address;
                homeValue.StreetAddress = model.StreetAddress;
                homeValue.TimePeriodId = model.TimePeriodId;
                homeValue.Notify = model.Notify;
                homeValue.PostalCode = model.PostalCode;
                homeValue.City = model.City;
                homeValue.IsCompleted = string.IsNullOrEmpty(model.EmailAddress) ? false : true;

            }

            Context.SaveChanges();
            return true;
        }


        public smHomeValue GetHomeValue(int postId)
        {
             return Context.Set<smHomeValue>().FirstOrDefault(x => x.PostId == postId);
        }
    }
}
