using Core.Domain;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace smartData.Areas.Users.Controllers
{
    public class ScheduleAPIController : ApiController, IScheduleAPIController
    {
           IScheduleService _scheduleService;

        #region constructor
           public ScheduleAPIController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
            //System.Net.Http.Headers.contContentType = new MediaTypeHeaderValue("application/json");
        }
        #endregion

        public List<smPost> GetAllPostList()
        {
            return _scheduleService.GetAllPostList();
        }

        public List<smPost> GetAllPostListByUserId(int userId, DateTime sDate, DateTime eDate)
        {
            return _scheduleService.GetAllPostListByUserId(userId, sDate, eDate);
        }

       public smWatermark AddWaterMark(smWatermark watermark)
        {
            return _scheduleService.AddWaterMark(watermark);
        }

       public smWatermark GetWaterMarkDetails(int userId)
       {
           return _scheduleService.GetWaterMarkDetails(userId);
       }

        public List<smSocialMediaProfile> GetAllSocialMediaAccountByUserId(int userId)
        {
            return _scheduleService.GetAllSocialMediaAccountByUserId(userId);
        }

        public smSocialMediaProfile GetSocialMediaAccnt(int userId, string socialmedia)
        {
            return _scheduleService.GetSocialMediaAccnt(userId, socialmedia);
        }

        public smPost GetPostById(int PostId)
        {
            return _scheduleService.GetPostById(PostId);
        }

        public smPost UpdatePostDetails(smPost post)
        {
            return _scheduleService.UpdatePostDetails(post);
        }

       public void SaveFacebookPostDetailsSchedule(int postId, int userId, string type, string fbId, string fbPostId)
        {
            _scheduleService.SaveFacebookPostDetailsSchedule(postId, userId, type, fbId, fbPostId);
        }

    }
}
