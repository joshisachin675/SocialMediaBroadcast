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
   public class ScheduleService : IScheduleService
    {
        IScheduleRepository _scheduleRepository = null;
        #region ctor
        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
        }
        #endregion

       public List<smPost> GetAllPostList()
       {
           return _scheduleRepository.GetAllPostList();
       }

       public List<smPost> GetAllPosts()
       {
           return _scheduleRepository.GetAllPosts();
       }

       public List<smPost> GetAllPostListByUserId(int userId, DateTime sDate, DateTime eDate)
       {
           return _scheduleRepository.GetAllPostListByUserId(userId,sDate,eDate);
       }

       public smWatermark AddWaterMark(smWatermark watermark)
       {
           return _scheduleRepository.AddWaterMark(watermark);
       }

       public smWatermark GetWaterMarkDetails(int userId)
       {
           return _scheduleRepository.GetWaterMarkDetails(userId);
       }

      public List<smSocialMediaProfile> GetAllSocialMediaAccountByUserId(int userId)
      {
          return _scheduleRepository.GetAllSocialMediaAccountByUserId(userId);
      }


       public smSocialMediaProfile GetSocialMediaAccnt(int userId, string socialmedia)
       {
           return _scheduleRepository.GetSocialMediaAccnt(userId, socialmedia);
       }

       public smPost GetPostById(int PostId)
       {
           return _scheduleRepository.GetPostById(PostId);
       }

       public smPost GetPostByUniqueId(string UniquePostId)
       {
           return _scheduleRepository.GetPostByUniqueId(UniquePostId);
       }

       public smPost UpdatePostDetails(smPost post)
       {
           return _scheduleRepository.UpdatePostDetails(post);
       }

       public void SaveFacebookPostDetailsSchedule(int postId, int userId, string type, string fbId, string fbPostId)
       {
           _scheduleRepository.SaveFacebookPostDetailsSchedule(postId, userId, type, fbId, fbPostId);
       }

       public smPublishingTime AddPublishingTime(TimeSpan TimeStamp, int Day, int UserId, string Time, int ImpersonateUserId)
       {
           return _scheduleRepository.AddPublishingTime(TimeStamp, Day, UserId, Time, ImpersonateUserId);
       }

      public List<smPublishingTime> GetPublishingTimeByDay(int Day, int UserId)
      {
          return _scheduleRepository.GetPublishingTimeByDay(Day, UserId);
      }

       public List<smPublishingTime> GetAllPublishingTime(int UserId)
      {
          return _scheduleRepository.GetAllPublishingTime(UserId);
      }

       public bool DeletePublishingTime(string id, int userId, int ImpersonateUserId)
      {
          var status = true;
          _scheduleRepository.DeletePublishingTime(id, userId, ImpersonateUserId);
          return status;
      }

    }
}
