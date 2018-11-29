using Newtonsoft.Json;
using ServiceLayer.Services;
using smartData.Common;
using smartData.Filter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using Core.Domain;
using System.Net;
using System.Drawing.Imaging;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace smartData.Areas.Admin.Controllers
{
    class PostClass
    {
        public int ContentId { get; set; }
        public string TextMessage { get; set; }
        public List<string> ImageArray { get; set; }
        public string Url { get; set; }
        public string Heading { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string SocialMedia { get; set; }
        public int SubIndustryId { get; set; }
        public string SubIndustryName { get; set; }
        public string ContentSource { get; set; }
        public bool globalImg { get; set; }
        public string GroupId { get; set; }
        //  public string CategoryName { get; set; }  
    }

    class SocialAccountListFiltersPost : smartData.Common.GridFilter
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public int currentUserId { get; set; }

    }

    [AuthorizedRoles(Roles = "SuperAdmin, Admin")]
    public class ManageContentController : Controller
    {
        #region Global Variables
        ServiceLayer.Interfaces.IManageContentService _manageContentService;
        IManageContentAPIController _manageContentAPIController;
        //public List<ScreenPermissionList> obj;
        //public List<ScreenPermissionList> objScreenPermissionList = null;
        JavaScriptSerializer serializer = new JavaScriptSerializer();


        #endregion

        #region constructor
        public ManageContentController(ManageContentService manageContentService, IManageContentAPIController manageContentAPIController)
        {
            _manageContentService = manageContentService;
            _manageContentAPIController = manageContentAPIController;
        }
        #endregion


        // GET: /Admin/ManageContent/
        // [AuditLog(Event = "Manage Content", Message = "Manage Content")] 
        public ActionResult Index()
        {
            smContentLibrary content = new smContentLibrary();
            if (smartData.Common.SessionManager.LoggedInUser.UserType == 3)//superadmin
            {
                content.categoryList = new SelectList(_manageContentAPIController.GetCategories(), "IndustryId", "IndustryName");
            }
            else
            {
                smIndustry newlist = _manageContentService.GetIndustryById(smartData.Common.SessionManager.LoggedInUser.IndustryId);
                ViewBag.IndustryId = newlist.IndustryId.ToString();
                ViewBag.IndustryName = newlist.IndustryName.ToString();
            }
            TempData["img"] = false;
            return View(content);
        }

        [HttpGet]
        public ActionResult GetAllSubIndustry(int IndustryId)
        {

            //int id = 0; 

            var subIndustry = _manageContentAPIController.GetAllSubIndustry(IndustryId);
            var result = (from s in subIndustry
                          select new
                          {
                              id = s.IndustryId,
                              name = s.SubIndustryName
                          }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region upload Image
        [AuditLog(Event = "Upload Image", Message = "Upload image while adding content")]
        public ActionResult SaveUploadedFile()
        {
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool isSavedSuccessfully = true;
            string fName = "";
            string path = string.Empty;
            try
            {
                foreach (string fileName in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[fileName];
                    //Save file content goes here
                    fName = file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        // var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\WallImages", Server.MapPath(@"\")));
                        var originalDirectory = Server.MapPath("~/Images/WallImages/ContentImages/");

                        //string pathString = System.IO.Path.Combine(originalDirectory.ToString(), Convert.ToString(userId));
                        string pathString = originalDirectory;

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);
                        TempData["img"] = true;
                    }

                }

            }
            catch (Exception ex)
            {
                isSavedSuccessfully = false;
            }


            if (isSavedSuccessfully)
            {
                return Json(new { Message = path });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
        #endregion

        #region Post Content into library
        [HttpPost]
        [ValidateInput(false)]
        [AuditLog(Event = "Add content", Message = "Ad content to content library")]
        public ActionResult PostContent(string PostInformation)
        {
            PostClass postedContent = new PostClass();
            postedContent = JsonConvert.DeserializeObject<PostClass>(PostInformation);
            bool status = false;
            try
            {

                string imgs = "";
                if (TempData["img"] == null)
                {
                    TempData["img"] = false;
                    imgs = TempData["img"].ToString();
                }
                else
                {
                    imgs = TempData["img"].ToString();
                }
                //string imgs = globalImg.ToString();
                int userId = SessionManager.LoggedInUser.UserID;
                // var odj = _manageContentService.GetContentByTitle(postedContent.Title);              
                status = _manageContentAPIController.PostContent(postedContent.TextMessage, postedContent.ImageArray, postedContent.Url, userId, postedContent.CategoryId, postedContent.SocialMedia, postedContent.CategoryName, postedContent.SubIndustryId, postedContent.SubIndustryName, postedContent.ContentSource, imgs, postedContent.Title, postedContent.Heading ,postedContent.GroupId);
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        /// <summary>
        /// Delete the User admin by superadmin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Delete content", Message = "Delete content")]
        public JsonResult DeleteContent(string id)
        {
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool status = _manageContentAPIController.DeleteContent(id, userId);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete the User admin by superadmin
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Edit content", Message = "Edit content")]
        public JsonResult EditContent(int id)
        {
            smContentLibrary content = _manageContentAPIController.EditContent(id);
            return Json(new { result = content });
        }

        /// <summary>
        /// Delete the User Social Media account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Update content status", Message = "Update content status to active")]
        public JsonResult UpdateContentStatusActive(string id)
        {
            bool stat = true;
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool status = _manageContentAPIController.UpdateContentStatus(id, userId, stat);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete the User Social Media account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Update content status", Message = "Update content status to deactive")]
        public JsonResult UpdateContentStatusDeactive(string id)
        {
            bool stat = false;
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool status = _manageContentAPIController.UpdateContentStatus(id, userId, stat);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Update the content
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        [AuditLog(Event = "Update content", Message = "Update content to content library")]
        public ActionResult UpdateContent(string PostInformation)
        {
            PostClass postedContent = new PostClass();
            postedContent = JsonConvert.DeserializeObject<PostClass>(PostInformation);
            bool status = false;
            try
            {
                string imgs = "";
                if (TempData["img"] == null)
                {
                    TempData["img"] = false;
                    imgs = TempData["img"].ToString();
                }
                else
                {
                    imgs = TempData["img"].ToString();
                }
                int userId = SessionManager.LoggedInUser.UserID;
                status = _manageContentAPIController.UpdateContent(postedContent.ContentId, postedContent.TextMessage, postedContent.ImageArray, postedContent.Url, userId, postedContent.CategoryId, postedContent.SocialMedia, postedContent.CategoryName, postedContent.SubIndustryId, postedContent.SubIndustryName, imgs, postedContent.Title, postedContent.Heading);
            }
            catch (Exception ex)
            {
                status = false;
            }
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetIndustryById(int id)
        {
            smIndustry smIndustry = _manageContentAPIController.GetIndustryById(id);
            return Json(smIndustry, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [ValidateInput(false)]
        public string CheckDuplicateContent(string description, string socialMedia, int IndustryId)
        {
            List<smContentLibrary> content = _manageContentService.CheckDuplicatecontent(description, socialMedia, IndustryId).ToList();
            if (content.Count > 0)
            {
                return "exists";
            }
            else
            {
                return "";
            }

        }




        #region TermsAndConditions section
        /// <summary>
        /// Add Terms and Conditions
        /// </summary>
        /// <returns></returns>
        public ActionResult TermsAndConditions()
        {


            return View();
        }


        #region   terms and condition  section

        [HttpPost]
        [ValidateInput(false)]

        public ActionResult SaveTermsAndCondition(string termsHtml, string labelandtittle, string selectedIndustryID)
        {


            bool status = false;
            try
            {


                //string imgs = globalImg.ToString();
                int userId = SessionManager.LoggedInUser.UserID;
                // var odj = _manageContentService.GetContentByTitle(postedContent.Title);              
                status = _manageContentAPIController.SaveTermsAndCondition(termsHtml, userId, labelandtittle, selectedIndustryID);
            }
            catch (Exception ex)
            {
                //  status = false;
            }
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = " UpdateTermsandConditionDeactive", Message = "Update Terms and Condition Deactive")]
        public JsonResult UpdateTermsandConditionDeactive(int id)
        {
            bool stat = true;
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool status = _manageContentAPIController.UpdateTermsandConditionDeactive(id, userId);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = " UpdateTermsandConditionactive", Message = "Update Terms and Condition Active")]
        public JsonResult UpdateTermsandConditionactive(int id, int id_Industry)
        {
            bool stat = true;
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool status = _manageContentAPIController.UpdateTermsandConditionactive(id, userId, id_Industry);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AntiforgeryValidate]
        [AuditLog(Event = "Delete Terms And Condition", Message = "Delete term and Conditions")]
        public JsonResult DeleteTermsandCondition(int id)
        {

            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool status = _manageContentAPIController.DeleteTermsandCondition(id, userId);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }



        #region  Update  Terms and Conditions
        /// <summary>
        /// Update  Terms and Conditions 
        /// </summary>
        /// <param name="termsHtml"></param>
        /// <param name="labelandtittle"></param>
        /// <param name="selectedIndustryID"></param>
        /// <param name="TermsAndConditionsId"></param>
        /// <returns></returns>
        ///
        [HttpPost]
        [AntiforgeryValidate]
        [ValidateInput(false)]
        public JsonResult UpdateTermsAndCondition(string termsHtml, string labelandtittle, string selectedIndustryID, int TermsAndConditionsId)
        {
            bool status = _manageContentAPIController.UpdateTermsAndCondition(termsHtml, labelandtittle, selectedIndustryID, TermsAndConditionsId);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #endregion
        [HttpPost]
        [ValidateInput(false)]

        public dynamic FatchData (string data)
        {
            List<Fetchdata> result = new List<Fetchdata>();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(data);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string htmldata = readStream.ReadToEnd();
                var innerText = htmldata.Split('>')[1].Split('<')[0];
                List<string> links = new List<string>();
                List<string> FinalData = new List<string>();
                string li;
                //string regexImgSrc = @"<meta property.*?>";               
                string regexImgSrc = @"<meta .*?>";              
                string regexFindTitle = @"<title>\s*(.+?)\s*</title>";
                string regexFindDescriptiom = @"<meta name=.*?>";



                //// Find All meta 
                MatchCollection matches = Regex.Matches(htmldata, regexImgSrc, RegexOptions.IgnoreCase | RegexOptions.Singleline);            
                foreach (Match k in matches)
                {

                    links.Add(k.Value);
                   

                }



                foreach (var item in links)
                {
                    FinalData.Add(item.Replace("data-react-helmet=\"true\"", ""));

                }
                //// Find All meta 

                //// Find All title
                MatchCollection matchestitle = Regex.Matches(htmldata, regexFindTitle, RegexOptions.IgnoreCase | RegexOptions.Singleline);

                foreach (Match k in matchestitle)
                {
                    FinalData.Add(k.Value);
                }

                //// Find All title


                //// Find All description
                MatchCollection matchedes = Regex.Matches(htmldata, regexFindDescriptiom, RegexOptions.IgnoreCase | RegexOptions.Singleline);

                foreach (Match k in matchedes)
                {
                    FinalData.Add(k.Value);
                }

                //// Find All description
 


                response.Close();
                readStream.Close();
                //eturn links;
                return new JsonResult { Data = FinalData };
               

            
               



              
                            
            }
            else
            {
                return false;
            }
            
        }

        #region manage leads
      
        public ActionResult ManageLeadsAdmin()
        {
            return View(); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageLeadsAdmin(Core.Domain.smHomeValue data)
        {
            try
            {
                int UserId = SessionManager.LoggedInUser.UserID;
                var dara = _manageContentService.EditLeadsAdmin(data);
                return Json(new { Response = "success" });
            }
            catch (Exception ex)
            {
                return Json(new { Response = "Error" });
            }
        }

        #endregion 

        public JsonResult ArchiveContentEnable(string id)
        {

            //bool stat = false;
            MembershipUser mu = Membership.GetUser();
            int userId = 0;
            //userId = Convert.ToInt32(mu.ProviderUserKey);
            userId = smartData.Common.SessionManager.LoggedInUser.UserID;
            bool status = _manageContentAPIController.ArchiveContentEnable(id, userId , true);
            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }
     
    }
}