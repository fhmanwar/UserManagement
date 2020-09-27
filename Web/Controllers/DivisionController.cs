using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.ViewModels;

namespace Web.Controllers
{
    public class DivisionController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://winarto-001-site1.dtempurl.com/api/")
        };
        public IActionResult Index()
        {
            return View("~/Views/Dashboard/Division.cshtml");
        }


        public IActionResult LoadData()
        {
            IEnumerable<DivisionVM> dataVMs = null;
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var resTask = client.GetAsync("divisions/");
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<DivisionVM>>();
                readTask.Wait();
                dataVMs = readTask.Result;
            }
            else
            {
                dataVMs = Enumerable.Empty<DivisionVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(dataVMs);

        }

        public IActionResult GetById(string Id)
        {
            DivisionVM data = null;
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var resTask = client.GetAsync("divisions/" + Id);
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<DivisionVM>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error.");
            }
            return Json(data);
        }

        public IActionResult InsertOrUpdate(DivisionVM data, string id)
        {
            try
            {
                AuthController controller = new AuthController();
                var Session = HttpContext.Session.GetString("email");
                var json = JsonConvert.SerializeObject(data);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
                if (data.Id == null)
                {
                    var result = client.PostAsync("divisions/", byteContent).Result;
                    controller.SendLogs(Session + " Create role", Session);
                    return Json(result);
                }
                else if (data.Id == id)
                {
                    var result = client.PutAsync("divisions/" + id, byteContent).Result;
                    controller.SendLogs(Session + " Update role", Session);
                    return Json(result);
                }

                return Json(404);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IActionResult Delete(string id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var result = client.DeleteAsync("divisions/" + id).Result;
            AuthController controller = new AuthController();
            controller.SendLogs(HttpContext.Session.GetString("email") + " Delete role", HttpContext.Session.GetString("email"));
            return Json(result);
        }
    }
}
