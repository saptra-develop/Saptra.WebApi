using Newtonsoft.Json.Linq;
using Saptra.WebApi.Models;
using System;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Http;

namespace Saptra.WebApi.Controllers
{
    [RoutePrefix("Saptra/CheckIn")]
    public class CheckInController : ApiController
    {
        [HttpPost]
        [Route("PostCheckIn")]
        public IHttpActionResult PostCheckIn(Object obj)
        {
            try
            {
                String path = ConfigurationManager.AppSettings["Path"];
                var jsonObject = JObject.FromObject(obj);
                var checkin = jsonObject.ToObject<mCheckIn>();
                var bytes = jsonObject["ImageData"].ToString();
                Byte[] b_img = Convert.FromBase64String(bytes);
                MemoryStream ms = new MemoryStream(b_img, 0, b_img.Length);
                Image image = System.Drawing.Image.FromStream(ms, true);
                image.Save(Path.Combine(path, Guid.NewGuid().ToString() + ".jpeg"), System.Drawing.Imaging.ImageFormat.Jpeg);
                
                //using (Stream file = File.OpenWrite(Path.Combine(path, Guid.NewGuid().ToString()+".jpeg")))
                //{
                //    file.Write(b_img, 0, b_img.Length);
                //}
                return Json("");
            }
            catch (HttpResponseException ex)
            {
                return Json(ex.StackTrace);
            }
        }
    }
}
