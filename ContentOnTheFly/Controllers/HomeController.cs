using ContentOnTheFly.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContentOnTheFly.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IndexModel IM = new IndexModel();
            IM.ApplicationContent = IndexPageContent();
            return View(IM);
        }

        public string IndexPageContent()
        {
            try
            {
               // Provide the connection key of the Azure Storage Account which you want to connect to as the parameter
               //In order to get the connection key Goto Azure Portal -> Storage Accounts -> Select the Storage Account  -> Access Keys -> Select either of the twoconnection strings present there
                
                CloudStorageAccount CSA = CloudStorageAccount.Parse("Content On The Fly");
                CloudBlobClient CBC = CSA.CreateCloudBlobClient();

                //Provide the name of the container as the Parameter
                CloudBlobContainer Container = CBC.GetContainerReference("indexpagecontent");
                CloudBlockBlob Blob = Container.GetBlockBlobReference("DemoApp.json");
                var stream = new MemoryStream();
                var DemoAppContent = new object();
                Blob.DownloadToStream(stream);
                stream.Position = 0;
                StreamReader Reader = new StreamReader(stream);
                var serializer = new JsonSerializer();

                using (var jsonReader = new JsonTextReader(Reader))
                {
                    DemoAppContent = serializer.Deserialize(jsonReader);
                }
                return DemoAppContent.ToString();
            }
            catch (Exception ex)
            {
               return ex.Message;
           }
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}