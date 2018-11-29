using AppInterfaces.Infrastructure;
using AppInterfaces.Repository;
using Core.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repositories
{
    public class HomeRepository : BaseRepository<smSocialMediaProfile>, IHomeRepository
    {
        #region ctor

        public HomeRepository(IAppUnitOfWork uow)
            : base(uow)
        {


        }
        #endregion

        #region public methods
        public string CreateUser(smSocialMediaProfile profile)
        {
            string response = string.Empty;

            if (!Context.Set<smSocialMediaProfile>().Any(x => x.Email.ToLower() == profile.Email.ToLower() && x.SocialMedia.ToLower() == profile.SocialMedia.ToLower() && x.IsDeleted != true && x.SocialMediaId == profile.SocialMediaId))
            {

                Context.Set<smSocialMediaProfile>().Add(profile);

                Context.SaveChanges();
                response = "Account added";
            }
            else
            {
                response = "Already added";
            }
            return response;
        }

        #region Save facebook page Data and DEfault Preference
        public string SaveUserFBPageDetail(smFacebookPageDetail pageDetail)
        {
            string response = string.Empty;
            if (!Context.Set<smFacebookPageDetail>().Any(x => x.UserId == pageDetail.UserId && x.PageId == pageDetail.PageId && x.PageAccessToken == pageDetail.PageAccessToken))
            {
                Context.Set<smFacebookPageDetail>().Add(pageDetail);
                Context.SaveChanges();
                response = "Added";
            }
            else
            {
                response = "Already Added";
            }
            return response;
        }
        public smFacebookDefaultPreference SetFacebookDefaultPost(smFacebookDefaultPreference fbDefault)
        {
            smFacebookDefaultPreference response = new smFacebookDefaultPreference();
            try
            {
                int SavedUserId = Context.Set<smFacebookDefaultPreference>().Where(x => x.userID == fbDefault.userID).Select(y => y.userID).FirstOrDefault();
                if (SavedUserId == 0)
                {
                    Context.Set<smFacebookDefaultPreference>().Add(fbDefault);
                    Context.SaveChanges();
                }
                else
                {
                    var obj = Context.Set<smFacebookDefaultPreference>().Where(x => x.userID == fbDefault.userID).FirstOrDefault();
                    obj.UpdatedDate = fbDefault.UpdatedDate;
                    obj.PageId = fbDefault.PageId;
                    obj.Type = fbDefault.Type;
                    Context.SaveChanges();

                }


            }
            catch (Exception ex)
            {

                throw;
            }

            return response;
        }


        public smFacebookDefaultPreference GetFacebookPereference(int userId)
        {
            return Context.Set<smFacebookDefaultPreference>().Where(x => x.userID == userId).FirstOrDefault();

        }
        #endregion

        public List<smSocialMediaProfile> GetSocialMedia(int UserId)
        {

            //Get(x => x.UserId == UserId && x.IsActive == true);
            return GetAll().Where(x => x.UserId == UserId && x.IsDeleted == false).ToList();
        }



        public string UpdateSocialMediaStatus(string accountInfo, int userId)
        {
            string response = string.Empty;
            dynamic fields = JsonConvert.DeserializeObject<dynamic>(accountInfo);
            string socialAccount = fields.Account;
            bool status = fields.Status;
            // smSocialMediaProfile account = new smSocialMediaProfile();
            // account.UserId = userId;
            // account.SocialMedia = socialAccount;
            //account.IsActive = true;
            // account.Partner_Id = 0;

            var userObject = Context.Set<smSocialMediaProfile>().Where(x => x.UserId == userId && x.SocialMedia == socialAccount);
            if (status == true)
            {
                userObject.FirstOrDefault().IsAccountActive = true;
            }
            else
            {
                userObject.FirstOrDefault().IsAccountActive = false;
            }
            Context.SaveChanges();
            response = "Account updated";

            return response;
        }

        public List<smSocialMediaProfile> GetAllSocialMediaAccounts(int limit, int offset, string order, string sort, string FirstName, string LastName, string Email, string Photo, int SocialId, int currentUserId, out int total)
        {
            AzureDBContext db = new AzureDBContext();
            // var data = from u in db.smSocialMediaProfile.AsQueryable()
            //where u.UserId == currentUserId
            // select new smSocialMediaProfile { Fid = u.Fid, FirstName = u.FirstName, LastName = u.LastName, Photo = u.Photo, Email = u.Email };
            // var data = GetAll().Where(x => x.UserId == currentUserId && x.IsActive == true).ToList();
            var data = Context.Set<smSocialMediaProfile>().Where(x => x.UserId == currentUserId && x.IsDeleted == false);
            if (!string.IsNullOrEmpty(FirstName))
            {
                data = data.Where(x => x.FirstName.Contains(FirstName));
            }
            if (!string.IsNullOrEmpty(LastName))
            {
                data = data.Where(x => x.LastName.Contains(LastName));
            }
            if (!string.IsNullOrEmpty(Photo))
            {
                data = data.Where(x => x.Photo.Contains(Photo));
            }
            if (!string.IsNullOrEmpty(Email))
            {
                data = data.Where(x => x.Email.Contains(Email));
            }
            //Order by
            GetSortedData(ref data, sort + order.ToUpper());
            total = data.Count();
            return data.Skip(offset).Take(limit).ToList();
        }

        public void GetSortedData(ref IQueryable<smSocialMediaProfile> obj, string Case)
        {
            switch (Case)
            {
                case "FirstNameASC": obj = obj.OrderBy(x => x.FirstName.ToLower());
                    break;
                case "FirstNameDESC": obj = obj.OrderByDescending(x => x.FirstName.ToLower());
                    break;
                case "LastNameASC": obj = obj.OrderBy(x => x.LastName.ToLower());
                    break;
                case "LastNameDESC": obj = obj.OrderByDescending(x => x.LastName.ToLower());
                    break;
                case "EmailASC": obj = obj.OrderBy(x => x.Email.ToLower());
                    break;
                case "EmailDESC": obj = obj.OrderByDescending(x => x.Email.ToLower());
                    break;
                default: obj = obj.OrderBy(x => x.Fid);
                    break;
            }
        }

        /// <summary>
        /// Delete clinic staff
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public bool DeleteSocialAccount(int id, int userId, int ImpersonateUserID)
        {
            bool status = false;
            try
            {
                var objUser = Context.Set<smSocialMediaProfile>().Where(x => x.UserId == userId && x.Fid == id).FirstOrDefault();
                objUser.IsDeleted = true;
                objUser.IsAccountActive = false;
                if (ImpersonateUserID != 0)
                {
                    objUser.DeletedBy = ImpersonateUserID;
                }
                else
                {
                    objUser.DeletedBy = userId;
                }
                objUser.DeletedDate = DateTime.UtcNow;
                Context.Set<smSocialMediaProfile>().Remove(objUser);
                Context.SaveChanges();
                status = true;


                var deletePreference = Context.Set<smFacebookDefaultPreference>().Where(x => x.userID == userId).FirstOrDefault();
                Context.Set<smFacebookDefaultPreference>().Remove(deletePreference);
                Context.SaveChanges();
            }



            catch
            {
                status = false;
            }


            try
            {
                if (status == true)
                {
                    var UserPageDel = Context.Set<smFacebookPageDetail>().Where(x => x.UserId == userId).ToList();
                    foreach (var item in UserPageDel)
                    {
                        item.IsDeleted = true;
                        Context.Set<smFacebookPageDetail>().Remove(item);
                        Context.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return status;
        }

        /// <summary>
        /// Delete clinic staff
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public bool UpdateSocialAccount(int id, int userId, bool status, int ImpersonateUserID)
        {
            bool stat = false;
            try
            {
                var objUser = Context.Set<smSocialMediaProfile>().Where(x => x.UserId == userId && x.Fid == id).FirstOrDefault();
                if (ImpersonateUserID != 0)
                {
                    if (status == true)
                    {
                        objUser.AccountActiveBy = ImpersonateUserID;
                    }
                    else
                    {
                        objUser.AccountDeactiveBy = ImpersonateUserID;
                    }
                }
                else
                {
                    if (status == true)
                    {
                        objUser.AccountActiveBy = userId;
                    }
                    else
                    {
                        objUser.AccountDeactiveBy = userId;
                    }
                }
                objUser.IsActive = status;
                Context.SaveChanges();
                stat = true;
            }
            catch
            {
                stat = false;
            }
            return stat;
        }

        public List<smPost> GetScheduledPosts(int userId)
        {
            List<smPost> smpost = new List<smPost>();
            try
            {
                smpost = Context.Set<smPost>().Where(x => x.UserId == userId && x.IsDeleted == false && x.IsActive == true && x.PostDate > DateTime.UtcNow).ToList();
            }
            catch (Exception ex)
            {

            }
            return smpost;
        }

        public List<smPost> GetTotalPosts(int userId)
        {
            List<smPost> smpost = new List<smPost>();
            try
            {
                smpost = Context.Set<smPost>().Where(x => x.UserId == userId && x.IsDeleted == false && x.IsActive == true && x.IsPosted == true).ToList();
            }
            catch (Exception ex)
            {

            }
            return smpost;
        }

        public List<smPost> GetTotalPostsByUser(int userId)
        {

            //var data = (from a in Context.Set<smPost>().AsEnumerable()
            //            let user = Context.Set<Users>().Where(c => c.UserId == a.UserId).FirstOrDefault()
            //            let CreatedUser = Context.Set<Users>().Where(c => c.UserId == a.CreatedBy).FirstOrDefault()
            //            let localDate = a.PostDate.ToLocalTime()
            //            where a.UserId == userId && a.IsDeleted == false && a.IsActive == true && a.PostDate <= DateTime.UtcNow
            //            select new smPost
            //            {
            //                PostId = a.PostId,
            //                UserId = a.UserId,
            //                SocialMediaProfileId = a.SocialMediaProfileId,
            //                SocialMedia = a.SocialMedia,
            //                Name = a.Name,
            //                Caption = a.Caption,
            //                Description = a.Description,
            //                Url = a.Url,
            //                ImageUrl = a.ImageUrl,
            //                PostDate = localDate,
            //                IsActive = a.IsActive,
            //                IsDeleted = a.IsDeleted,
            //                ModifiedDate = a.ModifiedDate,
            //                FirstName = user.FirstName,
            //                LastName = user.LastName,
            //                CreatedByName = CreatedUser.Email,
            //                RoleType = CreatedUser.UserType,
            //                LikesCount = a.LikesCount,
            //                CommentsCount = a.CommentsCount,
            //                LikesNames = a.LikesNames,
            //                CommentsText = a.CommentsText
            //            }).OrderByDescending(x => x.PostDate).ToList();


            //return data;



            var data = (from a in Context.Set<smPost>().AsEnumerable()
                        where a.UserId == userId && a.IsDeleted == false && a.IsActive == true && a.PostDate <= DateTime.UtcNow
                        select new smPost
                        {
                            PostId = a.PostId,
                            UserId = a.UserId,
                            SocialMediaProfileId = a.SocialMediaProfileId,
                            SocialMedia = a.SocialMedia,
                            Name = a.Name,
                            Caption = a.Caption,
                            Description = a.Description,
                            Url = a.Url,
                            ImageUrl = a.ImageUrl,
                            PostDate = a.PostDate.ToLocalTime(),
                            IsActive = a.IsActive,
                            IsDeleted = a.IsDeleted,
                            ModifiedDate = a.ModifiedDate,
                            LikesCount = a.LikesCount,
                            CommentsCount = a.CommentsCount,
                            LikesNames = a.LikesNames,
                            CommentsText = a.CommentsText,
                            CreatedBy = a.CreatedBy
                        }).OrderByDescending(x => x.PostDate).ToList();

            data = (from a in data
                    join user in Context.Set<Users>() on a.UserId equals user.UserId
                    let CreatedUser = Context.Set<Users>().Where(c => c.UserId == a.CreatedBy).FirstOrDefault()
                    select new smPost
                    {
                        PostId = a.PostId,
                        UserId = a.UserId,
                        SocialMediaProfileId = a.SocialMediaProfileId,
                        SocialMedia = a.SocialMedia,
                        Name = a.Name,
                        Caption = a.Caption,
                        Description = a.Description,
                        Url = a.Url,
                        ImageUrl = a.ImageUrl,
                        PostDate = a.PostDate.ToLocalTime(),
                        IsActive = a.IsActive,
                        IsDeleted = a.IsDeleted,
                        ModifiedDate = a.ModifiedDate,
                        LikesCount = a.LikesCount,
                        CommentsCount = a.CommentsCount,
                        LikesNames = a.LikesNames,
                        CommentsText = a.CommentsText,
                        CreatedBy = a.CreatedBy,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        CreatedByName = CreatedUser.Email,
                        RoleType = CreatedUser.UserTypeId,


                    }).ToList().OrderByDescending(x => x.PostDate).ToList();






            return data;





        }

        public List<smPost> GetTotalPostsByDate(int userId, DateTime? startDate, DateTime? endDate)
        {
            List<smPost> smpost = new List<smPost>();
            try
            {
                if (startDate != null)
                {
                    smpost = Context.Set<smPost>().Where(x => x.UserId == userId && x.IsDeleted == false && x.IsActive == true && x.PostDate >= startDate && x.PostDate <= endDate && x.IsPosted == true).ToList();
                }
                else
                {
                    smpost = Context.Set<smPost>().Where(x => x.UserId == userId && x.IsDeleted == false && x.IsActive == true && x.IsPosted == true).ToList();
                }

            }
            catch (Exception ex)
            {

            }
            return smpost;
        }


        public dynamic GetTotalClicks(int UserId)
        {
            AzureDBContext dbcontext = new AzureDBContext();
            List<smViewsModel> data = new List<smViewsModel>();
            try
            {
                DbDataReader _reader;
                dbcontext.Database.Initialize(force: false);
                dbcontext.Database.Connection.Open();
                var _dbCmd = dbcontext.Database.Connection.CreateCommand();
                _dbCmd.CommandText = "[smGetViewClick]";
                _dbCmd.CommandType = CommandType.StoredProcedure;
                _dbCmd.CommandTimeout = 60 * 5;
                _dbCmd.Parameters.AddRange(new SqlParameter[] {
                        new SqlParameter("@UserId",UserId),
                       
                    });
                _reader = _dbCmd.ExecuteReader();

                data = ((IObjectContextAdapter)dbcontext).ObjectContext.Translate<smViewsModel>(_reader).ToList();


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }
            return data.ToList();
        }

        public smScheduleEvents UpdateEventById(int EventId)
        {
            smScheduleEvents events = (from x in base.Context.Set<smScheduleEvents>()
                                       where (x.EventId == EventId) && !x.IsDeleted
                                       select x).FirstOrDefault<smScheduleEvents>();
            events.IsPosted = true;
            base.Context.SaveChanges();
            return events;
        }



        public List<smSocialMediaProfile> GetSocialMediaAccountsProfie(int userId)
        {
            List<smSocialMediaProfile> smsocialprofile = new List<smSocialMediaProfile>();
            try
            {
                smsocialprofile = Context.Set<smSocialMediaProfile>().Where(x => x.UserId == userId).ToList();
            }
            catch (Exception ex)
            {

            }
            return smsocialprofile;
        }

        public List<Users> GetUsersList(int limit, int offset, string order, string sort, string FirstName, string LastName, string EmailId, out int total)
        {
            var data = Context.Set<Users>().Where(x => x.UserTypeId == 2 && x.IsDeleted == false);
            if (!string.IsNullOrEmpty(FirstName))
            {
                data = data.Where(x => x.FirstName.Contains(FirstName));
            }
            if (!string.IsNullOrEmpty(LastName))
            {
                data = data.Where(x => x.LastName.Contains(LastName));
            }

            if (!string.IsNullOrEmpty(EmailId))
            {
                data = data.Where(x => x.Email.Contains(EmailId));
            }
            //Order by
            GetUserSortedData(ref data, sort + order.ToUpper());
            total = data.Count();
            return data.Skip(offset).Take(limit).ToList();
        }

        public void GetUserSortedData(ref IQueryable<Users> obj, string Case)
        {
            switch (Case)
            {
                case "FirstNameASC": obj = obj.OrderBy(x => x.FirstName.ToLower());
                    break;
                case "FirstNameDESC": obj = obj.OrderByDescending(x => x.FirstName.ToLower());
                    break;
                case "LastNameASC": obj = obj.OrderBy(x => x.LastName.ToLower());
                    break;
                case "LastNameDESC": obj = obj.OrderByDescending(x => x.LastName.ToLower());
                    break;
                case "EmailASC": obj = obj.OrderBy(x => x.Email.ToLower());
                    break;
                case "EmailDESC": obj = obj.OrderByDescending(x => x.Email.ToLower());
                    break;
                default: obj = obj.OrderByDescending(x => x.CreatedDate);
                    break;
            }
        }

        public List<smContentLibrary> GetCategoryList(int limit, int offset, string order, string sort, int UserType, int IndustryId, out int total)
        {
            List<smContentLibrary> library = Context.Set<smContentLibrary>().Where(x => x.CategoryId == IndustryId && x.IsActive == true && x.IsDeleted == false && x.SocialMedia != "Rss").ToList();
            //List<smContentLibrary> smContentLibrary = new List<smContentLibrary>();
            //foreach (var item in category)
            //{
            //    List<smContentLibrary> catg = Context.Set<smContentLibrary>().Where(x => x.CategoryName.Contains(item)).ToList();
            //    List<smContentLibrary> catg = library.Where(x => x.CategoryName.Contains(item)).ToList();
            //    foreach (smContentLibrary cat in catg)
            //    {
            //        smContentLibrary.Add(cat);
            //    }
            //}

            var data = library;
            //var data = smContentLibrary;           
            //Order by
            // GetSortedContent(data, sort + order.ToUpper());
            total = data.Count();
            return data.Skip(offset).Take(limit).ToList();
        }



        //public List<smContentLibrary> GetCategoryListforcalender(int IndustryId, List<smPreference> list)
        //{
        //    List<smContentLibrary> library = Context.Set<smContentLibrary>().Where(x => x.CategoryId == IndustryId && x.IsActive == true && x.IsDeleted == false && x.SocialMedia != "Rss").ToList();
        //    List<smContentLibrary> smContentLibrary = new List<smContentLibrary>();
        //    if (list.Count != 0)
        //    {
        //        foreach (var item in library)
        //        {
        //            if (!string.IsNullOrEmpty(item.SubIndustryName))
        //            {
        //                List<smPreference> catg = list.Where(x => x.Preference.ToLower() == item.SubIndustryName.ToLower()).ToList();
        //                smContentLibrary.Add(item);
        //            }
        //        }
        //    }
        //    return smContentLibrary;
        //}






        public List<smContentLibrary> GetCategoryListforcalender(int IndustryId, List<smPreference> list, int userId, string data)
        {


            var myvar = from a in Context.Set<smContentLibrary>().Where(x => x.CategoryId == IndustryId && x.IsActive == true && !x.IsDeleted)
                        join b in Context.Set<smSocialMediaProfile>().Where(x => x.UserId == userId) on a.SocialMedia equals b.SocialMedia
                        select new { a };


            // var rssContent = Context.Set<smContentLibrary>().Where(x => x.CategoryId == IndustryId && x.IsActive && !x.IsDeleted && x.SocialMedia != "Rss").ToList();

            List<smContentLibrary> list2 = new List<smContentLibrary>();
            foreach (var item in myvar)
            {
                list2.Add(item.a);
            }

            //  list2.AddRange(rssContent);



            //List<smContentLibrary> list2 = (from x in base.Context.Set<smContentLibrary>()
            //                              ///  where (((x.CategoryId == IndustryId) && x.IsActive) && !x.IsDeleted) && (x.SocialMedia != "Rss")    ///old 
            //                                where (((x.CategoryId == IndustryId) && x.IsActive) && !x.IsDeleted)    /// new SRohit
            //                                select x).ToList<smContentLibrary>();
            List<smContentstatus> list3 = (from x in base.Context.Set<smContentstatus>()
                                           where x.UserId == userId
                                           select x).ToList<smContentstatus>();
            List<smContentLibrary> source = new List<smContentLibrary>();
            if (list.Count != 0)
            {
                //using (List<smContentLibrary>.Enumerator enumerator = list2.GetEnumerator())
                //{
                //    while (enumerator.MoveNext())
                //    {
                //        Func<smPreference, bool> predicate = null;
                //        smContentLibrary item = enumerator.Current;
                //        if (!string.IsNullOrEmpty(item.SubIndustryName))
                //        {
                //            if (predicate == null)
                //            {
                //                predicate = x => x.Preference.ToLower() == item.SubIndustryName.ToLower();
                //            }
                //            list.Where<smPreference>(predicate).ToList<smPreference>();
                //            source.Add(item);

                //        }
                //    }
                //}
                var preferenceList = list.Select(x => x.Preference).ToList();
                source = list2.Where(m => preferenceList.Contains(m.SubIndustryName)).ToList();

            }

            List<smContentLibrary> second = new List<smContentLibrary>();
            List<smContentLibrary> list6 = new List<smContentLibrary>();
            if (list3.Count != 0)
            {
                using (List<smContentstatus>.Enumerator enumerator2 = list3.GetEnumerator())
                {
                    while (enumerator2.MoveNext())
                    {
                        Func<smContentLibrary, bool> func2 = null;
                        smContentstatus it = enumerator2.Current;
                        if (func2 == null)
                        {
                            func2 = x => x.ContentId == it.ContentId;
                        }
                        smContentLibrary library = source.Where<smContentLibrary>(func2).FirstOrDefault<smContentLibrary>();
                        second.Add(library);
                        list6 = source.Except<smContentLibrary>(second).ToList<smContentLibrary>();
                    }
                    //  return list6;


                    var AddedList = Context.Set<smPost>().Where(x => x.UserId == userId).Select(x => x.ContentId).ToList();
                    var OldList = list6.Where(e => AddedList.Contains(e.ContentId));
                    var NewList = list6.Where(e => !AddedList.Contains(e.ContentId));
                    if (!String.IsNullOrEmpty(data))
                    {
                        if (data == "0")
                        {
                            return OldList.ToList();
                        }
                        else
                        {
                            return NewList.ToList();
                        }
                    }


                }
            }
            var AddedLists = Context.Set<smPost>().Where(x => x.UserId == userId).Select(x => x.ContentId).ToList();
            var OldLists = source.Where(e => AddedLists.Contains(e.ContentId));
            var NewLists = source.Where(e => !AddedLists.Contains(e.ContentId));
            if (!String.IsNullOrEmpty(data))
            {
                if (data == "0")
                {
                    return OldLists.ToList();
                }
                else
                {
                    return NewLists.ToList();
                }
            }
            else
            {
                return source;
            }
        }






        public smScheduleEvents AddScheduleEvents(smScheduleEvents events)
        {
            smScheduleEvents evnt = new smScheduleEvents();
            evnt.ScheduleTime = events.ScheduleTime;
            evnt.LocalTime = events.LocalTime;
            evnt.Evnt_Id = events.Evnt_Id;
            evnt.ContentId = events.ContentId;
            evnt.Title = events.Title.Trim();
            evnt.IsFacebook = events.IsFacebook;
            evnt.IsLinkedIn = events.IsLinkedIn;
            evnt.IsTwitter = events.IsTwitter;
            evnt.UserId = events.UserId;
            evnt.CreatedDate = DateTime.UtcNow;
            Context.Set<smScheduleEvents>().Add(evnt);
            Context.SaveChanges();
            return evnt;
        }


        public List<smScheduleEvents> GetScheduleEvents(int UserId, DateTime startdate, DateTime endDate)
        {
            List<smScheduleEvents> events = Context.Set<smScheduleEvents>().Where(x => x.UserId == UserId && x.IsDeleted == false).ToList();
            return events;
        }



        public List<smContentLibrary> GetCategoryListByPrefrence(List<smPreference> list, int limit, int offset, string order, string sort, int UserType, int IndustryId, int UserId, out int total)
        {
            //List<smContentLibrary> library = Context.Set<smContentLibrary>().Where(x => x.CategoryId == IndustryId && x.IsActive == true && x.IsDeleted == false && x.SocialMedia != "Rss").ToList();
            //List<smContentLibrary> smContentLibrary = new List<smContentLibrary>();
            //if (list.Count != 0)
            //{
            //    foreach (var item in library)
            //    {
            //        if (!string.IsNullOrEmpty(item.SubIndustryName))
            //        {
            //            List<smPreference> catg = list.Where(x => x.Preference.ToLower() == item.SubIndustryName.ToLower()).ToList();
            //            smContentLibrary.Add(item);
            //        }
            //    }
            //}
            // var data = smContentLibrary.OrderByDescending(x => x.ContentId);


            var myvar = from a in Context.Set<smContentLibrary>().Where(x => x.CategoryId == IndustryId && x.IsActive == true && !x.IsDeleted)
                        join b in Context.Set<smSocialMediaProfile>().Where(x => x.UserId == UserId) on a.SocialMedia equals b.SocialMedia
                        select new { a };
            // var rssContent = Context.Set<smContentLibrary>().Where(x => x.CategoryId == IndustryId && x.IsActive && !x.IsDeleted && x.SocialMedia == "Rss").ToList();

            List<smContentLibrary> list2 = new List<smContentLibrary>();
            foreach (var item in myvar)
            {
                list2.Add(item.a);
            }

            //    list2.AddRange(rssContent);

            List<smContentLibrary> source = new List<smContentLibrary>();
            if (list.Count != 0)
            {

                var preferenceList = list.Select(x => x.Preference).ToList();
                source = list2.Where(m => preferenceList.Contains(m.SubIndustryName)).ToList();

            }


            var data = source.OrderByDescending(x => x.CreatedDate);
            //var data = smContentLibrary;           
            //Order by
            // GetSortedContent(data, sort + order.ToUpper());
            total = data.Count();
            return data.Skip(offset).Take(limit).ToList();
        }
        public List<smScheduleEvents> GetEventsBydate(int UserId, DateTime date)
        {
            return Context.Set<smScheduleEvents>().Where(m => (m.UserId == UserId) && !m.IsDeleted).ToList();
        }

        public smScheduleEvents GetEventById(int EventId)
        {
            return Context.Set<smScheduleEvents>().Where(m => m.EventId == EventId && !m.IsDeleted).FirstOrDefault();
        }

        public smScheduleEvents UpdateScheduleEvents(smScheduleEvents events)
        {
            smScheduleEvents events2 = (from x in Context.Set<smScheduleEvents>()
                                        where (x.EventId == events.EventId) && !x.IsDeleted
                                        select x).FirstOrDefault<smScheduleEvents>();
            events2.LocalTime = events.LocalTime;
            events2.ScheduleTime = events.ScheduleTime;
            Context.SaveChanges();
            return events2;
        }
        public smScheduleEvents GetEventByNewId(string EventId)
        {
            return (from x in Context.Set<smScheduleEvents>()
                    where (x.Evnt_Id == EventId) && !x.IsDeleted
                    select x).FirstOrDefault<smScheduleEvents>();
        }



        #endregion

        #region Manage POst


        public List<smPost> GetAllSocialMediaPost(int limit, int offset, string order, string sort, string Name, string Description, string Url, string ImageUrl, int currentUserId, out int total)
        {
            AzureDBContext db = new AzureDBContext();


            var data = (from a in db.smPost.AsEnumerable()
                        where a.UserId == currentUserId && a.IsDeleted == false && a.IsActive == true && a.PostDate <= DateTime.UtcNow
                        select a);

            if (!string.IsNullOrEmpty(Name))
            {
                data = data.Where(x => x.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Description))
            {
                data = data.Where(x => x.Description.Contains(Description));
            }
            if (!string.IsNullOrEmpty(ImageUrl))
            {
                data = data.Where(x => x.ImageUrl.Contains(ImageUrl));
            }
            if (!string.IsNullOrEmpty(Url))
            {
                data = data.Where(x => x.Url.Contains(Url));
            }

            GetSortedPost(ref data, sort + order.ToUpper());
            total = data.Count();

            data = (from a in data
                    join u in db.Users on a.UserId equals u.UserId
                    let c = Context.Set<Users>().Where(c => c.UserId == a.CreatedBy).FirstOrDefault()
                    select new smPost
                           {
                               PostId = a.PostId,
                               UserId = a.UserId,
                               SocialMediaProfileId = a.SocialMediaProfileId,
                               SocialMedia = a.SocialMedia,
                               Name = a.Name,
                               Caption = a.Caption,
                               Description = a.Description,
                               Url = a.Url,
                               ImageUrl = a.ImageUrl,
                               PostDate = a.PostDate,
                               IsActive = a.IsActive,
                               IsDeleted = a.IsDeleted,
                               ModifiedDate = a.ModifiedDate,
                               FirstName = u.FirstName,
                               LastName = u.LastName,
                               CreatedByName = c.Email,
                               RoleType = c.UserTypeId,
                               LikesCount = a.LikesCount,
                               CommentsCount = a.CommentsCount,
                               LikesNames = a.LikesNames,
                               CommentsText = a.CommentsText
                           });
            return data.Skip(offset).Take(limit).ToList();
        }


        public List<smPost> GetAllSocialFuturePost(int limit, int offset, string order, string sort, string Name, string Description, string Url, string ImageUrl, int currentUserId, out int total)
        {
            AzureDBContext db = new AzureDBContext();


            var data = (from a in db.smPost.AsEnumerable()
                        where a.UserId == currentUserId && a.IsDeleted == false && a.IsActive == true && a.PostDate > DateTime.UtcNow
                        select a);

            if (!string.IsNullOrEmpty(Name))
            {
                data = data.Where(x => x.Name.Contains(Name));
            }
            if (!string.IsNullOrEmpty(Description))
            {
                data = data.Where(x => x.Description.Contains(Description));
            }
            if (!string.IsNullOrEmpty(ImageUrl))
            {
                data = data.Where(x => x.ImageUrl.Contains(ImageUrl));
            }
            if (!string.IsNullOrEmpty(Url))
            {
                data = data.Where(x => x.Url.Contains(Url));
            }

            GetSortedPost(ref data, sort + order.ToUpper());
            total = data.Count();

            data = (from a in data
                    join u in db.Users on a.UserId equals u.UserId
                    let c = Context.Set<Users>().Where(c => c.UserId == a.CreatedBy).FirstOrDefault()
                    select new smPost
                    {
                        PostId = a.PostId,
                        UserId = a.UserId,
                        SocialMediaProfileId = a.SocialMediaProfileId,
                        SocialMedia = a.SocialMedia,
                        Name = a.Name,
                        Caption = a.Caption,
                        Description = a.Description,
                        Url = a.Url,
                        ImageUrl = a.ImageUrl,
                        PostDate = a.PostDate,
                        IsActive = a.IsActive,
                        IsDeleted = a.IsDeleted,
                        ModifiedDate = a.ModifiedDate,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        CreatedByName = c.Email,
                        RoleType = c.UserTypeId
                    });
            return data.Skip(offset).Take(limit).ToList();



        }
        public bool DeleteSocialPost(int id, int userId, int ImpersonateUserId)
        {
            bool status = false;
            try
            {
                var objUser = Context.Set<smPost>().Where(x => x.UserId == userId && x.PostId == id).FirstOrDefault();
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
                Context.Set<smPost>().Remove(objUser);
                Context.SaveChanges();
                status = true;
            }
            catch
            {
                status = false;
            }
            return status;
        }
        public bool UpdateContent(int PostId, string Description, string Name, List<string> imageIds, string url, int userId, int ImpersonateUserId)
        {
            bool status = false;
            try
            {


                string image = string.Empty;
                string imagePath = string.Empty;
                //string socialMedia = string.Empty;
                // Get Image name fom image array
                if (imageIds.Count != 0)
                {
                    foreach (var item in imageIds)
                    {
                        image = item;
                    }
                    if (image != string.Empty)
                    {
                        imagePath = "/Images/WallImages/" + userId + "/" + image;
                    }
                }

                var objContent = Context.Set<smPost>().Where(x => x.PostId == PostId).FirstOrDefault();
                if (objContent != null)
                {
                    if (String.IsNullOrEmpty(Name))
                    {
                        Name = objContent.Name;
                    }
                    objContent.Description = Description;
                    objContent.Name = Name;
                    if (image != string.Empty)
                    {
                        objContent.ImageUrl = imagePath;
                    }
                    objContent.IsActive = true;
                    objContent.IsDeleted = false;
                    objContent.Url = url;
                    if (ImpersonateUserId != 0)
                    {
                        objContent.ModifiedBy = ImpersonateUserId;
                    }
                    else
                    {
                        objContent.ModifiedBy = userId;
                    }
                    objContent.ModifiedDate = DateTime.UtcNow;
                    Context.SaveChanges();
                    status = true;
                }
            }
            catch
            {
                status = false;
            }
            return status;
        }

        public List<smPost> GetSocialMediaPost(int userId)
        {
            List<smPost> smsocialprofile = new List<smPost>();
            try
            {
                smsocialprofile = Context.Set<smPost>().Where(x => x.UserId == userId && x.PostDate <= DateTime.UtcNow).ToList();
                if (smsocialprofile.Count > 0)
                {
                    foreach (var data in smsocialprofile)
                    {
                        //data.PostDate = data.PostDate.ToLocalTime();
                        data.PostDate = data.PostDate.ToLocalTime();

                    }
                }

            }
            catch (Exception ex)
            {

            }
            return smsocialprofile;
        }
        public smPost EditContent(int id)
        {
            var objContent = Context.Set<smPost>().Where(x => x.PostId == id).FirstOrDefault();
            return objContent;
        }
        //public bool EditContent(smPost category)
        //{
        //    bool status = false;
        //    try
        //    {
        //        var objCategory = Context.Set<smPost>().Where(x => x.PostId == category.PostId).FirstOrDefault();
        //        objCategory.Name = category.Name;
        //        Context.SaveChanges();
        //        status = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        status = false;
        //    }
        //    return status;
        //}
        public List<smPost> GetFuturePost(int userId)
        {
            List<smPost> smsocialprofile = new List<smPost>();
            try
            {
                smsocialprofile = Context.Set<smPost>().OrderByDescending(x => x.PostDate).Where(x => x.UserId == userId && x.PostDate > DateTime.UtcNow).ToList();


            }
            catch (Exception)
            {

                throw;
            }
            return smsocialprofile;
        }

        public void GetSortedPost(ref IEnumerable<smPost> obj, string Case)
        {
            switch (Case)
            {
                case "NameASC":
                    obj = obj.OrderBy(x => x.Name.ToLower());
                    break;
                case "NameDESC":
                    obj = obj.OrderByDescending(x => x.Name.ToLower());
                    break;
                case "DescriptionASC":
                    obj = obj.OrderBy(x => x.Description.ToLower());
                    break;
                case "DescriptionDESC":
                    obj = obj.OrderByDescending(x => x.Description.ToLower());
                    break;
                case "UrlASC":
                    obj = obj.OrderBy(x => x.Url.ToLower());
                    break;
                case "UrlDESC":
                    obj = obj.OrderByDescending(x => x.Url.ToLower());
                    break;
                case "PostDateASC":
                    obj = obj.OrderBy(x => x.PostDate.ToString());
                    break;
                case "PostDateDESC":
                    obj = obj.OrderByDescending(x => x.PostDate.ToString());
                    break;
                default:
                    obj = obj.OrderByDescending(x => x.PostDate);
                    break;
            }
        }


        public void GetSortedContent(ref IQueryable<smContentLibrary> obj, string Case)
        {
            switch (Case)
            {
                case "DescriptionASC":
                    obj = obj.OrderBy(x => x.Description.ToLower());
                    break;
                case "DescriptionDESC":
                    obj = obj.OrderByDescending(x => x.Description.ToLower());
                    break;
                default:
                    obj = obj.OrderBy(x => x.UserId);
                    break;
            }
        }

        public List<smPreference> GetPreference(int UserId)
        {
            var result = Context.Set<smPreference>().Where(x => x.UserId == UserId && x.IsDeleted == false).ToList();
            return result;

        }



        #endregion

        #region
        public dynamic GetFacebookPageDetail(int userID)
        {
            List<smFacebookPageDetail> data = new List<smFacebookPageDetail>();


            data = Context.Set<smFacebookPageDetail>().Where(x => x.UserId == userID & x.IsDeleted == false & x.IsActive == true).ToList();
            List<string> Categories = data.Select(x => x.category).Distinct().ToList();
            List<CustomModelForPage> pageCUsr = new List<CustomModelForPage>();
            if (data.Count() > 0)
            {


                foreach (var item in Categories)
                {
                    CustomModelForPage newpage = new CustomModelForPage();
                    newpage.category = item;
                    newpage.dataList = data.Where(x => x.category == item).Select(x => new smFacebookPageDetail
                    {
                        PageAccessToken = x.PageAccessToken,
                        PageId = x.PageId,
                        PageName = x.PageName,
                        category = x.category,
                    }).ToList();
                    pageCUsr.Add(newpage);

                }
            }
            return pageCUsr;
        }
        #endregion

        public smFacebookDefaultPreference GetDefaultPrerefence(int userId)
        {
            return Context.Set<smFacebookDefaultPreference>().Where(x => x.userID == userId).FirstOrDefault();
        }
        #region Get Default preference to bind radio button
        #endregion

        #region  GetIndustryNameTermsAndCondition

        public List<smIndustry> GetIndustryNameTermsAndCondition()
        {

            return Context.Set<smIndustry>().Where(x => x.IsActive == true & x.IsDeleted == false).ToList();
        }
        #endregion

        #region Manage Leads


        public List<smHomeValue> GetAllLeads(int limit, int offset, string order, string sort, string Name, string Description, int currentUserId, out int total)
        {

            var checkAdmin = Context.Set<Users>().Where(x => x.UserId == currentUserId).Select(x => x.UserTypeId).FirstOrDefault();
            if (checkAdmin == 3 || checkAdmin == 2)
            {
                var data = Context.Set<smHomeValue>().AsEnumerable();
                if (!string.IsNullOrEmpty(Name))
                {
                    data = data.Where(x => x.FirstName.Contains(Name));
                }
                if (!string.IsNullOrEmpty(Description))
                {
                    data = data.Where(x => x.EmailAddress.Contains(Description));
                }
                GetSortedLeads(ref data, sort + order.ToUpper());
                total = data.Count();
                return data.Skip(offset).Take(limit).ToList();
            }
            else
            {
                var data = Context.Set<smHomeValue>().Where(x => x.userID == currentUserId).AsEnumerable();
                if (!string.IsNullOrEmpty(Name))
                {
                    data = data.Where(x => x.FirstName.Contains(Name));
                }
                if (!string.IsNullOrEmpty(Description))
                {
                    data = data.Where(x => x.EmailAddress.Contains(Description));
                }
                GetSortedLeads(ref data, sort + order.ToUpper());
                total = data.Count();
                return data.Skip(offset).Take(limit).ToList();
            }


            //Order by



        }

        public void GetSortedLeads(ref IEnumerable<smHomeValue> obj, string Case)
        {
            switch (Case)
            {
                case "EmailAddressASC":
                    obj = obj.OrderBy(x => x.EmailAddress.ToLower());
                    break;
                case "EmailAddressDESC":
                    obj = obj.OrderByDescending(x => x.EmailAddress.ToLower());
                    break;
                case "IsCompletedASC":
                    obj = obj.OrderBy(x => x.IsCompleted);
                    break;
                case "IsCompletedDESC":
                    obj = obj.OrderByDescending(x => x.IsCompleted);
                    break;
                case "DateSubmitASC":
                    obj = obj.OrderBy(x => x.DateSubmit.ToString());
                    break;
                case "DateSubmitDESC":
                    obj = obj.OrderByDescending(x => x.DateSubmit.ToString());
                    break;
                case "FirstNameASC":
                    obj = obj.OrderBy(x => x.FirstName.ToLower());
                    break;
                case "FirstNameDESC":
                    obj = obj.OrderByDescending(x => x.FirstName.ToLower());
                    break;
                default:
                    obj = obj.OrderByDescending(x => x.DateSubmit);
                    break;
            }
        }

        public dynamic EditLeads(smHomeValue data)
        {
              
            var datas = Context.Set<smHomeValue>().Where(x => x.HomeValueId == data.HomeValueId).FirstOrDefault();
            if (datas != null)
            {
                datas.FirstName = data.FirstName;
                datas.LastName = data.LastName;
                datas.PhoneNumber = data.PhoneNumber;
                datas.PostalCode = data.PostalCode;
                datas.StreetAddress = data.StreetAddress;
                datas.City = data.City;
                datas.EmailAddress = data.EmailAddress;
                datas.Address = data.Address;
                datas.Province = data.Province;
                datas.TimePeriodId = data.TimePeriodId;
                datas.Unit = data.Unit;
              
                Context.SaveChanges();
                return "success";
            }
            else
            {
                return "error"; ;
            }

        }
        #endregion
    }
}
