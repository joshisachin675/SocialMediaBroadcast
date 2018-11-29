using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace smartData.Areas.Users
{
    public class shcedular : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //WriteToFile("Hello");
            try
            {
                SendAlert();
                SendAlertForlikes();
                WriteToFile("Service started at");
            }
            catch (Exception e)
            {

            }
            //var da = client.Dispute();
        }

        public void SendAlert()
        {
            try
            {
                //string content="Hello";
                string url = ConfigurationManager.AppSettings["SiteUrl"].ToString() + "/Schedule/PostContentSchedule";
                var request = WebRequest.Create(url);
                request.Method = "Get";
                //request.ContentLength = 0;
                WebResponse response = request.GetResponse();
                var dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // WriteToFile("Server returns " + responseFromServer + " {0}");
                // Display the content.
                // Console.WriteLine(responseFromServer);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                // WriteToFile(e.Message.ToString());
            }

        }


        public void SendAlertForlikes()
        {
            try
            {
                //string content="Hello";
                string url = ConfigurationManager.AppSettings["SiteUrl"].ToString() + "/Post/GetContentFromSocialMedia";
                var request = WebRequest.Create(url);
                request.Method = "Get";
                //request.ContentLength = 0;
                WebResponse response = request.GetResponse();
                var dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // WriteToFile("Server returns " + responseFromServer + " {0}");
                // Display the content.
                // Console.WriteLine(responseFromServer);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                // WriteToFile(e.Message.ToString());
            }

        }
        public void SaveAutoPostDataByPublishingTime()
        {
            try
            {
                //string content="Hello";
                string url = ConfigurationManager.AppSettings["SiteUrl"].ToString() + "/Schedule/SaveAutoPostDataByPublishingTime";
                var request = WebRequest.Create(url);
                request.Method = "Get";
                //request.ContentLength = 0;
                WebResponse response = request.GetResponse();
                var dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // WriteToFile("Server returns " + responseFromServer + " {0}");
                // Display the content.
                // Console.WriteLine(responseFromServer);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                // WriteToFile(e.Message.ToString());
            }

        }

        void WriteToFile(string text)
        {

            //ServiceReference1.HelpServiceClient data = new ServiceReference1.HelpServiceClient();
            //var da = data.Dispute();

            string path = "D:\\ServiceLog.txt";

            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(string.Format(text + "{0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
            writer.Close();
        }

    }
    public class shcedularPublisher : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            //WriteToFile("Hello");
            try
            {
            
                SaveAutoPostDataByPublishingTime();
                WriteToFile("Service started at");
            }
            catch (Exception e)
            {

            }
            //var da = client.Dispute();
        }
   
        public void SaveAutoPostDataByPublishingTime()
        {
            try
            {
                //string content="Hello";
                string url = ConfigurationManager.AppSettings["SiteUrl"].ToString() + "/Schedule/SaveAutoPostDataByPublishingTime";
                var request = WebRequest.Create(url);
                request.Method = "Get";
                //request.ContentLength = 0;
                WebResponse response = request.GetResponse();
                var dataStream = response.GetResponseStream();
                // Open the stream using a StreamReader for easy access.
                StreamReader reader = new StreamReader(dataStream);
                // Read the content.
                string responseFromServer = reader.ReadToEnd();
                // WriteToFile("Server returns " + responseFromServer + " {0}");
                // Display the content.
                // Console.WriteLine(responseFromServer);
                // Clean up the streams.
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception e)
            {
                // WriteToFile(e.Message.ToString());
            }

        }

        void WriteToFile(string text)
        {

            //ServiceReference1.HelpServiceClient data = new ServiceReference1.HelpServiceClient();
            //var da = data.Dispute();

            string path = "D:\\ServiceLog.txt";

            StreamWriter writer = new StreamWriter(path, true);
            writer.WriteLine(string.Format(text + "{0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt")));
            writer.Close();
        }

    }
    public class JobScheduler
    {
        public static void Start()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();
            IJobDetail job = JobBuilder.Create<shcedular>().Build();
            ITrigger trigger = TriggerBuilder.Create()
              .WithDailyTimeIntervalSchedule
                (s =>
                   s.WithIntervalInSeconds(60)
                  .OnEveryDay()
                  .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                )
              .Build();
            sched.ScheduleJob(job, trigger);
            //-----------------------------------------------------
        }
    }
    public class JodForLikes
    {
        public static void Start()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();
            IJobDetail job = JobBuilder.Create<shcedular>().Build();
            ITrigger trigger = TriggerBuilder.Create()
              .WithDailyTimeIntervalSchedule
                (s =>
                   s.WithIntervalInSeconds(270)
                  .OnEveryDay()
                  .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                )
              .Build();
            sched.ScheduleJob(job, trigger);
            //-----------------------------------------------------
        }
    }


    public class JodAutoPost
    {
        public static void Start()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            // get a scheduler
            IScheduler sched = schedFact.GetScheduler();
            sched.Start();
            IJobDetail job = JobBuilder.Create<shcedularPublisher>().Build();
            ITrigger trigger = TriggerBuilder.Create()
              .WithDailyTimeIntervalSchedule
                (s =>
                   s.WithIntervalInSeconds(150)
                  .OnEveryDay()
                  .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                )
              .Build();
            sched.ScheduleJob(job, trigger);
            //-----------------------------------------------------
        }
    }

}
