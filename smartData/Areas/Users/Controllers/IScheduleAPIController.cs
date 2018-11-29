using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smartData.Areas.Users.Controllers
{
   public interface IScheduleAPIController
    {
       List<smPost> GetAllPostList();
       List<smPost> GetAllPostListByUserId(int userId, DateTime sDate, DateTime eDate);
       smWatermark AddWaterMark(smWatermark watermark);
       smWatermark GetWaterMarkDetails(int userId);
       List<smSocialMediaProfile> GetAllSocialMediaAccountByUserId(int userId);
       smSocialMediaProfile GetSocialMediaAccnt(int userId, string socialmedia);
       smPost GetPostById(int PostId);
       smPost UpdatePostDetails(smPost post);
       void SaveFacebookPostDetailsSchedule(int postId, int userId, string type, string fbId, string fbPostId);
       
    }
}
