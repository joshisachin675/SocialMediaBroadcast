using Core.Domain;
using Newtonsoft.Json;
using ServiceLayer.Interfaces;
using smartData.Common;
using smartData.Filter;
using smartData.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace smartData.Areas.Users.Controllers
{
    public class ReportsController : Controller
    {
        IScheduleService _scheduleService;

        #region constructor
        public ReportsController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
            //System.Net.Http.Headers.contContentType = new MediaTypeHeaderValue("application/json");
        }
        #endregion
        //
        // GET: /Users/Reports/
        [AuditLog(Event = "View Reports", Message = "View Reports")] 
        public ActionResult Index()
        {
            return View();
        }

        [AuditLog(Event = "View Report in chart form", Message = "View report in different perspective")] 
        public string GetPostDetailsChart(int UserId, string StartDate, string EndDate, string ChartType)
        {
            List<ReportsGraph> ListReportsGraph = null;
            ReportsGraph ReportsGraph = null;
            var chartTestlist = new List<ChartTest>();
            var utcNow = DateTime.UtcNow.Date;
            List<smSocialMediaProfile> listSocialMedia = _scheduleService.GetAllSocialMediaAccountByUserId(UserId);
            List<smPost> Post = null;

            var currentDate = DateTime.Now.Date;
            var SDate = !string.IsNullOrEmpty(StartDate) ? Convert.ToDateTime(StartDate) : currentDate.AddMonths(-1);
            var EDate = (!string.IsNullOrEmpty(EndDate) ? Convert.ToDateTime(EndDate) : currentDate).Date;
            //
            // End of day
            EDate = EDate.AddDays(1).AddTicks(-1);

            string[] MonthArray = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            //var ChartType = "Day"; //Request.QueryString["ChartType"];
            var CalendarType = Request.QueryString["CalendarType"];

            List<smPost> listPost = _scheduleService.GetAllPostListByUserId(UserId, SDate, EDate);

            ListReportsGraph = new List<ReportsGraph>();

            if (listPost.Count > 0)
            {
                if (ChartType == "Day")
                {
                    foreach (var item in listSocialMedia)
                    {
                        var test = new ChartTest { SourceName = item.SocialMedia, DataList = new List<Data>() };
                        for (var i = 31; i >= 1; i--)
                        {
                            var filtered = listPost.Where(m => m.SocialMediaProfileId == item.Fid).ToList();
                            var cDate = utcNow.AddDays(-i + 1).Date;
                            Post = filtered.Where(m => m.PostDate.Date >= cDate && m.PostDate.Date < cDate.AddDays(1)).ToList();
                            var data = new Data
                            {
                                Date = utcNow.AddDays(-i + 1).Day + " " + MonthArray[utcNow.AddDays(-i + 1).Month - 1],
                                Count = Post.Count > 0 ? Post.Count : 0
                            };
                            test.DataList.Add(data);
                        }
                        chartTestlist.Add(test);
                    }
                }
                else if (ChartType == "Week")
                {
                    foreach (var item in listSocialMedia)
                    {

                        var test = new ChartTest { SourceName = item.SocialMedia, DataList = new List<Data>() };
                        for (var i = 35; i >= 0; i -= 7)
                        {
                            var filtered = listPost.Where(m => m.SocialMediaProfileId == item.Fid).ToList();
                            Post =
                                filtered.Where(
                                    m =>
                                        m.PostDate.Date <= utcNow.AddDays(-i).Date &&
                                        m.PostDate.Date >= utcNow.AddDays(-(i + 7)).Date).ToList();

                            var data = new Data
                            {
                                Date = utcNow.AddDays(-i).Date.Day + " " + MonthArray[utcNow.AddDays(-i).Month - 1],
                                Count = Post.Count,
                                ToolTip =
                                    utcNow.AddDays(-(i + 7)).DayOfWeek + ", " + utcNow.AddDays(-(i + 7)).Day + ", " +
                                    MonthArray[utcNow.AddDays(-(i + 7)).Month - 1] + "<br/>Keyword Tracker: " +
                                    Post.Count
                            };

                            test.DataList.Add(data);
                        }
                        chartTestlist.Add(test);
                    }
                }
                else if (ChartType == "Month")
                {
                    var date = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
                    foreach (var item in listSocialMedia)
                    {
                        var test = new ChartTest { SourceName = item.SocialMedia, DataList = new List<Data>() };
                        var data = new Data { Date = date.Day + " " + MonthArray[date.AddMonths(-1).Month - 1] };
                        var filtered = listPost.Where(m => m.SocialMediaProfileId == item.Fid).ToList();
                        Post =
                            filtered.Where(m => m.PostDate.Date <= date && m.PostDate.Date >= date.AddMonths(-1))
                                .ToList();
                        data.Count = Post.Count;
                        test.DataList.Add(data);

                        data = new Data();
                        data.Date = "1 " + MonthArray[DateTime.UtcNow.Month - 1];
                        var Filtered1 = listPost.Where(m => m.SocialMediaProfileId == item.Fid).ToList();
                        Post = Filtered1.Where(m => m.PostDate.Date >= date.Date).ToList();
                        data.Count = Post.Count;
                        test.DataList.Add(data);
                        chartTestlist.Add(test);
                    }
                }

                else if (ChartType == "Custom")
                {
                    var DateDiff = EDate >= SDate ? Convert.ToInt32((EDate - SDate).TotalDays) : 0;
                    foreach (var item in listSocialMedia)
                    {
                        var Test = new ChartTest();
                        Test.SourceName = item.SocialMedia;
                        Test.DataList = new List<Data>();
                        Data data = null;
                        for (var i = DateDiff + 1; i >= 1; i--)
                        {
                            data = new Data
                            {
                                Date = EDate.AddDays(-i + 1).Day + " " + MonthArray[EDate.AddDays(-i + 1).Month - 1]
                            };


                            var Filtered = listPost.Where(m => m.SocialMediaProfileId == item.Fid).ToList();
                            var cDate = EDate.AddDays(-i + 1).Date;
                            Post =
                                Filtered.Where(m => m.PostDate.Date >= cDate && m.PostDate.Date < cDate.AddDays(1)).ToList();
                            data.Count = Post.Count > 0 ? Post.Count : 0;
                            Test.DataList.Add(data);
                        }
                        chartTestlist.Add(Test);
                    }
                }
            }
            var Serializer = new JavaScriptSerializer();
            return Serializer.Serialize(chartTestlist);

        }


        public string GetLikesOnPosts(int UserId, string StartDate, string EndDate, string ChartType)
        {
            List<ReportsGraph> ListReportsGraph = null;
            ReportsGraph ReportsGraph = null;
            var chartTestlist = new List<ChartTest>();
            var utcNow = DateTime.UtcNow.Date;
            List<smSocialMediaProfile> listSocialMedia = _scheduleService.GetAllSocialMediaAccountByUserId(UserId);
            List<smPost> Post = null;

            var currentDate = DateTime.Now.Date;
            var SDate = !string.IsNullOrEmpty(StartDate) ? Convert.ToDateTime(StartDate) : currentDate.AddMonths(-1);
            var EDate = (!string.IsNullOrEmpty(EndDate) ? Convert.ToDateTime(EndDate) : currentDate).Date;
            //
            // End of day
            EDate = EDate.AddDays(1).AddTicks(-1);

            string[] MonthArray = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            //var ChartType = "Day"; //Request.QueryString["ChartType"];
            var CalendarType = Request.QueryString["CalendarType"];
            List<smPost> listPost = _scheduleService.GetAllPostListByUserId(UserId, SDate, EDate);
            ListReportsGraph = new List<ReportsGraph>();
            if (ChartType == "Likes")
            {
                foreach (var item in listPost)
                {
                    ReportsGraph = new ReportsGraph();
                    ReportsGraph.Count = item.LikesCount;
                    ReportsGraph.Date ="P"+item.PostId.ToString();
                    ReportsGraph.TrackerName = "Likes";
                    ListReportsGraph.Add(ReportsGraph);
                }
            }
            else
            {
                foreach (var item in listPost)
                {
                    ReportsGraph = new ReportsGraph();
                    ReportsGraph.Count = item.CommentsCount;
                    ReportsGraph.Date = "P"+item.PostId.ToString();
                    ReportsGraph.TrackerName = "Comments";
                    ListReportsGraph.Add(ReportsGraph);
                }
            }           
            var Serializer = new JavaScriptSerializer();
            return Serializer.Serialize(ListReportsGraph);
        }



        [AuditLog(Event = "Add watermark", Message = "Add watermark to image")] 
        public static void AddWaterMark(MemoryStream ms, string watermarkText, MemoryStream outputStream)
        {
            System.Drawing.Image img = System.Drawing.Image.FromStream(ms);
            Graphics gr = Graphics.FromImage(img);
            Font font = new Font("Tahoma", (float)40);
            Color color = Color.FromArgb(50, 241, 235, 105);
            double tangent = (double)img.Height / (double)img.Width;
            double angle = Math.Atan(tangent) * (180 / Math.PI);
            double halfHypotenuse = Math.Sqrt((img.Height * img.Height) + (img.Width * img.Width)) / 2;
            double sin, cos, opp1, adj1, opp2, adj2;

            for (int i = 100; i > 0; i--)
            {
                font = new Font("Tahoma", i, FontStyle.Bold);
                SizeF sizef = gr.MeasureString(watermarkText, font, int.MaxValue);

                sin = Math.Sin(angle * (Math.PI / 180));
                cos = Math.Cos(angle * (Math.PI / 180));
                opp1 = sin * sizef.Width;
                adj1 = cos * sizef.Height;
                opp2 = sin * sizef.Height;
                adj2 = cos * sizef.Width;

                if (opp1 + adj1 < img.Height && opp2 + adj2 < img.Width)
                    break;
                //
            }

            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            stringFormat.LineAlignment = StringAlignment.Center;

            gr.SmoothingMode = SmoothingMode.AntiAlias;
            gr.RotateTransform((float)angle);
            gr.DrawString(watermarkText, font, new SolidBrush(color), new Point((int)halfHypotenuse, 0), stringFormat);

            img.Save(outputStream, ImageFormat.Jpeg);
        }

       // [AuditLog]
        public string Watermark()
        {

            var imagePath = ConfigurationManager.AppSettings["SiteUrl"];
            imagePath = Server.MapPath("~/Images/");
            // string WorkingDirectory = imagePath;

            string WorkingDirectory = Server.MapPath("~/Images");

            //string pathString = System.IO.Path.Combine(originalDirectory.ToString(), Convert.ToString(userId));
            string pathString = WorkingDirectory;
            string img = "social_media.png";

            //define a string of text to use as the Copyright message
            string Copyright = "Copyright © 2002 - AP Photo/David Zalubowski";

            //create a image object containing the photograph to watermark
            Image imgPhoto = Image.FromFile(pathString + "\\" + img);
            int phWidth = imgPhoto.Width;
            int phHeight = imgPhoto.Height;

            //create a Bitmap the Size of the original photograph
            Bitmap bmPhoto = new Bitmap(phWidth, phHeight, PixelFormat.Format24bppRgb);

            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);

            //load the Bitmap into a Graphics object 
            Graphics grPhoto = Graphics.FromImage(bmPhoto);

            //create a image object containing the watermark
            Image imgWatermark = new Bitmap(imagePath + "\\logo2.png");
            int wmWidth = imgWatermark.Width;
            int wmHeight = imgWatermark.Height;

            //------------------------------------------------------------
            //Step #1 - Insert Copyright message
            //------------------------------------------------------------

            //Set the rendering quality for this Graphics object
            grPhoto.SmoothingMode = SmoothingMode.AntiAlias;

            //Draws the photo Image object at original size to the graphics object.
            grPhoto.DrawImage(
                imgPhoto,                               // Photo Image object
                new Rectangle(0, 0, phWidth, phHeight), // Rectangle structure
                0,                                      // x-coordinate of the portion of the source image to draw. 
                0,                                      // y-coordinate of the portion of the source image to draw. 
                phWidth,                                // Width of the portion of the source image to draw. 
                phHeight,                               // Height of the portion of the source image to draw. 
                GraphicsUnit.Pixel);                    // Units of measure 

            //-------------------------------------------------------
            //to maximize the size of the Copyright message we will 
            //test multiple Font sizes to determine the largest posible 
            //font we can use for the width of the Photograph
            //define an array of point sizes you would like to consider as possiblities
            //-------------------------------------------------------
            int[] sizes = new int[] { 16, 14, 12, 10, 8, 6, 4 };

            Font crFont = null;
            SizeF crSize = new SizeF();

            //Loop through the defined sizes checking the length of the Copyright string
            //If its length in pixles is less then the image width choose this Font size.
            for (int i = 0; i < 7; i++)
            {
                //set a Font object to Arial (i)pt, Bold
                crFont = new Font("arial", sizes[i], FontStyle.Bold);
                //Measure the Copyright string in this Font
                crSize = grPhoto.MeasureString(Copyright, crFont);

                if ((ushort)crSize.Width < (ushort)phWidth)
                    break;
            }

            //Since all photographs will have varying heights, determine a 
            //position 5% from the bottom of the image
            int yPixlesFromBottom = (int)(phHeight * .05);

            //Now that we have a point size use the Copyrights string height 
            //to determine a y-coordinate to draw the string of the photograph
            float yPosFromBottom = ((phHeight - yPixlesFromBottom) - (crSize.Height / 2));

            //Determine its x-coordinate by calculating the center of the width of the image
            float xCenterOfImg = (phWidth / 2);

            //Define the text layout by setting the text alignment to centered
            StringFormat StrFormat = new StringFormat();
            StrFormat.Alignment = StringAlignment.Center;

            //define a Brush which is semi trasparent black (Alpha set to 153)
            SolidBrush semiTransBrush2 = new SolidBrush(Color.FromArgb(153, 0, 0, 0));

            //Draw the Copyright string
            grPhoto.DrawString(Copyright,                 //string of text
                crFont,                                   //font
                semiTransBrush2,                           //Brush
                new PointF(xCenterOfImg + 1, yPosFromBottom + 1),  //Position
                StrFormat);

            //define a Brush which is semi trasparent white (Alpha set to 153)
            SolidBrush semiTransBrush = new SolidBrush(Color.FromArgb(153, 255, 255, 255));

            //Draw the Copyright string a second time to create a shadow effect
            //Make sure to move this text 1 pixel to the right and down 1 pixel
            grPhoto.DrawString(Copyright,                 //string of text
                crFont,                                   //font
                semiTransBrush,                           //Brush
                new PointF(xCenterOfImg, yPosFromBottom),  //Position
                StrFormat);                               //Text alignment



            //------------------------------------------------------------
            //Step #2 - Insert Watermark image
            //------------------------------------------------------------

            //Create a Bitmap based on the previously modified photograph Bitmap
            Bitmap bmWatermark = new Bitmap(bmPhoto);
            bmWatermark.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            //Load this Bitmap into a new Graphic Object
            Graphics grWatermark = Graphics.FromImage(bmWatermark);

            //To achieve a transulcent watermark we will apply (2) color 
            //manipulations by defineing a ImageAttributes object and 
            //seting (2) of its properties.
            ImageAttributes imageAttributes = new ImageAttributes();

            //The first step in manipulating the watermark image is to replace 
            //the background color with one that is trasparent (Alpha=0, R=0, G=0, B=0)
            //to do this we will use a Colormap and use this to define a RemapTable
            ColorMap colorMap = new ColorMap();

            //My watermark was defined with a background of 100% Green this will
            //be the color we search for and replace with transparency
            colorMap.OldColor = Color.FromArgb(255, 0, 255, 0);
            colorMap.NewColor = Color.FromArgb(0, 0, 0, 0);

            ColorMap[] remapTable = { colorMap };

            imageAttributes.SetRemapTable(remapTable, ColorAdjustType.Bitmap);

            //The second color manipulation is used to change the opacity of the 
            //watermark.  This is done by applying a 5x5 matrix that contains the 
            //coordinates for the RGBA space.  By setting the 3rd row and 3rd column 
            //to 0.3f we achive a level of opacity
            float[][] colorMatrixElements = { 
												new float[] {1.0f,  0.0f,  0.0f,  0.0f, 0.0f},       
												new float[] {0.0f,  1.0f,  0.0f,  0.0f, 0.0f},        
												new float[] {0.0f,  0.0f,  1.0f,  0.0f, 0.0f},        
												new float[] {0.0f,  0.0f,  0.0f,  0.3f, 0.0f},        
												new float[] {0.0f,  0.0f,  0.0f,  0.0f, 1.0f}};
            ColorMatrix wmColorMatrix = new ColorMatrix(colorMatrixElements);

            imageAttributes.SetColorMatrix(wmColorMatrix, ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);

            //For this example we will place the watermark in the upper right
            //hand corner of the photograph. offset down 10 pixels and to the 
            //left 10 pixles

            int xPosOfWm = ((phWidth - wmWidth) - 240);
            int yPosOfWm = 250;

            grWatermark.DrawImage(imgWatermark,
                new Rectangle(xPosOfWm, yPosOfWm, wmWidth, wmHeight),  //Set the detination Position
                0,                  // x-coordinate of the portion of the source image to draw. 
                0,                  // y-coordinate of the portion of the source image to draw. 
                wmWidth,            // Watermark Width
                wmHeight,		    // Watermark Height
                GraphicsUnit.Pixel, // Unit of measurment
                imageAttributes);   //ImageAttributes Object

            //Replace the original photgraphs bitmap with the new Bitmap
            imgPhoto = bmWatermark;
            grPhoto.Dispose();
            grWatermark.Dispose();

            //save new image to file system.
            imgPhoto.Save(pathString + "\\" + "wtr-" + img, ImageFormat.Jpeg);
            imgPhoto.Dispose();
            imgWatermark.Dispose();
            return pathString;
        }

        [AuditLog]
        public ActionResult EditWaterMark()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        [AuditLog(Event = "Save watermark", Message = "Save water mark image to database")] 
        public string saveWaterMark(string PostInformation)
        {
            smWatermark model = new smWatermark();
            JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            model = JsonConvert.DeserializeObject<smWatermark>(PostInformation);
            int ImpersonateUserId = SessionManager.LoggedInUser.ImpersonateUserId;
            int UserID = SessionManager.LoggedInUser.UserID;
            model.UserID = UserID;
            if (ImpersonateUserId!=0){

                model.CreatedBy = ImpersonateUserId;
            }
            else
            {
                model.CreatedBy = UserID;
            }           
            model.CreatedDate = DateTime.UtcNow;
            model.IsDeleted = false;
            smWatermark smWatermark = _scheduleService.AddWaterMark(model);
            return "true";
        }

       // [AuditLog]
        public ActionResult GetwaterMark(int userId)
        {
            smWatermark smWatermark = _scheduleService.GetWaterMarkDetails(userId);
            return Json(smWatermark, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        [AuditLog(Event = "Upload logo for watermark", Message = "Upload watermark image")] 
        public ActionResult SaveUploadedLogoWateramrk()
        {
            int userId = smartData.Common.SessionManager.LoggedInUser.UserID;
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
                        var originalDirectory = Server.MapPath("~/Images/WallImages");

                        //string pathString = System.IO.Path.Combine(originalDirectory.ToString(), Convert.ToString(userId));
                        string pathString = originalDirectory + "/" + userId + "/LogoImages";

                        var fileName1 = Path.GetFileName(file.FileName);

                        bool isExists = System.IO.Directory.Exists(pathString);

                        if (!isExists)
                            System.IO.Directory.CreateDirectory(pathString);

                        path = string.Format("{0}\\{1}", pathString, file.FileName);
                        file.SaveAs(path);

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

    }
}