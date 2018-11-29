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
    public class ManageContentRepository : BaseRepository<smContentLibrary>, IManageContentRepository
    {
        #region ctor

        public ManageContentRepository(IAppUnitOfWork uow)
            : base(uow)
        {


        }
        #endregion

        #region public methods

        public List<smSubIndustry> GetAllSubIndustry(int IndustryId)
        {
            List<smSubIndustry> GetAllSub = new List<smSubIndustry>();
            try
            {
                GetAllSub = Context.Set<smSubIndustry>().Where(x => x.IndustryId == IndustryId && x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {

            }
            return GetAllSub;
        }
        /// <summary>
        /// Change User status active/deactive
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public bool PostContent(string message, List<string> imageIds, string url, int userId, int categoryId, string socialMedia, string categoryName, int SubIndustryId, string SubIndustryName, string contentsource, string imgs, string title, string heading, string GroupId)
        {
            message = message.Replace("—", "-");
            title = title.Replace("—", "-");
            message = message.Replace("’", "'");
            title = title.Replace("’", "'");
            bool status = false;
            try
            {
                string image = string.Empty;
                string imagePath = string.Empty;
                // string socialMedia = string.Empty;
                string tags = string.Empty;
                // Get Image name fom image array
                //foreach (var item in imageIds)
                //{
                //    image = item;
                //}
                if (imgs == "True")
                {
                    if (imageIds.Count != 0)
                    {
                        image = imageIds[0];
                        if (!String.IsNullOrEmpty(image))   //image != string.Empty
                        {
                            imagePath = image;
                            //imagePath = "/Images/WallImages/" + userId + "/" + image;
                        }
                        else
                        {
                            imagePath = null;
                        }
                    }

                }
                else
                {
                    if (imageIds.Count != 0)
                    {
                        image = imageIds[0];
                        if (!String.IsNullOrEmpty(image))
                        {
                            imagePath = image;
                            //imagePath = "/Images/WallImages/" + userId + "/" + image;
                        }
                        else
                        {
                            imagePath = null;
                        }
                    }
                }

                smContentLibrary content = new smContentLibrary();
                content.UserId = userId;
                content.SocialMedia = socialMedia;
                content.CategoryId = categoryId;
                content.Title = title;
                content.Heading = heading;
                content.ContentSource = contentsource;
                //content.Tags = tags;
                content.Description = message;
                content.Url = url;
                content.ImageUrl = imagePath;
                content.CreatedBy = userId;
                content.CreatedDate = DateTime.UtcNow;
                content.IsActive = true;
                content.IsDeleted = false;
                content.CategoryName = categoryName;
                content.SubIndustryId = SubIndustryId;
                content.SubIndustryName = SubIndustryName;
                
                if (GroupId==null)
                {
                    content.GroupId = RandomString(10);
                }
                else
                {
                    content.GroupId = GroupId;
                }

                var checkExist = Context.Set<smContentLibrary>().Where(x => x.GroupId.Trim() == GroupId.Trim() && x.SocialMedia == socialMedia).FirstOrDefault();
                if (checkExist == null)
                {
                    Context.Set<smContentLibrary>().Add(content);
                    Context.SaveChanges();
                }
                else
                {
                    checkExist.UserId = userId;
                    checkExist.SocialMedia = socialMedia;
                    checkExist.CategoryId = categoryId;
                    checkExist.Title = title;
                    checkExist.Heading = heading;
                    checkExist.ContentSource = contentsource;
                    //content.Tags = tags;
                    checkExist.Description = message;
                    checkExist.Url = url;
                    checkExist.ImageUrl = imagePath;
                    checkExist.CreatedBy = userId;
                    checkExist.CreatedDate = DateTime.UtcNow;
                    checkExist.IsActive = true;
                    checkExist.IsDeleted = false;
                    checkExist.CategoryName = categoryName;
                    checkExist.SubIndustryId = SubIndustryId;
                    checkExist.SubIndustryName = SubIndustryName;
                   /// checkExist.GroupId = GroupId;
                    Context.SaveChanges();
                }




              

                status = true;
            }
            catch
            {
                status = false;
            }
            return status;
        }

        /// <summary>
        /// Delete content
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public bool DeleteContent(string id, int userId)
        {
            bool status = false;
            try
            {

                Context.Set<smContentLibrary>().Where(x => x.GroupId.Trim() == id.Trim()).ToList().ForEach(u => u.IsDeleted = true); ;
                //objUser.IsDeleted = true;
                //objUser.DeletedBy = userId;
                //objUser.DeletedDate = DateTime.UtcNow;
                Context.SaveChanges(); 

                //var objUser = Context.Set<smContentLibrary>().Where(x => x.GroupId == id).FirstOrDefault();
                //objUser.IsDeleted = true;
                //objUser.DeletedBy = userId;
                //objUser.DeletedDate = DateTime.UtcNow;
                //Context.SaveChanges();
                status = true;
            }
            catch
            {
                status = false;
            }
            return status;
        }
        public string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// Delete content
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public smContentLibrary EditContent(int id)
        {
            var objContent = Context.Set<smContentLibrary>().Where(x => x.ContentId == id).FirstOrDefault();
            return objContent;
        }

        /// <summary>
        /// Change User status active/deactive
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public bool UpdateContentStatus(string id, int userId, bool status)
        {
            bool stat = false;
            try
            {
                //var objUser = Context.Set<Core.Domain.smContentLibrary>().Where(x => x.GroupId == id).FirstOrDefault();
                //objUser.IsActive = status;
                Context.Set<smContentLibrary>().Where(x => x.GroupId.Trim() == id.Trim()).ToList().ForEach(x => x.IsActive = status); 
                Context.SaveChanges();
                stat = true;
            }
            catch
            {
                stat = false;
            }
            return stat;
        }



        //public smContentLibrary UpdateContentOnSchedular(int contentId, int UserId, int ImpersonateId, int IndustryId)
        //{
        //    smContentLibrary library = (from x in base.Context.Set<smContentLibrary>()
        //                                where x.ContentId == contentId
        //                                select x).FirstOrDefault<smContentLibrary>();
        //    string title = library.Title;
        //    foreach (IGrouping<string, smContentLibrary> grouping in (from x in
        //                                                                  (from x in base.Context.Set<smContentLibrary>()
        //                                                                   where (((x.CategoryId == IndustryId) && !x.IsDeleted) && x.IsActive) && (x.SocialMedia != "Rss")
        //                                                                   select x).ToList<smContentLibrary>()
        //                                                              group x by x.Title).Distinct<IGrouping<string, smContentLibrary>>())
        //    {
        //        foreach (smContentLibrary library2 in grouping)
        //        {
        //            if (library2.Title == title)
        //            {
        //                smContentstatus contentstatus = new smContentstatus
        //                {
        //                    ContentId = library2.ContentId,
        //                    UserId = UserId,
        //                    IsDeleted = true
        //                };
        //                if (ImpersonateId != 0)
        //                {
        //                    contentstatus.DeletedBy = ImpersonateId;
        //                }
        //                else
        //                {
        //                    contentstatus.DeletedBy = UserId;
        //                }
        //                contentstatus.DeletedDate = DateTime.UtcNow;
        //                base.Context.Set<smContentstatus>().Add(contentstatus);
        //                base.Context.SaveChanges();
        //            }
        //        }
        //    }
        //    return library;
        //}




        public smContentLibrary UpdateContentOnSchedular(int contentId, int UserId, int ImpersonateId, int IndustryId)
        {
            smContentLibrary library = (from x in base.Context.Set<smContentLibrary>()
                                        where x.ContentId == contentId
                                        select x).FirstOrDefault<smContentLibrary>();
            string title = library.Title;
            foreach (IGrouping<string, smContentLibrary> grouping in (from x in
                                                                          (from x in base.Context.Set<smContentLibrary>()
                                                                           where (((x.CategoryId == IndustryId) && !x.IsDeleted) && x.IsActive) && (x.SocialMedia != "Rss")
                                                                           select x).ToList<smContentLibrary>()
                                                                      group x by x.Title).Distinct<IGrouping<string, smContentLibrary>>())
            {
                foreach (smContentLibrary library2 in grouping)
                {
                    if (library2.Title == title)
                    {
                        smContentstatus contentstatus = new smContentstatus
                        {
                            ContentId = library2.ContentId,
                            UserId = UserId,
                            IsDeleted = true
                        };
                        if (ImpersonateId != 0)
                        {
                            contentstatus.DeletedBy = ImpersonateId;
                        }
                        else
                        {
                            contentstatus.DeletedBy = UserId;
                        }
                        contentstatus.DeletedDate = DateTime.UtcNow;
                        base.Context.Set<smContentstatus>().Add(contentstatus);
                        base.Context.SaveChanges();
                    }
                }
            }
            return library;
        }







        /// <summary>
        /// Change User status active/deactive
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deletedByUserId"></param>
        /// <returns></returns>
        public bool UpdateContent(int contentId, string message, List<string> imageIds, string url, int userId, int categoryId, string socialMedia, string CategoryName, int SubIndustryId, string SubIndustryName, string imgs, string title, string heading)
        {
            bool status = false;
            try
            {
                string image = string.Empty;
                string imagePath = string.Empty;
                //string socialMedia = string.Empty;
                // Get Image name fom image array
                //if (imgs == "True")
                //{
                if (imageIds.Count != 0)
                {
                    foreach (var item in imageIds)
                    {
                        image = item;
                        if (!String.IsNullOrEmpty(image))
                        {
                            imagePath = image;
                            break;
                        }
                    }
                    //if (image != string.Empty)
                    //{
                    //    imagePath = "/Images/WallImages/ContentImages/"+ image;
                    //}
                }
                //}
                //else
                //{
                //    if (imageIds.Count != 0)
                //    {
                //        foreach (var item in imageIds)
                //        {
                //            image = item;
                //            if (!String.IsNullOrEmpty(image))
                //            {
                //                imagePath = image;
                //                break;
                //            }
                //        }
                //        //if (image != string.Empty)
                //        //{
                //        //    imagePath = "/Images/WallImages/ContentImages/"+ image;
                //        //}
                //    }
                //}
                var objContent = Context.Set<smContentLibrary>().Where(x => x.ContentId == contentId).FirstOrDefault();
                if (objContent != null)
                {
                    objContent.Description = message;
                    if (!String.IsNullOrEmpty(image))
                    {
                        objContent.ImageUrl = imagePath;
                    }
                    else
                    {
                        objContent.ImageUrl = null;
                    }
                    objContent.Title = title;
                    objContent.Heading = heading;
                    objContent.IsActive = true;
                    objContent.IsDeleted = false;
                    objContent.Url = url;
                    objContent.CategoryName = CategoryName;
                    objContent.SocialMedia = socialMedia;
                    objContent.CategoryId = categoryId;
                    objContent.ModifiedBy = userId;
                    objContent.SubIndustryId = SubIndustryId;
                    objContent.SubIndustryName = SubIndustryName;
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

        public List<smIndustry> GetCategories()
        {
            List<smIndustry> category = new List<smIndustry>();
            try
            {
                // category = Context.Set<smIndustry>().Where(x => x.IsDeleted == false && x.IsActive == true).ToList();
                category = Context.Set<Core.Domain.smIndustry>().Where(x => x.IsActive == true && x.IsDeleted == false).OrderBy(x => x.IndustryName).ToList();
            }
            catch
            {

            }
            return category;
        }

        public smIndustry GetIndustryById(int id)
        {
            smIndustry industry = Context.Set<Core.Domain.smIndustry>().Where(x => x.IsActive == true && x.IsDeleted == false && x.IndustryId == id).FirstOrDefault();
            return industry;
        }


        public List<smContentLibrary> CheckDuplicatecontent(string desc, string social, int IndustryId)
        {
            List<smContentLibrary> content = Context.Set<Core.Domain.smContentLibrary>().Where(x => x.IsActive == true && x.IsDeleted == false && x.SocialMedia.ToLower() == social.ToLower() && x.Description.ToLower() == desc.ToLower() && x.CategoryId == IndustryId).ToList();
            return content;
        }

        public smContentLibrary GetContentById(int Id)
        {
            smContentLibrary content = Context.Set<Core.Domain.smContentLibrary>().Where(x => x.ContentId == Id).FirstOrDefault();
            return content;
        }

        public smScheduleEvents GetContentId(int id)
        {
            return Context.Set<smScheduleEvents>().Where(m => m.EventId == id).FirstOrDefault();
        }



        public List<smContentLibrary> GetContentByTitle(string Title)
        {
            return (from x in Context.Set<smContentLibrary>()
                    where ((x.Title.ToLower() == Title.ToLower()) && !x.IsDeleted) && x.IsActive
                    select x).ToList<smContentLibrary>();
        }


        #region //// Terms and Condition Section
        public bool SaveTermsAndCondition(string termsHtml, int userId, string labelandtittle, string selectedIndustryID)
        {
            try
            {
                smTermsConditions TermsConditions = new smTermsConditions();
                TermsConditions.TermsConditions = termsHtml;
                TermsConditions.createdBy = userId;
                TermsConditions.cratedDate = DateTime.UtcNow;
                TermsConditions.isActive = false;
                TermsConditions.isDeleted = false;
                TermsConditions.id_Industry = Convert.ToInt32(selectedIndustryID);
                TermsConditions.labelandtittle = labelandtittle;
                Context.Set<smTermsConditions>().Add(TermsConditions);
                Context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }


        }
        public bool UpdateTermsandConditionDeactive(int id, int userId)
        {
            bool stat = false;
            try
            {
                var objUser = Context.Set<Core.Domain.smTermsConditions>().Where(x => x.id == id).FirstOrDefault();
                objUser.isActive = false;
                Context.SaveChanges();
                stat = true;
            }
            catch
            {
                stat = false;
            }
            return stat;
        }
        public bool UpdateTermsandConditionactive(int id, int userId, int id_Industry)
        {
            bool stat = false;
            try
            {
                var TotalTermsAndContionActive = Context.Set<smTermsConditions>().Where(x => x.isActive == true && x.isDeleted == false && x.id_Industry == id_Industry);
                if (TotalTermsAndContionActive.Count() > 1)
                {
                    var activeTerms = Context.Set<Core.Domain.smTermsConditions>().Where(x => x.isActive == true && x.id_Industry == id_Industry).FirstOrDefault();
                    if (activeTerms != null)
                    {
                        activeTerms.isActive = false;
                        Context.SaveChanges();
                    }
                }
                else
                {
                    var activeTerms = Context.Set<Core.Domain.smTermsConditions>().Where(x => x.isActive == true && x.id_Industry == id_Industry).FirstOrDefault();
                    if (activeTerms != null)
                    {
                        activeTerms.isActive = false;
                        Context.SaveChanges();
                    }

                }



                var activeTermsList = Context.Set<Core.Domain.smTermsConditions>().Where(x => x.id == id).FirstOrDefault();
                if (activeTermsList != null)
                {
                    activeTermsList.isActive = true;
                    Context.SaveChanges();
                }
                stat = true;
            }
            catch
            {
                stat = false;
            }
            return stat;
        }
        public bool DeleteTermsandCondition(int id, int userId)
        {
            bool status = false;
            try
            {
                var objUser = Context.Set<smTermsConditions>().Where(x => x.id == id).FirstOrDefault();
                objUser.isDeleted = true;
                Context.SaveChanges();
                status = true;
            }
            catch
            {
                status = false;
            }
            return status;
        }
        public bool UpdateTermsAndCondition(string termsHtml, string labelandtittle, string selectedIndustryID, int TermsAndConditionsId)
        {
            try
            {

                var objUser = Context.Set<Core.Domain.smTermsConditions>().Where(x => x.id == TermsAndConditionsId).FirstOrDefault();
                objUser.TermsConditions = termsHtml;
                objUser.id_Industry = Convert.ToInt32(selectedIndustryID);
                objUser.labelandtittle = labelandtittle;
                Context.SaveChanges();


                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }
        #endregion
       /// <summary>
       /// To archive or non archive the content
       /// </summary>
       /// <param name="id"></param>
       /// <param name="userId"></param>
   
       /// <returns></returns>
        public bool ArchiveContentEnable(string id, int userId, bool status)
        {
            bool stat = false;
            try
            {
                //var objUser = Context.Set<Core.Domain.smContentLibrary>().Where(x => x.GroupId == id).FirstOrDefault();
                //objUser.IsActive = status;
                Context.Set<smContentLibrary>().Where(x => x.GroupId.Trim() == id.Trim()).ToList().ForEach(x => x.Archive = status);
                Context.SaveChanges();
                stat = true;
            }
            catch
            {
                stat = false;
            }
            return stat;
        }

        #endregion

        #region Update Leads
        public dynamic  EditLeadsAdmin(smHomeValue data)
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
