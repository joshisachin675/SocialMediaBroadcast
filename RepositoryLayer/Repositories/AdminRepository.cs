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
    public class AdminRepository : BaseRepository<Users>, IAdminRepository
    {
        #region ctor

        public AdminRepository(IAppUnitOfWork uow)
            : base(uow)
        {


        }
        #endregion

        #region Public Methods
        public List<Users> GetUserList(int limit, int offset, string order, string sort, string FirstName, string LastName, string Email, string Photo, out int total)
        {
            AzureDBContext db = new AzureDBContext();
            // var data = from u in db.smSocialMediaProfile.AsQueryable()
            //where u.UserId == currentUserId
            // select new smSocialMediaProfile { Fid = u.Fid, FirstName = u.FirstName, LastName = u.LastName, Photo = u.Photo, Email = u.Email };
            // var data = GetAll().Where(x => x.UserId == currentUserId && x.IsActive == true).ToList();
            var data = Context.Set<Users>().Where(x => x.UserTypeId == 2 && x.IsDeleted == false);
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

        public List<Users> GetEndUserList(int limit, int offset, string order, string sort, string FirstName, string LastName, string Email, string Photo, int userType, int Role, int industryId, out int total)
        {
            AzureDBContext db = new AzureDBContext();
            var data = Context.Set<Users>().Where(x => x.IsDeleted == false);
            if (userType == 3) // SUperadmin
            {
                if (Role == 1)
                {
                    data = Context.Set<Users>().Where(x => x.UserTypeId != 3 && x.IsDeleted == false && x.UserTypeId == 1);
                }
                else if (Role == 2)
                {
                    data = Context.Set<Users>().Where(x => x.IsDeleted == false && x.UserTypeId == 2);
                }
                else if (Role == 3)
                {
                    data = Context.Set<Users>().Where(x => x.UserTypeId != 3 && x.IsDeleted == false);
                }

            }
            else if (userType == 2) // Admin
            {
                data = Context.Set<Users>().Where(x => x.UserTypeId == 1 && x.IsDeleted == false && x.IndustryId == industryId);
            }
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

        public List<smContentLibrary> GetContentLists(int limit, int offset, string order, string sort, string socialMedia, string description, string FirstName, string LastName, string CategoryName, DateTime CreatedDate, int UserType, int IndustryId, string PostedBy, DateTime? DateFrom, DateTime? DateTo, string IsArchive, out int total)
        {
            IEnumerable<smContentLibrary> data1;
            // IEnumerable<smContentLibrary> data2;
            if (UserType == 2)
            {
                data1 = (from a in Context.Set<smContentLibrary>().AsEnumerable()
                         let user = Context.Set<Users>().Where(c => c.UserId == a.CreatedBy).FirstOrDefault()
                         where a.IsDeleted == false
                         where a.CategoryId == IndustryId && a.Archive == Convert.ToBoolean(IsArchive)
                         select new smContentLibrary
                         {
                             ContentId = a.ContentId,
                             UserId = a.UserId,
                             SocialMedia = a.SocialMedia,
                             Description = a.Description,
                             ContentSource = a.ContentSource,
                             Url = a.Url,
                             Tags = a.Tags,
                             SubIndustryName = a.SubIndustryName,
                             SubIndustryId = a.SubIndustryId,
                             CategoryId = a.CategoryId,
                             CreatedDate = a.CreatedDate,
                             ImageUrl = a.ImageUrl,
                             CategoryName = a.CategoryName,
                             IsActive = a.IsActive,
                             IsDeleted = a.IsDeleted,
                             CreatedByName = user.FirstName + " " + user.LastName,
                             UserType = user.UserTypeId,
                             Title = a.Title,
                             Heading = a.Heading,
                             GroupId= a.GroupId,
                             Archive = a.Archive

                         });

                //data2 = (from a in Context.Set<smPost>().AsEnumerable()
                //         let user = Context.Set<Users>().Where(c => c.UserId == a.UserId).FirstOrDefault()
                //         where a.IsDeleted == false
                //         where user.IndustryId == IndustryId
                //         select new smContentLibrary
                //         {
                //             ContentId = a.PostId,
                //             UserId = a.UserId,
                //             SocialMedia = a.SocialMedia,
                //             Description = a.Description,
                //             Url = a.Url,
                //             Tags = string.Empty,
                //             SubIndustryName = string.Empty,
                //             SubIndustryId = 0,
                //             CategoryId = user.IndustryId,
                //             CreatedDate = a.PostDate,
                //             ImageUrl = a.ImageUrl,
                //             CategoryName = user.IndustryName,
                //             IsActive = a.IsActive,
                //             IsDeleted = a.IsDeleted,
                //             CreatedByName = user.FirstName + " " + user.LastName,
                //         });
            }
            else
            {
                data1 = (from a in Context.Set<smContentLibrary>().AsEnumerable()
                         let user = Context.Set<Users>().Where(c => c.UserId == a.CreatedBy).FirstOrDefault()
                         where a.IsDeleted == false && a.Archive == Convert.ToBoolean(IsArchive)
                         select new smContentLibrary
                         {
                             ContentId = a.ContentId,
                             UserId = a.UserId,
                             SocialMedia = a.SocialMedia,
                             Description = a.Description,
                             ContentSource = a.ContentSource,
                             Url = a.Url,
                             Tags = a.Tags,
                             SubIndustryName = a.SubIndustryName,
                             SubIndustryId = a.SubIndustryId,
                             CategoryId = a.CategoryId,
                             CreatedDate = a.CreatedDate,
                             ImageUrl = a.ImageUrl,
                             CategoryName = a.CategoryName,
                             IsActive = a.IsActive,
                             IsDeleted = a.IsDeleted,
                             CreatedByName = user.FirstName + " " + user.LastName,
                             UserType = user.UserTypeId,
                             Title = a.Title,
                             Heading = a.Heading,
                             GroupId = a.GroupId,
                             Archive=a.Archive
                         });

                //data2 = (from a in Context.Set<smPost>().AsEnumerable()
                //         let user = Context.Set<Users>().Where(c => c.UserId == a.UserId).FirstOrDefault()
                //         where a.IsDeleted == false
                //         select new smContentLibrary
                //         {
                //             ContentId = a.PostId,
                //             UserId = a.UserId,
                //             SocialMedia = a.SocialMedia,
                //             Description = a.Description,
                //             Url = a.Url,
                //             Tags = string.Empty,
                //             SubIndustryName = string.Empty,
                //             SubIndustryId = 0,
                //             CategoryId = user.IndustryId,
                //             CreatedDate = a.PostDate,
                //             ImageUrl = a.ImageUrl,
                //             CategoryName = user.IndustryName,
                //             IsActive = a.IsActive,
                //             IsDeleted = a.IsDeleted,
                //             CreatedByName = user.FirstName + " " + user.LastName,
                //             UserType = user.UserType,
                //         });
            }

            var data = data1;

            if (!string.IsNullOrEmpty(socialMedia))
            {
                data = data.Where(x => x.SocialMedia.ToLower().Contains(socialMedia.ToLower()));
            }
            if (!string.IsNullOrEmpty(CategoryName))
            {
                data = data.Where(x => x.CategoryName.ToLower().Contains(CategoryName.ToLower()));
            }
            if (!string.IsNullOrEmpty(description))
            {
                data = data.Where(x => x.Description.ToLower().Contains(description.ToLower()));
            }
            if (!string.IsNullOrEmpty(PostedBy))
            {
                data = data.Where(x => x.CreatedByName.ToLower().Contains(PostedBy.ToLower()));
            }
            if (!string.IsNullOrEmpty(Convert.ToString(DateFrom)) && string.IsNullOrEmpty(Convert.ToString(DateTo)))
            {
                // Get Utc Date from DateFrom
                DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(DateFrom), TimeZoneInfo.Local);
                data = data.Where(x => x.CreatedDate >= utcTime);
            }
            else if (!string.IsNullOrEmpty(Convert.ToString(DateTo)) && string.IsNullOrEmpty(Convert.ToString(DateFrom)))
            {
                // Get Utc Date from DateTo
                DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(DateTo), TimeZoneInfo.Local);
                data = data.Where(x => x.CreatedDate <= utcTime);
            }
            else if (!string.IsNullOrEmpty(Convert.ToString(DateTo)) && !string.IsNullOrEmpty(Convert.ToString(DateFrom)))
            {
                // Get Utc Date from DateTo
                DateTime utcTimeFrom = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(DateFrom), TimeZoneInfo.Local);
                DateTime utcTimeTo = TimeZoneInfo.ConvertTimeToUtc(Convert.ToDateTime(DateTo), TimeZoneInfo.Local);
                data = data.Where(x => x.CreatedDate >= utcTimeFrom && x.CreatedDate <= utcTimeTo);
            }


            //Order by
            GetSortedContentData(ref data, sort + order.ToUpper());
            total = data.GroupBy(x=>x.GroupId.Trim()).Count();
           var  d = data.GroupBy(x => x.GroupId.Trim()).SelectMany(r => r).ToList(); ;
           //return d.Skip(offset).Take(limit).ToList();
           return data.ToList();
        }

        public List<smPost> GetPostList(int limit, int offset, string order, string sort, string socialMedia, string firstName, string lastName, string Description, out int total, int userType, int industry)
        {
            //var data = Context.Set<smPost>().Where(x => x.IsDeleted == false);

            IEnumerable<smPost> data;
            if (userType == 3)
            {
                 data = (from a in Context.Set<smPost>().AsEnumerable()
                            where a.IsDeleted == false && a.IsActive == true
                            select a);


                data = (from a in data
                        join u in Context.Set<Users>() on a.UserId equals u.UserId
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

            }
            else
            {
                 data = (from a in Context.Set<smPost>().AsEnumerable()
                            where   a.IsDeleted == false && a.IsActive == true
                            select a);


                data = (from a in data
                        join u in Context.Set<Users>() on a.UserId equals u.UserId
                        let c = Context.Set<Users>().Where(c => c.UserId == a.CreatedBy).FirstOrDefault()
                        where a.IsDeleted == false && a.IsActive == true && u.IndustryId == industry
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
            }





            //IEnumerable<smPost> data;
            //if (userType == 3)
            //{
            //    data = (from a in Context.Set<smPost>().AsEnumerable()
            //            let user = Context.Set<Users>().Where(c => c.UserId == a.UserId).FirstOrDefault()
            //            let CreatedUser = Context.Set<Users>().Where(c => c.UserId == a.CreatedBy).FirstOrDefault()
            //            where a.IsDeleted == false && a.IsActive == true
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
            //                PostDate = a.PostDate,
            //                IsActive = a.IsActive,
            //                IsDeleted = a.IsDeleted,
            //                ModifiedDate = a.ModifiedDate,
            //                FirstName = user.FirstName,
            //                LastName = user.LastName,
            //                CreatedByName = CreatedUser.Email,
            //                RoleType = CreatedUser.UserType
            //            });

            //}
            //else
            //{
            //    data = (from a in Context.Set<smPost>().AsEnumerable()
            //            let user = Context.Set<Users>().Where(c => c.UserId == a.UserId).FirstOrDefault()
            //            let CreatedUser = Context.Set<Users>().Where(c => c.UserId == a.CreatedBy).FirstOrDefault()
            //            where a.IsDeleted == false && a.IsActive == true && user.IndustryId == industry
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
            //                PostDate = a.PostDate,
            //                IsActive = a.IsActive,
            //                IsDeleted = a.IsDeleted,
            //                ModifiedDate = a.ModifiedDate,
            //                FirstName = user.FirstName,
            //                LastName = user.LastName,
            //                CreatedByName = CreatedUser.Email,
            //                RoleType = CreatedUser.UserType
            //            });
            //}

            if (!string.IsNullOrEmpty(firstName))
            {
                data = data.Where(x => x.FirstName.ToLower().Contains(firstName.ToLower()));
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                data = data.Where(x => x.LastName.ToLower().Contains(lastName.ToLower()));
            }
            if (!string.IsNullOrEmpty(socialMedia))
            {
                data = data.Where(x => x.SocialMedia.ToLower().Contains(socialMedia.ToLower()));
            }
            //Order by
            GetSortedPostData(ref data, sort + order.ToUpper());
            total = data.Count();
            return data.Skip(offset).Take(limit).ToList();
        }

        public List<smIndustry> GetCategories(int limit, int offset, string order, string sort, string categoryName, out int total)
        {
            var data = Context.Set<smIndustry>().Where(x => x.IsDeleted == false);

            if (!string.IsNullOrEmpty(categoryName))
            {
                data = data.Where(x => x.IndustryName.Contains(categoryName));
            }

            //Order by
            GetSortedCategories(ref data, sort + order.ToUpper());
            total = data.Count();
            return data.Skip(offset).Take(limit).ToList();
        }

        public List<smRssFeeds> GetFeeds(int limit, int offset, string order, string sort, int userType, string search, out int total)
        {
            //var data = Context.Set<smRssFeeds>().Where(x => x.IsDeleted == false && x.IsApproved == true);
            var data = Context.Set<smRssFeeds>().Where(x => x.IsDeleted == false);
            //if (userType == 3)
            //{
            //    data = Context.Set<smRssFeeds>().Where(x => x.IsDeleted == false);
            //}
            //else
            //{
            //    data = Context.Set<smRssFeeds>().Where(x => x.IsDeleted == false && x.IsApproved == true);
            //}
            //Order by
            if (!String.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.FeedUrl.Contains(search) || x.FeedName.Contains(search));

            }

            GetSortedRss(ref data, sort + order.ToUpper());
            total = data.Count();
            return data.Skip(offset).Take(limit).ToList();
        }


        public List<smSubIndustry> GetAllSubCategoryList(int limit, int offset, string order, string sort, string subIndustry, int IndustryId, int userType, out int total)
        {
            var data = Context.Set<smSubIndustry>().Where(x => x.IsDeleted == false);
            if (userType == 3)
            {
                data = Context.Set<smSubIndustry>().Where(x => x.IsDeleted == false);
            }
            else
            {
                data = Context.Set<smSubIndustry>().Where(x => x.IsDeleted == false && x.IndustryId == IndustryId);
            }

            if (!string.IsNullOrEmpty(subIndustry))
            {
                data = data.Where(x => x.SubIndustryName.Contains(subIndustry));
            }

            //Order by
            GetSortedSubIndustry(ref data, sort + order.ToUpper());
            total = data.Count();
            return data.Skip(offset).Take(limit).ToList();
        }

        public List<smActivityLog> GetActivityList(int limit, int offset, string order, string sort, string Name, string Email, int userType, int role, out int total)
        {
            IEnumerable<smActivityLog> data;
            AzureDBContext db = new AzureDBContext();

            var data1 = (from activity in db.smActivityLogs
                         let user = db.Users.Where(c => c.UserId == activity.UserId).FirstOrDefault()
                         let createdEmail = db.Users.Where(d => d.UserId == activity.CreatedBy).Select(c => c.Email).FirstOrDefault()
                         where activity.UserId != 0
                         select new
                        {
                            activity,
                            user,
                            createdEmail
                        });




            if (!string.IsNullOrEmpty(Name))
                data1 = data1.Where(x => x.activity.FirstName.ToLower().Contains(Name) || x.activity.LastName.ToLower().Contains(Name));


            if (!string.IsNullOrEmpty(Email))
                data1 = data1.Where(x => x.activity.UserName.ToLower().Contains(Email));


            if (!string.IsNullOrEmpty(Name))
                data1 = data1.Where(x => x.activity.Name.ToLower().Contains(Name));

            if (role == 1 || role == 2)
                data1 = data1.Where(x => x.user.UserTypeId == role);

            if (userType == 3)
                data1 = data1.Where(r => r.user.UserTypeId != 3);
            else
                data1 = data1.Where(r => r.user.UserTypeId == 1);

            var result = data1.ToList().Select(d => new smActivityLog
            {
                ActivityId = d.activity.ActivityId,
                UserId = d.activity.UserId,
                IpAddress = d.activity.IpAddress,
                UserName = d.activity.UserName,
                AreaAccessed = d.activity.AreaAccessed,
                TimeStamp = d.activity.TimeStamp,
                FirstName = d.user.FirstName,
                LastName = d.user.LastName,
                Role = d.user.UserTypeId,
                Name = d.user.FirstName + " " + d.user.LastName,
                Email = d.user.Email,
                Event = d.activity.Event,
                Message = d.activity.Message,
                CreatedBy = d.activity.CreatedBy,
                CreatedByEmail = d.createdEmail

            }).AsEnumerable();

            GetSortedActivityList(ref result, sort + order.ToUpper());
            total = result.Count();
            return result.Skip(offset).Take(limit).ToList();

            throw new NotImplementedException();
            //data1 = (from a in data1
            //         let createdUser = db.Users.Where(d => d.UserId == a.activity.CreatedBy).FirstOrDefault()
            //         select new smActivityLog
            //             {
            //                 ActivityId = a.activity.ActivityId,
            //                 UserId = a.activity.UserId,
            //                 IpAddress = a.activity.IpAddress,
            //                 UserName = a.activity.UserName,
            //                 AreaAccessed = a.activity.AreaAccessed,
            //                 TimeStamp = a.activity.TimeStamp,
            //                 FirstName = a.user.FirstName,
            //                 LastName = a.user.LastName,
            //                 Role = a.user.UserType,
            //                 Name = a.user.FirstName + " " + a.user.LastName,
            //                 Email = a.user.Email,
            //                 Event = a.activity.Event,
            //                 Message = a.activity.Message,
            //                 CreatedBy = a.activity.CreatedBy,
            //                 CreatedByEmail = createdUser.Email
            //             });


            //GetSortedActivityList(ref data1, sort + order.ToUpper());
            //total = data1.Count();
            //return data1.Skip(offset).Take(limit).ToList();

            //// SuperAdmin
            //if (userType == 3)
            //{
            //    data = (from a in Context.Set<smActivityLog>().AsNoTracking().AsEnumerable()
            //            let user = Context.Set<Users>().AsNoTracking().Where(c => c.UserId == a.UserId).FirstOrDefault()
            //            let createdUser = Context.Set<Users>().AsNoTracking().Where(d => d.UserId == a.CreatedBy).FirstOrDefault()
            //            where a.UserId != 0 && user.UserType != 3
            //            select new smActivityLog
            //            {
            //                ActivityId = a.ActivityId,
            //                UserId = a.UserId,
            //                IpAddress = a.IpAddress,
            //                UserName = a.UserName,
            //                AreaAccessed = a.AreaAccessed,
            //                TimeStamp = a.TimeStamp,
            //                FirstName = user.FirstName,
            //                LastName = user.LastName,
            //                Role = user.UserType,
            //                Name = user.FirstName + " " + user.LastName,
            //                Email = user.Email,
            //                Event = a.Event,
            //                Message = a.Message,
            //                CreatedBy = a.CreatedBy,
            //                CreatedByEmail = createdUser.Email
            //            });
            //}
            //else // Admin
            //{
            //    data = (from a in Context.Set<smActivityLog>().AsNoTracking().AsEnumerable()
            //            let user = Context.Set<Users>().AsNoTracking().Where(c => c.UserId == a.UserId).FirstOrDefault()
            //            let createdUser = Context.Set<Users>().AsNoTracking().Where(d => d.UserId == a.CreatedBy).FirstOrDefault()
            //            where a.UserId != 0 && user.UserType == 1
            //            select new smActivityLog
            //            {
            //                ActivityId = a.ActivityId,
            //                UserId = a.UserId,
            //                IpAddress = a.IpAddress,
            //                UserName = a.UserName,
            //                AreaAccessed = a.AreaAccessed,
            //                TimeStamp = a.TimeStamp,
            //                FirstName = user.FirstName,
            //                LastName = user.LastName,
            //                Role = user.UserType,
            //                Name = user.FirstName + " " + user.LastName,
            //                Email = user.Email,
            //                Event = a.Event,
            //                Message = a.Message,
            //                CreatedBy = a.CreatedBy,
            //                CreatedByEmail = createdUser.Email
            //            });
            //}

            //if (!string.IsNullOrEmpty(Name))
            //{
            //    data = data.Where(x => x.FirstName.ToLower().Contains(Name) || x.LastName.ToLower().Contains(Name));
            //}

            //if (!string.IsNullOrEmpty(Email))
            //{
            //    data = data.Where(x => x.Email.ToLower().Contains(Email));
            //}

            //if (!string.IsNullOrEmpty(Name))
            //{
            //    data = data.Where(x => x.Name.ToLower().Contains(Name));
            //}

            //if (role == 1) // User
            //{
            //    data = data.Where(x => x.Role == 1);
            //}
            //else if (role == 2) // Admin
            //{
            //    data = data.Where(x => x.Role == 2);
            //}
            //else // All
            //{
            //    data = data.ToList();
            //}

            ////Order by
            //GetSortedActivityList(ref data, sort + order.ToUpper());
            //total = data.Count();
            //return data.Skip(offset).Take(limit).ToList();
        }



        public List<CustomTermsAndCondition> GetTermsandCondition(int limit, int offset, string order, string sort, out int total)
        {
            IQueryable<CustomTermsAndCondition> data;
            // SuperAdmin    




            data = (from x in Context.Set<smTermsConditions>().AsNoTracking().Where(x => x.isDeleted == false)
                    join o in Context.Set<smIndustry>().AsNoTracking() on x.id_Industry equals o.IndustryId into l
                    from s in l.DefaultIfEmpty()
                    select new CustomTermsAndCondition
                    {
                        isActive = x.isActive,
                        isDeleted = x.isDeleted,
                        TermsConditions = x.TermsConditions,
                        id = x.id,
                        labelandtittle = x.labelandtittle,
                        id_Industry = x.id_Industry,
                        cratedDate = x.cratedDate,
                        createdBy = x.createdBy,

                        IndustryName = (s == null || s.IndustryName == null ? "All Industry" : s.IndustryName),

                    });
            GetSortedTermsandConditon(ref data, sort + order.ToUpper());
            total = data.Count();
            return data.Skip(offset).Take(limit).ToList();
            //    return data.OrderByDescending(x=>x.cratedDate).ToList();


        }

        public List<CustomTermsAndCondition> GetTermsandConditionUser(int userID, int IndustryID)
        {
            List<CustomTermsAndCondition> data;
            // SuperAdmin    


            data = (from x in Context.Set<smTermsConditions>().AsNoTracking().Where(x => x.isDeleted == false && x.id_Industry == IndustryID && x.isActive == true)


                    select new CustomTermsAndCondition
                    {

                        TermsConditions = x.TermsConditions,

                        labelandtittle = x.labelandtittle,





                    }).ToList();

            if (data.Count() <= 0)
            {
                data = (from x in Context.Set<smTermsConditions>().AsNoTracking().Where(x => x.isDeleted == false && x.isActive == true && x.id_Industry == 0)


                        select new CustomTermsAndCondition
                        {

                            TermsConditions = x.TermsConditions,

                            labelandtittle = x.labelandtittle,





                        }).ToList();
            }




            return data.OrderByDescending(x => x.cratedDate).ToList();
            //data = (from a in Context.Set<smTermsConditions>().AsNoTracking().AsEnumerable()
            //        where a.isDeleted == false
            //        select new smTermsConditions
            //           {
            //               isActive = a.isActive,
            //               isDeleted = a.isDeleted,
            //               TermsConditions = a.TermsConditions,
            //               id = a.id,
            //               labelandtittle = a.labelandtittle,
            //               id_Industry =a.id_Industry,
            //               cratedDate = a.cratedDate,
            //               createdBy = a.createdBy


            //           });
            //total = data.Count();
            //return data.ToList();

        }

        public void GetSortedData(ref IQueryable<Users> obj, string Case)
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

        public void GetSortedContentData(ref IEnumerable<smContentLibrary> obj, string Case)
        {
            switch (Case)
            {
                case "SocialMediaASC": obj = obj.OrderBy(x => x.SocialMedia.ToLower());
                    break;
                case "SocialMediaDESC": obj = obj.OrderByDescending(x => x.SocialMedia.ToLower());
                    break;
                case "DescriptionASC": obj = obj.OrderBy(x => x.SocialMedia.ToLower());
                    break;
                case "DescriptionDESC": obj = obj.OrderByDescending(x => x.SocialMedia.ToLower());
                    break;
                case "CategoryNameASC": obj = obj.OrderBy(x => x.CategoryName.ToLower());
                    break;
                case "CategoryNameDESC": obj = obj.OrderByDescending(x => x.CategoryName.ToLower());
                    break;

                case "CreatedDateASC": obj = obj.OrderBy(x => x.CreatedDate.ToString());

                    break;
                case "CreatedDateDESC": obj = obj.OrderByDescending(x => x.CreatedDate.ToString());
                    break;
                default: obj = obj.OrderByDescending(x => x.CreatedDate);
                    break;
            }
        }

        public void GetSortedPostData(ref IEnumerable<smPost> obj, string Case)
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
                case "SocialMediaASC": obj = obj.OrderBy(x => x.SocialMedia.ToLower());
                    break;
                case "SocialMediaDESC": obj = obj.OrderByDescending(x => x.SocialMedia.ToLower());
                    break;
                case "DescriptionASC": obj = obj.OrderBy(x => x.Description.ToLower());
                    break;
                case "DescriptionDESC": obj = obj.OrderByDescending(x => x.Description.ToLower());
                    break;
                default: obj = obj.OrderByDescending(x => x.PostDate);
                    break;
            }
        }

        public void GetSortedCategories(ref IQueryable<smIndustry> obj, string Case)
        {
            switch (Case)
            {
                case "IndustryNameASC": obj = obj.OrderBy(x => x.IndustryName.ToLower());
                    break;
                case "IndustryNameDESC": obj = obj.OrderByDescending(x => x.IndustryName.ToLower());
                    break;
                default: obj = obj.OrderBy(x => x.IndustryId);
                    break;
            }
        }

        public void GetSortedRss(ref IQueryable<smRssFeeds> obj, string Case)
        {
            switch (Case)
            {
                case "FeedNameASC": obj = obj.OrderBy(x => x.FeedName.ToLower());
                    break;
                case "FeedNameDesc": obj = obj.OrderByDescending(x => x.FeedName.ToLower());
                    break;
                case "DateProcessASC": obj = obj.OrderBy(x => x.DateProcess);
                    break;
                case "DateProcessDESC": obj = obj.OrderByDescending(x => x.DateProcess);
                    break;
                default: obj = obj.OrderByDescending(x => x.DateProcess);
                    break;
            }
        }


        public void GetSortedSubIndustry(ref IQueryable<smSubIndustry> obj, string Case)
        {
            switch (Case)
            {
                case "SubIndustryNameASC": obj = obj.OrderBy(x => x.SubIndustryName.ToLower());
                    break;
                case "SubIndustryNameDesc": obj = obj.OrderByDescending(x => x.SubIndustryName.ToLower());
                    break;
                default: obj = obj.OrderByDescending(x => x.CreatedDate);
                    break;
            }
        }

        public void GetSortedActivityList(ref IEnumerable<smActivityLog> obj, string Case)
        {
            switch (Case)
            {
                case "NameASC": obj = obj.OrderBy(x => x.FirstName.ToLower());
                    break;
                case "NameDESC": obj = obj.OrderByDescending(x => x.FirstName.ToLower());
                    break;
                case "RoleASC": obj = obj.OrderBy(x => x.Role);
                    break;
                case "RoleDESC": obj = obj.OrderByDescending(x => x.Role);
                    break;
                case "EmailASC": obj = obj.OrderBy(x => x.Email.ToLower());
                    break;
                case "EmailDESC": obj = obj.OrderByDescending(x => x.Email.ToLower());
                    break;
                case "TimeStampASC": obj = obj.OrderBy(x => x.TimeStamp);
                    break;
                case "TimeStampDESC": obj = obj.OrderByDescending(x => x.TimeStamp);
                    break;
                default: obj = obj.OrderByDescending(x => x.ActivityId);
                    break;
            }
        }
        public void GetSortedTermsandConditon(ref IQueryable<CustomTermsAndCondition> obj, string Case)
        {
            switch (Case)
            {
                case "labelandtittleASC": obj = obj.OrderBy(x => x.labelandtittle.ToLower());
                    break;
                case "labelandtittleDesc": obj = obj.OrderByDescending(x => x.labelandtittle.ToLower());
                    break;
                case "IndustryNameASC": obj = obj.OrderBy(x => x.IndustryName.ToLower());
                    break;
                case "IndustryNameDesc": obj = obj.OrderByDescending(x => x.IndustryName.ToLower());
                    break;
                default: obj = obj.OrderByDescending(x => x.cratedDate);
                    break;
            }
        }

        #endregion
    }
}
