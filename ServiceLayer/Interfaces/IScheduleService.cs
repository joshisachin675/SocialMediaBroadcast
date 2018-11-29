using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Interfaces
{
   public interface IScheduleService
    {
        List<smPost> GetAllPostList();

        List<smPost> GetAllPosts();

        List<smPost> GetAllPostListByUserId(int userId, DateTime sDate, DateTime eDate);

        smWatermark AddWaterMark(smWatermark watermark);

        smWatermark GetWaterMarkDetails(int userId);

        List<smSocialMediaProfile> GetAllSocialMediaAccountByUserId(int userId);

        smSocialMediaProfile GetSocialMediaAccnt(int UserId, string socialmedia);

        smPost GetPostById(int PostId);

        smPost GetPostByUniqueId(string UniquePostId);

        smPost UpdatePostDetails(smPost post);

        void SaveFacebookPostDetailsSchedule(int postId, int userId, string type, string fbId, string fbPostId);

        smPublishingTime AddPublishingTime(TimeSpan TimeStamp, int Day, int UserId, string Time, int ImpersonateUserId);

        List<smPublishingTime> GetPublishingTimeByDay(int Day, int UserId);

        List<smPublishingTime> GetAllPublishingTime(int UserId);

        bool DeletePublishingTime(string id, int userId, int ImpersonateUserId);


    }
}
