using AppInterfaces.Infrastructure;
using AppInterfaces.Repository;
using Core.Domain;
using Facebook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class PostRepository : BaseRepository<smSocialMediaProfile>, IPostRepository
    {
        #region ctor
        public PostRepository(IAppUnitOfWork uow)
            : base(uow)
        {


        }
        #endregion
        public class datelist
        {
            public DateTime scheduleDate { get; set; }
            public DateTime LocalDate { get; set; }
        }
        #region public methods
        public List<smSocialMediaProfile> GetSocialMediaAccounts(int UserId, List<int> Ids)
        {
            List<smSocialMediaProfile> list = new List<smSocialMediaProfile>();
            foreach (var item in Ids)
            {
                smSocialMediaProfile data = Context.Set<smSocialMediaProfile>().Where(x => x.UserId == UserId && x.IsDeleted == false && x.Fid == item).FirstOrDefault();
                list.Add(data);
            }
            return list;
        }

        public smPost SavePublishingPostDetails(List<string> SelectedPreferences, string SocialMedia, string GroupId, smSocialMediaProfile profile, string ImageArray, string TextMessage, DateTime timenew, int userId, int ImpersonateUserId, DateTime localtime)
        {


            #region Find latest content from content library ====
            //COnvert String List Into integer...........
            List<int> SelectedPreference = SelectedPreferences.Select(int.Parse).ToList();
            //Find industryId using subindustry
            int aSubIndustryId = SelectedPreference[0];
            var IndustryId = Context.Set<smSubIndustry>().Where(x => x.SubIndustryId.Equals(aSubIndustryId)).Select(x => x.IndustryId).FirstOrDefault();
            //Find industryId using subindustry

            //// Find Subindustry List using subIndustry iDs
            var list = Context.Set<smSubIndustry>().Where(x => SelectedPreference.Contains(x.SubIndustryId)).ToList();
            //-------------------------------------
            //// Get data from content library on behalf of social media that is activated by user
            var myvar = from a in Context.Set<smContentLibrary>().Where(x => x.CategoryId == IndustryId && x.IsActive == true && !x.IsDeleted)
                        join b in Context.Set<smSocialMediaProfile>().Where(x => x.UserId == userId && x.IsActive == true) on a.SocialMedia equals b.SocialMedia
                        select new { a };
            List<smContentLibrary> list2 = new List<smContentLibrary>();
            foreach (var item in myvar)
            {
                list2.Add(item.a);
            }
            //------------------------------------------

            ///////////// Filter COntent Library Data According to selected preference
            List<smContentLibrary> source = new List<smContentLibrary>();
            if (list.Count != 0)
            {
                var preferenceList = list.Select(x => x.SubIndustryName).ToList();
                source = list2.Where(m => preferenceList.Contains(m.SubIndustryName)).ToList();
            }
            List<smContentLibrary> second = new List<smContentLibrary>();
            List<smContentLibrary> list6 = new List<smContentLibrary>(); /// jai kaali ma 
            var AddedLists = Context.Set<smPost>().Where(x => x.UserId == userId && x.IsDeleted == false).Select(x => x.ContentId).ToList();
            //   var OldLists = source.Where(e => AddedLists.Contains(e.ContentId));    //// POsted list
            var NewLists = source.Where(e => !AddedLists.Contains(e.ContentId)).OrderByDescending(x => x.CreatedDate);   /// New COntent that is not posted yet on any social media.
            var ListForSyncWithCaledar = NewLists.GroupBy(x => x.GroupId.Trim()).ToList().Take(1);
            // Filter COntent Library Data According to selected preference
            //Time to save data  .,,,,,,.....................         

            #endregion
            #region Data Manupulation and save into db accordingly

            List<datelist> DateList = new List<datelist>()           
                                 {
                                     new datelist{ LocalDate=localtime, scheduleDate=timenew},
                                     new datelist{ LocalDate=localtime.AddDays(7), scheduleDate=timenew.AddDays(7)},
                                     new datelist{ LocalDate=localtime.AddDays(14), scheduleDate=timenew.AddDays(14)},
                                     new datelist{ LocalDate=localtime.AddDays(21), scheduleDate=timenew.AddDays(21)},
                                };
            smPost smpost = new smPost();
            var flag = 0;
            var schedule = 0;
            try
            {
                if (NewLists.Count() != 0)
                {
                    foreach (var items in ListForSyncWithCaledar)
                    {

                        #region    //// Check whether data exist or not  ===========
                        smpost = Context.Set<smPost>().Where(x => x.PostDate == timenew && x.UserId == userId && x.ContentCreatedId == 2 && x.IsDeleted == false && x.IsPosted == false).FirstOrDefault();
                        if (smpost != null)
                        {
                            if (items.Select(x => x.CategoryId).Contains(smpost.ContentId))
                            {
                                return smpost;
                            }
                            else
                            {
                                flag = 1;
                            }
                        }
                        else
                        {
                            smpost = new smPost();
                            flag = 2;
                        }
                        smScheduleEvents events = Context.Set<smScheduleEvents>().Where(x => x.ScheduleTime == timenew && x.UserId == userId && x.ContentCreatedId == 2 && x.IsDeleted == false).FirstOrDefault();
                        if (events != null)
                        {
                            if (items.Select(x => x.CategoryId).Contains(events.ContentId))
                            {
                                return smpost;
                            }
                            else
                            {
                                schedule = 1;
                            }
                        }
                        else
                        {
                            events = new smScheduleEvents();
                            schedule = 2;
                        }

                        #endregion
                        foreach (var dates in DateList)  ///// save upto  next 4 weak 
                        {
                            #region Sync data into scheduler calendar 
                            TimeZone localZone = TimeZone.CurrentTimeZone;
                            TimeSpan localOffset = localZone.GetUtcOffset(DateTime.Now);
                            int offset = Convert.ToInt32(localOffset.TotalMinutes.ToString());
                            /// Sync With Schedular Calendar           
                        
                            DateTime time = Convert.ToDateTime(timenew);
                            DateTime now = DateTime.Now;
                            DateTime time3 = DateTime.Now;
                            new List<smScheduleEvents>();
                            // int offset = Convert.ToInt32(TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow));
                            DateTime dt = FromUTCData(new DateTime?(time), offset);
                            string[] strArray = items.Select(x => x.Title).FirstOrDefault().Split(new char[] { '/' });
                      
                            events.Title = strArray[0].Trim();
                            events.ContentId = items.Select(x => x.ContentId).FirstOrDefault();
                            events.UserId = userId;
                            events.ScheduleTime = dates.scheduleDate;
                            events.LocalTime = dates.LocalDate;
                            events.Evnt_Id = null;
                            events.IsFacebook = items.Any(x => x.SocialMedia.Contains("Facebook"));
                            events.IsLinkedIn = items.Any(x => x.SocialMedia.Contains("LinkedIn"));
                            events.IsTwitter = items.Any(x => x.SocialMedia.Contains("Twitter"));
                            if (ImpersonateUserId != 0)
                            {
                                events.CreatedBy = ImpersonateUserId;
                            }
                            else
                            {
                                events.CreatedBy = userId;
                            }
                            events.CreatedDate = DateTime.UtcNow;
                            events.ContentCreatedId = 2;

                            if (schedule == 1)
                            {
                                Context.SaveChanges();
                            }
                            else
                            {
                                Context.Set<smScheduleEvents>().Add(events);
                                Context.SaveChanges();
                            }
                            int EventId = events.EventId;
                            #endregion Sync data into scheduler calendar
                            #region Save data into smpost  table 
                            foreach (var item in items)
                            {
                             
                                DateTime scheduledDateTime = DateTime.UtcNow;
                                TimeZoneInfo tz = TimeZoneInfo.Local;
                                var timeZoneId = tz.Id;
                                // Convert scheduledtime to utc time
                                TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                                smpost.SocialMediaProfileId = Context.Set<smSocialMediaProfile>().Where(x => x.UserId == userId && x.SocialMedia.ToLower() == item.SocialMedia).Select(x => x.Fid).FirstOrDefault();
                                smpost.UserId = userId;
                                smpost.EventId = EventId;
                                smpost.SocialMedia = item.SocialMedia;
                                if (item.Heading == null)
                                {
                                    item.Heading = item.Title;
                                }
                                smpost.Caption = item.Heading;
                                smpost.ContentId = item.ContentId;
                                smpost.Description = item.Description;
                                smpost.Name = item.Title;
                                smpost.Url = item.Url;
                                smpost.UniquePostId = RandomString(8);
                                if (string.IsNullOrEmpty(ImageArray))
                                {
                                    smpost.ImageUrl = item.ImageUrl;

                                }
                                smpost.PostDate = dates.scheduleDate;
                                smpost.ModifiedDate = scheduledDateTime;
                                smpost.IsPosted = false;
                                if (ImpersonateUserId != 0)
                                {
                                    smpost.CreatedBy = ImpersonateUserId;
                                }
                                else
                                {
                                    smpost.CreatedBy = userId;
                                }
                                smpost.CreatedDate = DateTime.UtcNow;
                                smpost.PostType = 2;
                                smpost.IsActive = true;
                                smpost.IsDeleted = false;
                                smpost.ContentCreatedId = 2; //  ====== Post Created From Auto post scheduler ....                              

                                if (flag==1)
                                {
                                   
                                    Context.SaveChanges();
                                }
                                else
                                {
                                    Context.Set<smPost>().Add(smpost);
                                    Context.SaveChanges();
                                }



                            }
                                #endregion
                        }
                    }
                }
            #endregion
                return smpost;
            }
            catch (Exception ex)
            {

            }
            return smpost;
        }
        public smPost AutoSavePublishingPostDetails(List<string> SelectedPreferences, string SocialMedia, string GroupId, smSocialMediaProfile profile, string ImageArray, string TextMessage, DateTime timenew, int userId, int ImpersonateUserId, DateTime localtime)
        {


            #region Find latest content from content library ====
            //COnvert String List Into integer...........
            List<int> SelectedPreference = SelectedPreferences.Select(int.Parse).ToList();
            //Find industryId using subindustry
            int aSubIndustryId = SelectedPreference[0];
            var IndustryId = Context.Set<smSubIndustry>().Where(x => x.SubIndustryId.Equals(aSubIndustryId)).Select(x => x.IndustryId).FirstOrDefault();
            //Find industryId using subindustry

            //// Find Subindustry List using subIndustry iDs
            var list = Context.Set<smSubIndustry>().Where(x => SelectedPreference.Contains(x.SubIndustryId)).ToList();
            //-------------------------------------
            //// Get data from content library on behalf of social media that is activated by user
            var myvar = from a in Context.Set<smContentLibrary>().Where(x => x.CategoryId == IndustryId && x.IsActive == true && !x.IsDeleted)
                        join b in Context.Set<smSocialMediaProfile>().Where(x => x.UserId == userId && x.IsActive == true) on a.SocialMedia equals b.SocialMedia
                        select new { a };
            List<smContentLibrary> list2 = new List<smContentLibrary>();
            foreach (var item in myvar)
            {
                list2.Add(item.a);
            }
            //------------------------------------------

            ///////////// Filter COntent Library Data According to selected preference
            List<smContentLibrary> source = new List<smContentLibrary>();
            if (list.Count != 0)
            {
                var preferenceList = list.Select(x => x.SubIndustryName).ToList();
                source = list2.Where(m => preferenceList.Contains(m.SubIndustryName)).ToList();
            }
            List<smContentLibrary> second = new List<smContentLibrary>();
            List<smContentLibrary> list6 = new List<smContentLibrary>(); /// jai kaali ma 
            var AddedLists = Context.Set<smPost>().Where(x => x.UserId == userId && x.IsDeleted == false).Select(x => x.ContentId).ToList();
            //   var OldLists = source.Where(e => AddedLists.Contains(e.ContentId));    //// POsted list
            var NewLists = source.Where(e => !AddedLists.Contains(e.ContentId)).OrderByDescending(x => x.CreatedDate);   /// New COntent that is not posted yet on any social media.
            var ListForSyncWithCaledar = NewLists.GroupBy(x => x.GroupId.Trim()).ToList().Take(1);
            // Filter COntent Library Data According to selected preference
            //Time to save data  .,,,,,,.....................         

            #endregion
            #region Data Manupulation and save into db accordingly

            List<datelist> DateList = new List<datelist>()           
                                 {
                                     new datelist{ LocalDate=localtime, scheduleDate=timenew},
                                     new datelist{ LocalDate=localtime.AddDays(7), scheduleDate=timenew.AddDays(7)},
                                     new datelist{ LocalDate=localtime.AddDays(14), scheduleDate=timenew.AddDays(14)},
                                     new datelist{ LocalDate=localtime.AddDays(21), scheduleDate=timenew.AddDays(21)},
                                };
            smPost smpost = new smPost();
            var flag = 0;
            var schedule = 0;
            try
            {
                if (NewLists.Count() != 0)
                {
                    foreach (var items in ListForSyncWithCaledar)
                    {

                        #region    //// Check whether data exist or not  ===========
                        smpost = Context.Set<smPost>().Where(x => x.PostDate.Year == timenew.Year && x.PostDate.Month == timenew.Month && x.PostDate.Day == timenew.Day && x.UserId == userId && x.ContentCreatedId == 2 && x.IsDeleted == false && x.IsPosted == false).FirstOrDefault();
                        if (smpost != null)
                        {
                            if (items.Select(x => x.ContentId).Contains(smpost.ContentId) || items.Select(x => x.ContentId).FirstOrDefault() <= smpost.ContentId)
                            {
                                return smpost;
                            }
                            else
                            {
                                flag = 1;
                            }
                        }
                        else
                        {
                            smpost = new smPost();
                            flag = 2;
                        }
                        smScheduleEvents events = Context.Set<smScheduleEvents>().Where(x => x.ScheduleTime.Year == timenew.Year && x.ScheduleTime.Month == timenew.Month && x.ScheduleTime.Day == timenew.Day && x.UserId == userId && x.ContentCreatedId == 2 && x.IsDeleted == false).FirstOrDefault();
                        if (events != null)
                        {
                            if (items.Select(x => x.CategoryId).Contains(events.ContentId))
                            {
                                return smpost;
                            }
                            else
                            {
                                schedule = 1;
                            }
                        }
                        else
                        {
                            events = new smScheduleEvents();
                            schedule = 2;
                        }

                        #endregion
                        //foreach (var dates in DateList)  ///// save upto  next 4 weak 
                        //{
                            #region Sync data into scheduler calendar 
                            TimeZone localZone = TimeZone.CurrentTimeZone;
                            TimeSpan localOffset = localZone.GetUtcOffset(DateTime.Now);
                            int offset = Convert.ToInt32(localOffset.TotalMinutes.ToString());
                            /// Sync With Schedular Calendar           
                        
                            DateTime time = Convert.ToDateTime(timenew);
                            DateTime now = DateTime.Now;
                            DateTime time3 = DateTime.Now;
                            new List<smScheduleEvents>();
                            // int offset = Convert.ToInt32(TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow));
                            DateTime dt = FromUTCData(new DateTime?(time), offset);
                            string[] strArray = items.Select(x => x.Title).FirstOrDefault().Split(new char[] { '/' });
                      
                            events.Title = strArray[0].Trim();
                            events.ContentId = items.Select(x => x.ContentId).FirstOrDefault();
                            events.UserId = userId;
                            events.ScheduleTime = timenew;
                            events.LocalTime = localtime;
                            events.Evnt_Id = null;
                            events.IsFacebook = items.Any(x => x.SocialMedia.Contains("Facebook"));
                            events.IsLinkedIn = items.Any(x => x.SocialMedia.Contains("LinkedIn"));
                            events.IsTwitter = items.Any(x => x.SocialMedia.Contains("Twitter"));
                            if (ImpersonateUserId != 0)
                            {
                                events.CreatedBy = ImpersonateUserId;
                            }
                            else
                            {
                                events.CreatedBy = userId;
                            }
                            events.CreatedDate = DateTime.UtcNow;
                            events.ContentCreatedId = 2;

                            if (schedule == 1)
                            {
                                Context.SaveChanges();
                            }
                            else
                            {
                                Context.Set<smScheduleEvents>().Add(events);
                                Context.SaveChanges();
                            }
                            int EventId = events.EventId;
                            #endregion Sync data into scheduler calendar
                            #region Save data into smpost  table 
                            foreach (var item in items)
                            {
                             
                                DateTime scheduledDateTime = DateTime.UtcNow;
                                TimeZoneInfo tz = TimeZoneInfo.Local;
                                var timeZoneId = tz.Id;
                                // Convert scheduledtime to utc time
                                TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                                smpost.SocialMediaProfileId = Context.Set<smSocialMediaProfile>().Where(x => x.UserId == userId && x.SocialMedia.ToLower() == item.SocialMedia).Select(x => x.Fid).FirstOrDefault();
                                smpost.UserId = userId;
                                smpost.EventId = EventId;
                                smpost.SocialMedia = item.SocialMedia;
                                if (item.Heading == null)
                                {
                                    item.Heading = item.Title;
                                }
                                smpost.Caption = item.Heading;
                                smpost.ContentId = item.ContentId;
                                smpost.Description = item.Description;
                                smpost.Name = item.Title;
                                smpost.Url = item.Url;
                                smpost.UniquePostId = RandomString(8);
                                if (string.IsNullOrEmpty(ImageArray))
                                {
                                    smpost.ImageUrl = item.ImageUrl;

                                }
                                smpost.PostDate = timenew;
                                smpost.ModifiedDate = scheduledDateTime;
                                smpost.IsPosted = false;
                                if (ImpersonateUserId != 0)
                                {
                                    smpost.CreatedBy = ImpersonateUserId;
                                }
                                else
                                {
                                    smpost.CreatedBy = userId;
                                }
                                smpost.CreatedDate = DateTime.UtcNow;
                                smpost.PostType = 2;
                                smpost.IsActive = true;
                                smpost.IsDeleted = false;
                                smpost.ContentCreatedId = 2; //  ====== Post Created From Auto post scheduler ....                              

                                if (flag==1)
                                {
                                   
                                    Context.SaveChanges();
                                }
                                else
                                {
                                    Context.Set<smPost>().Add(smpost);
                                    Context.SaveChanges();
                                }



                            }
                                #endregion
                        
                    }
                }
            #endregion
                return smpost;
            }
            catch (Exception ex)
            {

            }
            return smpost;
        }
        
        public static DateTime FromUTCData(DateTime? dt, int timezoneOffset)
        {
            DateTime newDate = dt.Value + new TimeSpan(timezoneOffset / 60, timezoneOffset % 60, 0);
            return newDate;
        }
        public smPost SavePostDetails(smSocialMediaProfile data, string Image, int UserId, string description, string post, int? postType, DateTime? scheduleDate, int ImpersonateUserId, string title, string heading, string link, string PostMethod)
        {
            smPost smpost = new smPost();
            try
            {
                DateTime scheduledDateTime = DateTime.UtcNow;
                TimeZoneInfo tz = TimeZoneInfo.Local;
                var timeZoneId = tz.Id;
                // Convert scheduledtime to utc time
                TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                smpost.SocialMediaProfileId = data.Fid;
                smpost.UserId = UserId;
                smpost.SocialMedia = data.SocialMedia;
                //smpost.Caption = param.caption;
                smpost.Name = title;
                smpost.Caption = heading;
                smpost.UniquePostId = RandomString(8);
                if (post.ToLower() != "image")
                {
                    smpost.Description = description;
                }
                else
                {
                    smpost.Description = description;
                }
                smpost.Url = link;
                if (Image != string.Empty)
                {
                    //if (Image.Contains('*'))
                    //{
                    //     Image = Image.Split('*')[1];
                    //     smpost.ImageUrl = "/Images/WallImages/ContentImages/" + Image;
                    //}

                    if (PostMethod == "Republish")
                    {
                        smpost.ImageUrl = Image;
                    }

                    else
                    {

                        if (Uri.IsWellFormedUriString(Image, UriKind.RelativeOrAbsolute))
                        {
                            smpost.ImageUrl = Image;
                        }
                        else
                        {
                            smpost.ImageUrl = "/Images/WallImages/" + UserId + "/" + Image;
                        }

                    }
                }
                if (postType == 1)
                {
                    smpost.PostDate = DateTime.UtcNow;
                    smpost.ModifiedDate = DateTime.UtcNow;
                    smpost.IsPosted = true;
                }
                //else if (postType == 2 )
                else
                {
                    //scheduledDateTime = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(scheduleDate), zone);
                    //scheduledDateTime = scheduleDate;
                    //smpost.PostDate = scheduledDateTime;
                    smpost.PostDate = scheduleDate ?? DateTime.UtcNow;
                    smpost.ModifiedDate = scheduledDateTime;
                    smpost.IsPosted = false;
                }
                if (ImpersonateUserId != 0)
                {
                    smpost.CreatedBy = ImpersonateUserId;
                }
                else
                {
                    smpost.CreatedBy = UserId;
                }
                smpost.CreatedDate = DateTime.UtcNow;
                smpost.PostType = postType;
                smpost.IsActive = true;
                smpost.IsDeleted = false;
                Context.Set<smPost>().Add(smpost);
                Context.SaveChanges();
                return smpost;
            }
            catch (Exception ex)
            {

            }
            return smpost;
        }
        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public smPost SaveTwitterPost(smSocialMediaProfile data, string imagePath, string link, string Title, int userId, string message, string post, int? postType, DateTime? scheduleDate, int ImpersonateUserId, string PostMethod)
        {
            smPost smpost = new smPost();
            try
            {
                DateTime scheduledDateTime = DateTime.UtcNow;
                TimeZoneInfo tz = TimeZoneInfo.Local;
                var timeZoneId = tz.Id;
                // Convert scheduledtime to utc time
                TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                smpost.SocialMediaProfileId = data.Fid;
                smpost.UserId = userId;
                smpost.SocialMedia = data.SocialMedia;
                smpost.Name = Title;
                // smpost.Caption = param.caption;
                smpost.Url = link;
                if (imagePath != string.Empty)
                {

                    if (PostMethod == "Republish")
                    {
                        smpost.ImageUrl = imagePath;
                    }
                    //if (imagePath.Contains('*'))
                    //{
                    //    imagePath = imagePath.Split('*')[1];
                    //    smpost.ImageUrl = "/Images/WallImages/ContentImages/" + imagePath;
                    //}
                    else
                    {
                        smpost.ImageUrl = "/Images/WallImages/" + userId + "/" + imagePath;
                    }
                }
                smpost.Description = message;


                if (postType == 1)
                {
                    smpost.PostDate = DateTime.UtcNow;
                    smpost.ModifiedDate = DateTime.UtcNow;
                    smpost.IsPosted = true;
                }
                // if (postType == 2)
                else
                {
                    //scheduledDateTime = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(scheduleDate), zone);
                    //smpost.PostDate = scheduledDateTime;
                    smpost.PostDate = scheduleDate ?? DateTime.UtcNow;
                    smpost.ModifiedDate = scheduledDateTime;
                    smpost.IsPosted = false;
                }
                if (ImpersonateUserId != 0)
                {
                    smpost.CreatedBy = ImpersonateUserId;
                }
                else
                {
                    smpost.CreatedBy = userId;
                }
                smpost.CreatedDate = DateTime.UtcNow;
                smpost.PostType = postType;
                smpost.IsActive = true;
                smpost.IsDeleted = false;
                Context.Set<smPost>().Add(smpost);
                Context.SaveChanges();
                return smpost;
            }
            catch (Exception ex)
            {

            }
            return smpost;
        }

        public void SaveTwitterPostDetails(int PostId, int userId, string post, string twitterId)
        {
            try
            {
                smTwitterPostDetails twitterPostDetails = new smTwitterPostDetails();
                twitterPostDetails.PostId = PostId;
                twitterPostDetails.Type = post;
                twitterPostDetails.UserId = userId;
                twitterPostDetails.Partner_Id = 0;
                twitterPostDetails.TwitterId = twitterId;
                twitterPostDetails.CreatedDate = DateTime.UtcNow;
                Context.Set<smTwitterPostDetails>().Add(twitterPostDetails);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }

        public void SaveFacebookPostDetails(int postId, int userId, string type, string fbId, string fbPostId)
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

        public smPost SaveLinkedInPost(smSocialMediaProfile data, string Image, int UserId, string title, string url, string message, string post, int? postType, DateTime? scheduledPost, int ImpersonateUserId, string heading, string PostMethod)
        {
            smPost smpost = new smPost();
            try
            {
                DateTime scheduledDateTime = DateTime.UtcNow;
                TimeZoneInfo tz = TimeZoneInfo.Local;
                var timeZoneId = tz.Id;
                // Convert scheduledtime to utc time
                TimeZoneInfo zone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                smpost.SocialMediaProfileId = data.Fid;
                smpost.UserId = UserId;
                smpost.SocialMedia = data.SocialMedia;
                smpost.Caption = heading;
                smpost.Name = title;
                smpost.Url = url;
                smpost.Description = message;
                if (Image != string.Empty)
                {
                    if (PostMethod == "Republish")
                    {
                        smpost.ImageUrl = Image;
                    }
                    //if (Image.Contains('*'))
                    //{
                    //    Image = Image.Split('*')[1];
                    //    smpost.ImageUrl = "/Images/WallImages/ContentImages/" + Image;
                    //}
                    else
                    {

                        if (Uri.IsWellFormedUriString(Image, UriKind.RelativeOrAbsolute))
                        {
                            smpost.ImageUrl = Image;
                        }
                        else
                        {
                            smpost.ImageUrl = "/Images/WallImages/" + UserId + "/" + Image;
                        }

                    }
                }

                if (postType == 1)
                {
                    smpost.PostDate = DateTime.UtcNow;
                    smpost.ModifiedDate = DateTime.UtcNow;
                    smpost.IsPosted = true;
                }
                //if (postType == 2)
                else
                {
                    //scheduledDateTime = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(scheduledPost), zone);
                    //smpost.PostDate = scheduledDateTime;
                    smpost.PostDate = scheduledPost ?? DateTime.UtcNow;
                    smpost.ModifiedDate = scheduledDateTime;
                    smpost.IsPosted = false;
                }
                if (ImpersonateUserId != 0)
                {
                    smpost.CreatedBy = ImpersonateUserId;
                }
                else
                {
                    smpost.CreatedBy = UserId;
                }
                smpost.CreatedDate = DateTime.UtcNow;
                smpost.PostType = postType;
                smpost.IsActive = true;
                smpost.IsDeleted = false;
                Context.Set<smPost>().Add(smpost);
                Context.SaveChanges();
                return smpost;
            }
            catch (Exception ex)
            {

            }
            return smpost;
        }

        public void SaveLinkedInPostDetails(int postId, int userId, string type, string key, string url)
        {
            try
            {
                smLinkedInPostDetails lIPostDetails = new smLinkedInPostDetails();
                lIPostDetails.PostId = postId;
                lIPostDetails.Type = type;
                lIPostDetails.UserId = userId;
                lIPostDetails.LIinkedInId = key;
                lIPostDetails.LinkedInUrl = url;
                lIPostDetails.Partner_Id = 0;
                lIPostDetails.AddedDate = DateTime.UtcNow;
                Context.Set<smLinkedInPostDetails>().Add(lIPostDetails);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
        public List<smFacebookPostDetails> GetAllFacebookPostDetails()
        {
            var postdetails = Context.Set<smFacebookPostDetails>().ToList();
            return postdetails;
        }

        public List<smTwitterPostDetails> GetAllTwitterPostDetails()
        {
            var postdetails = Context.Set<smTwitterPostDetails>().ToList();
            return postdetails;
        }

        public List<smLinkedInPostDetails> GetAllLinkedInPostDetails()
        {
            var postdetails = Context.Set<smLinkedInPostDetails>().ToList();
            return postdetails;
        }


        public smSocialMediaProfile GetSocilaMediaAccountByName(string Name)
        {
            var postdetails = Context.Set<smSocialMediaProfile>().Where(x => x.SocialMedia.ToLower() == Name.ToLower()).FirstOrDefault();
            return postdetails;
        }


        public smPost SaveScheduledPosts(smContentLibrary post, DateTime dt, int UserId, int ImpersonateUserId, int Fid, string socialarr, int EventId)
        {
            smPost entity = new smPost();
            try
            {
                List<smContentLibrary> data = Context.Set<smContentLibrary>().Where(x => x.GroupId.ToLower().Trim() == post.GroupId.ToLower().Trim()).Select(y => y).ToList();
                var strArra = socialarr.Split(',').ToList();
                data = data.Where(x => strArra.Any(l => x.SocialMedia.ToLower() == l.ToLower())).ToList();
                List<smPost> FinalData = data.Select(s => new smPost

                {
                    UserId = UserId,
                    EventId = EventId,
                    Caption = s.Heading,
                    Name = s.Title.Replace("’", "'"),
                    Url = s.Url,
                    UniquePostId = RandomString(8),
                    ImageUrl = s.ImageUrl,

                    Description = s.Description.Replace("’", "'"),
                    PostDate = dt,
                    ModifiedDate = DateTime.UtcNow,
                    IsPosted = false,
                    CreatedDate = new DateTime?(DateTime.UtcNow),
                    PostType = 2,
                    IsActive = true,
                    IsDeleted = false,
                    SocialMedia = s.SocialMedia,
                    CreatedBy = ImpersonateUserId == 0 ? UserId : ImpersonateUserId,
                    ContentId = s.ContentId,
                    SocialMediaProfileId = Context.Set<smSocialMediaProfile>().Where(x => x.SocialMedia.ToLower() == s.SocialMedia.ToLower() && x.UserId == UserId).Select(x => x.Fid).FirstOrDefault(),


                }).ToList();
                Context.Set<smPost>().AddRange(FinalData);
                Context.SaveChanges();
            }
            catch (Exception)
            {
            }
            return entity;
        }


        public bool DeleteScheduledPosts(int id, int UserId, int ImpersonateUserId)
        {
            bool status = false;
            try
            {
                var objUser = Context.Set<smScheduleEvents>().Where(x => x.EventId == id).FirstOrDefault();
                objUser.IsDeleted = true;
                objUser.DeletedBy = UserId;
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
        public List<smPost> GetPostsBYEventId(int eventId)
        {
            return (from x in Context.Set<smPost>()
                    where (x.EventId == eventId) && !x.IsDeleted
                    select x).ToList<smPost>();
        }



        public smPost UpdateScheduledPosts(int PostId, DateTime time, int UserId, int ImpersonateUserId, int eventID)
        {
            smPost post = (from x in base.Context.Set<smPost>()
                           where (x.PostId == PostId) && !x.IsDeleted
                           select x).FirstOrDefault<smPost>();
            post.PostDate = time;
            post.EventId = eventID;
            if (ImpersonateUserId != 0)
            {
                post.ModifiedBy = ImpersonateUserId;
            }
            else
            {
                post.ModifiedBy = UserId;
            }
            base.Context.SaveChanges();
            return post;
        }




        public bool DeleteScheduledEvents(int id, int UserId, int ImpersonateUserId)
        {
            try
            {
                smPost post = (from x in base.Context.Set<smPost>()
                               where x.PostId == id
                               select x).FirstOrDefault<smPost>();
                post.IsDeleted = true;
                if (ImpersonateUserId != 0)
                {
                    post.DeletedBy = ImpersonateUserId;
                }
                else
                {
                    post.DeletedBy = UserId;
                }
                post.DeletedDate = new DateTime?(DateTime.UtcNow);
                base.Context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<smPublishingTime> GetUserPublishingTime()
        {
            var data = Context.Set<smPublishingTime>().Where(x => x.IsActive == true && x.IsDeleted == false).ToList();


            data = data.Select(d =>
new smPublishingTime
{
    CreatedBy = d.CreatedBy,
    DayId = d.DayId,
    DeletedBy = d.DeletedBy,
    UserId = d.UserId,
    IsActive = d.IsActive,
    PublishingTimeId = d.PublishingTimeId,
    Time = d.Time,
    TimeStampPosted = d.TimeStampPosted,
    DeletedDate = d.DeletedDate,
    CreatedDate = d.CreatedDate,
    Day = d.DayId == 1 ? "Mon" : d.DayId == 2 ? "Tue" : d.DayId == 3 ? "Wed" : d.DayId == 4 ? "Thr" : d.DayId == 5 ? "Fri" : d.DayId == 6 ? "Sat" : "Sun"
}).ToList();
            return data;

        }


        #endregion

        #region get Page Access Token
        public List<smFacebookPageDetail> getPageAccesstoken(List<List<long>> list)
        {
            List<smFacebookPageDetail> fbpageDetail = new List<smFacebookPageDetail>();
            fbpageDetail = Context.Set<smFacebookPageDetail>().ToList();

            List<long> pageIdList = new List<long>();
            pageIdList = list.SelectMany(t => t).ToList();
            //foreach (var item in list)
            //{
            //    pageIdList = item;
            //}

            fbpageDetail = fbpageDetail.Where(x => pageIdList.Any(y => y == x.PageId)).ToList();
            // fbpageDetail = pageIdList.Select(x => fbpageDetail.Any(y => y.PageId == x)).ToList();
            return fbpageDetail;

        }
        #endregion

        #region SyncFacebookPage
        public string SyncFacebookPage(int userId)
        {
            var AccessTocken = string.Empty;
            try
            {
                AccessTocken = Context.Set<smSocialMediaProfile>().Where(x => x.UserId == userId && x.SocialMedia == "Facebook").Select(x => x.AccessToken).FirstOrDefault();


            }
            catch (Exception ex)
            {

                return "Error";
            }

            return AccessTocken;

        }
        public List<smFacebookPageDetail> SaveSyncFacebookPage(List<smFacebookPageDetail> _pageDetail, int UserId)
        {
            List<smFacebookPageDetail> newPagesList = new List<smFacebookPageDetail>();

            if (_pageDetail != null)
            {

            }
            List<smFacebookPageDetail> Oldadata = Context.Set<smFacebookPageDetail>().Where(x => x.UserId == UserId).ToList();

            if (Oldadata.Count() > 0)
            {
                _pageDetail.Select(c => { c.UserFaceBookID = Oldadata[0].UserFaceBookID; return c; }).ToList();
                newPagesList = _pageDetail.Where(b => !Oldadata.Any(a => a.PageId == b.PageId)).ToList();
                Context.Set<smFacebookPageDetail>().AddRange(newPagesList);
                Context.SaveChanges();
                return newPagesList;
            }
            else
            {
                Context.Set<smFacebookPageDetail>().AddRange(_pageDetail);
                Context.SaveChanges();
            }


            return newPagesList;


        }
        #endregion
    }
}
