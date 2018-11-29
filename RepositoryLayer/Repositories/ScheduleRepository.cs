using AppInterfaces.Infrastructure;
using AppInterfaces.Repository;
using Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class ScheduleRepository : BaseRepository<smPost>, IScheduleRepository
    {

         #region ctor
        public ScheduleRepository(IAppUnitOfWork uow)
            : base(uow)
        {


        }
        #endregion
        public List<smPost> GetAllPostList()
        {
            var post = Context.Set<smPost>().Where(x => x.IsPosted == false && x.IsDeleted == false && x.IsActive == true && (x.PostType == 2 || x.PostType == 3)).ToList();
            return post;

        }

        public List<smPost> GetAllPosts()
        {
            var post = Context.Set<smPost>().Where(x => x.IsPosted == true && x.IsDeleted == false && x.IsActive == true).OrderByDescending(x=>x.PostId).ToList();
            return post;

        }

        public List<smPost> GetAllPostListByUserId(int userId, DateTime sDate, DateTime eDate)
        {
            var post = Context.Set<smPost>().Where(x => x.IsPosted == true &&x.UserId== userId && x.PostDate>=sDate && x.PostDate<=eDate && x.IsDeleted == false && x.IsActive == true).ToList();
            return post;

        }

        public smWatermark AddWaterMark(smWatermark watermark)
        {
           var detail= GetWaterMarkDetails(watermark.UserID);
           if (detail != null)
           {
               var objdetail = Context.Set<Core.Domain.smWatermark>().Where(x => x.UserID == watermark.UserID).FirstOrDefault();
               objdetail.IsDeleted = false;
               objdetail.Margin = watermark.Margin;
               objdetail.ImagePathLogo = watermark.ImagePathLogo;
               objdetail.ImageText = watermark.ImageText;
               objdetail.Opacity = watermark.Opacity;
               objdetail.Gravity = watermark.Gravity;
               objdetail.Fontfamily = watermark.Fontfamily;
               objdetail.OutputHeight = watermark.OutputHeight;
               objdetail.OutPutType = watermark.OutPutType;
               objdetail.OutputWidth = watermark.OutputWidth;
               objdetail.TextBg = watermark.TextBg;
               objdetail.Textcolor = watermark.Textcolor;
               objdetail.TextSiz = watermark.TextSiz;
               objdetail.TextWidth = watermark.TextWidth;
               objdetail.CreatedBy = watermark.CreatedBy;
               objdetail.ModifiedDate = DateTime.UtcNow;
               Context.SaveChanges();
           }
           else
           {
               Context.Set<smWatermark>().Add(watermark);
               Context.SaveChanges();
           }          
            return watermark;
        }

        public smWatermark GetWaterMarkDetails(int userId)
        {
            var details = Context.Set<smWatermark>().Where(x => x.UserID == userId && x.IsDeleted == false).FirstOrDefault();
            return details;
        }

        public List<smSocialMediaProfile> GetAllSocialMediaAccountByUserId(int userId)
        {
            var media = Context.Set<smSocialMediaProfile>().Where(x => x.UserId == userId && x.IsDeleted == false && x.IsAccountActive == true).ToList();
            return media;
        }

        public smSocialMediaProfile GetSocialMediaAccnt(int UserId, string socialmedia)
        {
            smSocialMediaProfile data = Context.Set<smSocialMediaProfile>().Where(x => x.UserId == UserId && x.IsDeleted == false && x.SocialMedia.ToLower() == socialmedia.ToLower()).FirstOrDefault();
            return data;
        }

        public smPost GetPostById(int PostId)
        {
            var post = Context.Set<smPost>().Where(x => x.PostId == PostId && x.IsDeleted == false && x.IsActive == true).FirstOrDefault();
            return post;

        }

        public smPost GetPostByUniqueId(string UniquePostId)
        {
            var post = Context.Set<smPost>().Where(x => x.UniquePostId == UniquePostId && x.IsDeleted == false && x.IsActive == true).FirstOrDefault();
            return post;
        }

        public smPost UpdatePostDetails(smPost post)
        {    
            try{
                post.IsPosted = true;
                post.IsActive = true;
                post.IsDeleted = false;
                Context.Set<Core.Domain.smPost>().Where(x => x.PostId == post.Id).FirstOrDefault();
                Context.SaveChanges();
                return post;
            }              
          
            catch (Exception ex)
            {

            }
            return post;
        }


        public void SaveFacebookPostDetailsSchedule(int postId, int userId, string type, string fbId, string fbPostId)
        {
            try
            {
                smFacebookPostDetails fbPostDetails = new smFacebookPostDetails();
                fbPostDetails.PostId = postId;
                fbPostDetails.Type = type;
                fbPostDetails.UserId = userId;
                fbPostDetails.FBId = fbId;
                fbPostDetails.FBPostId = fbPostId;
                fbPostDetails.AddedDate = DateTime.UtcNow;
                Context.Set<smFacebookPostDetails>().Add(fbPostDetails);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }


        public smPublishingTime AddPublishingTime(TimeSpan TimeStamp, int Day, int UserId, string Time, int ImpersonateUserId)
        {
            smPublishingTime time = new smPublishingTime();
            time.DayId = Day;
            time.TimeStampPosted = TimeStamp;
            time.Time = Time;
            time.UserId = UserId;
            if (ImpersonateUserId != 0)
            {
                time.CreatedBy = ImpersonateUserId;
            }
            else
            {
                time.CreatedBy = UserId;
            }       
            time.CreatedDate = DateTime.Now;
            time.IsActive = true;
            time.IsDeleted = false;
            Context.Set<smPublishingTime>().Add(time);
            Context.SaveChanges();
            return time;
        }

       public List<smPublishingTime> GetPublishingTimeByDay(int Day, int UserId)
        {
            var post = Context.Set<smPublishingTime>().Where(x => x.DayId == Day && x.UserId == UserId && x.IsDeleted == false && x.IsActive == true).OrderBy(x=>x.TimeStampPosted).ToList();
            return post;

        }



      public List<smPublishingTime> GetAllPublishingTime(int UserId)
       {
           var post = Context.Set<smPublishingTime>().Where(x => x.UserId == UserId && x.IsDeleted == false && x.IsActive == true).ToList();
           return post;

       }


      public bool DeletePublishingTime(string id, int userId, int ImpersonateUserId)
       {
           bool status = false;
           try
           {
               int pubId = Convert.ToInt32(id);
               var objUser = Context.Set<smPublishingTime>().Where(x => x.UserId == userId && x.PublishingTimeId == pubId).FirstOrDefault();
             
               objUser.IsDeleted = true;
               objUser.IsActive = false;
               if (ImpersonateUserId != 0)
               {
                   objUser.DeletedBy = ImpersonateUserId;
               }
               else
               {
                   objUser.DeletedBy = userId;
               }             
               objUser.DeletedDate = DateTime.UtcNow;
               Context.SaveChanges();
               status = true;
           }
           catch
           {
               status = false;
           }
           return status;
       }

    }
}
