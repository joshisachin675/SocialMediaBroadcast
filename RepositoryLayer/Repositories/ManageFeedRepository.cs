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
    public class ManageFeedRepository : BaseRepository<smRssFeeds>, IManageRssFeedRepository
    {

        #region ctor

        public ManageFeedRepository(IAppUnitOfWork uow)
            : base(uow)
        {


        }
        #endregion

        #region public methods
        public bool AddRss(string FeedName, string Feed, int UserId, Core.Domain.Users user)
        {
            bool status = false;
            try
            {
                smRssFeeds feed = new smRssFeeds();
                feed.FeedName = FeedName;
                feed.FeedUrl = Feed;
                feed.UserId = UserId;
                feed.CreatedBy = user.FirstName +" "+ user.LastName;
                feed.CreatedDate = DateTime.UtcNow;
                if (user.UserTypeId == 3)
                {
                    feed.UserType = "SuperAdmin";
                    feed.IsApproved = true;
                }
                else
                {
                    feed.UserType = "Admin";
                    feed.IsApproved = false;
                }                
                feed.IsActive = true;
                feed.IsDeleted = false;
                Context.Set<smRssFeeds>().Add(feed);
                Context.SaveChanges();
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }


        public bool DeleteRss(int id)
        {
            bool status = false;
            try
            {
                var objUser = Context.Set<smRssFeeds>().Where(x => x.FeedId == id).FirstOrDefault();
                objUser.IsDeleted = true;
                Context.SaveChanges();
                status = true;
            }
            catch
            {
                status = false;
            }
            return status;
        }


        public bool ApproveRssFeed(int FeedId)
        {
            bool status = false;
            try
            {
                var objUser = Context.Set<smRssFeeds>().Where(x => x.FeedId == FeedId).FirstOrDefault();
                objUser.IsApproved = true;
                Context.SaveChanges();
                status = true;
            }
            catch
            {
                status = false;
            }
            return status;
        }

        public smRssFeeds GetFeedById(int id)
        {
            var feed = Context.Set<smRssFeeds>().Where(x => x.FeedId == id).FirstOrDefault();
            return feed;
        }

        public smRssFeeds GetFeedByUrl(string Url)
        {
            return Context.Set<smRssFeeds>().Where(x => (x.FeedUrl.ToLower() == Url.ToLower()) && !x.IsDeleted).FirstOrDefault(); 
        }

        public smRssFeeds GetFeedByName(string Name)
        {
            return Context.Set<smRssFeeds>().Where(x => (x.FeedName.ToLower() == Name.ToLower()) && !x.IsDeleted).FirstOrDefault();
        }
     
        public List<smRssArtical> getIgnoreList(int userId)
        {
            var rss = Context.Set<smRssArtical>().Where(x => x.UserId == userId);
            return rss.ToList();
        }

        public List<smContentLibrary> getAddedData(int userId)
        {
            //var addedData
            var addedData = Context.Set<smContentLibrary>();
            return addedData.ToList();
        }


        public int IgnoreRssData(string messsage, string url, int userId, DateTime createdDate)
        {
            int flag = 0;
            try
            {
                smRssArtical content = new smRssArtical();
                content.Description = messsage;
                content.Url = url;
                content.CreatedDate = DateTime.UtcNow;
                content.UserId = userId;
                content.IsIgnored = true;
                content.IsDeleted = false;

              
                string checkIgnored = string.Empty;
                int? id =0;
                var IgnoredData = Context.Set<smRssArtical>().Where(x => x.Description == content.Description & x.IsIgnored==true);
               
                foreach (var item in IgnoredData)
                {
                    checkIgnored = item.Description;
                    id = item.UserId;
                }
                if (checkIgnored == content.Description && id==content.UserId )
                {
                    flag = 2;
                }
                else
                {
                    flag = 1;
                    Context.Set<smRssArtical>().Add(content);
                    Context.SaveChanges();
                    
                }

            }
            catch (Exception ex)
            {

                flag = 3;

            }
            return flag;
        }


        public int AddRssData(string message, string imageIds, string url, int userId, int categoryId, string socialMedia, string categoryName, DateTime CreatedDate, string textdescription, string Subindustryname, string Heading, string OriginalTitle , int FeedID)
        {
           // bool status = false;
            if (!String.IsNullOrEmpty(textdescription))
            {
                textdescription = textdescription.Replace("’", "'");
            }
            if (!String.IsNullOrEmpty(message))
            {
                message = message.Replace("’", "'");
            }
          
            int flag = 1;
            string Message = string.Empty;
            try
            {
                string image = string.Empty;
                string imagePath = string.Empty;
                if (!string.IsNullOrEmpty(imageIds))
                {
                    if (Uri.IsWellFormedUriString(imageIds, UriKind.RelativeOrAbsolute))
                    {
                        imagePath = imageIds;
                    }
                    else
                    {
                        imagePath = "/Images/WallImages/ContentImages/" + imageIds;
                    }
                  
                }
                else
                {
                    imagePath = null;
                }
                //if (imageIds.Count != 0)
                //{
                //    image = imageIds[0];
                //    if (image != string.Empty)
                //    {
                //        imagePath = image;
                //    }
                //    else
                //    {
                //        imagePath = null;
                //    }
                //}

                smContentLibrary content = new smContentLibrary();
                content.UserId = userId;
                content.SocialMedia = socialMedia;
                content.CategoryId = categoryId;
                //content.Tags = tags;
                string string1 = message.Replace("&#39;", "").Trim();
                string str = textdescription.Replace("/'", "").Trim();                
                string result = string1.TrimStart(str.ToCharArray());
              //  content.Description = result;
                content.Description = string1;
                content.Title = textdescription;
                content.Url = url;
                content.ImageUrl = imagePath;
                content.CreatedBy = userId;
                content.CreatedDate = DateTime.UtcNow;
                content.IsActive = true;
                content.IsDeleted = false;
                content.CategoryName = categoryName;
                content.SubIndustryName= Subindustryname;
                content.Heading = Heading;
                content.GroupId = RandomString(10);
                content.OriginalTitle = OriginalTitle;

               // content.CreatedDate = CreatedDate;
                string Des =string.Empty;
               //string DesCheck=string.Empty;
                int? id = 0;
                //var CheckTittle = Context.Set<smContentLibrary>().Where(x => x.Description == content.Description &  x.CreatedBy == content.UserId);
                var CheckTittle = Context.Set<smContentLibrary>().Where(x => x.Description == content.Description);

                foreach (var item in CheckTittle.ToList())
                {
                     Des= item.Description;
                     id = item.CreatedBy;
                     
                }
                if (Des == "")
                {

                    int feedID = Convert.ToInt32(FeedID);
                    var RssData = Context.Set<smRssFeeds>().Where(x => x.FeedId ==feedID).FirstOrDefault();
                    if (RssData!=null)
                    {
                        RssData.DateProcess = DateTime.UtcNow;
                    }
                    Context.Set<smContentLibrary>().Add(content);
                    Context.SaveChanges();
                    flag = 1;
                }

                else
                {
                    flag = 2;
                }
    
            }
            catch
            {
                flag = 3;
               // status = false;
            }


            return flag;
        }
        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        #endregion

    }
}
