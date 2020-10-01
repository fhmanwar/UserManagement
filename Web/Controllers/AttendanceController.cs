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
    public class AttendanceController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://winarto-001-site1.dtempurl.com/api/")
        };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoadData()
        {
            IEnumerable<GetAbsentVM> dataVM = null;
            var resTask = client.GetAsync("absents/");
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<GetAbsentVM>>();
                readTask.Wait();
                dataVM = readTask.Result;
            }
            else
            {
                dataVM = Enumerable.Empty<GetAbsentVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(dataVM);

        }

        public IActionResult GetById(int Id)
        {
            GetAbsentVM data = null;

            var resTask = client.GetAsync("absents/" + Id);
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<GetAbsentVM>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error.");
            }
            return Json(data);
        }

        public IActionResult InsertOrUpdate(GetAbsentVM data, int id)
        {
            try
            {
                data.InsAt = DateTime.Now;
                data.UpdAt = DateTime.Now;
                var json = JsonConvert.SerializeObject(data);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                if (data.Id == 0)
                {
                    var result = client.PostAsync("absents/", byteContent).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return Json(result);
                    }
                    var getdata = result.Content.ReadAsStringAsync().Result;
                    return Json(new { result, msg = getdata });
                }
                else if (data.Id != 0)
                {
                    var result = client.PutAsync("absents/" + id, byteContent).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return Json(result);
                    }
                    var getdata = result.Content.ReadAsStringAsync().Result;
                    return Json(new { result, msg = getdata });
                }

                return Json(404);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex });
            }
        }

    }
}
