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
    public class RoleController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://winarto-001-site1.dtempurl.com/api/")
            //BaseAddress = new Uri("https://localhost:44356/api/")
        };

        public IActionResult Index()
        {
            return View("~/Views/Dashboard/Role.cshtml");
        }

        public IActionResult LoadData()
        {
            IEnumerable<RoleVM> roleVMs = null;
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var resTask = client.GetAsync("roles/");
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<RoleVM>>();
                readTask.Wait();
                roleVMs = readTask.Result;
            }
            else
            {
                roleVMs = Enumerable.Empty<RoleVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(roleVMs);

        }

        public IActionResult GetById(string Id)
        {
            RoleVM data = null;
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var resTask = client.GetAsync("roles/" + Id);
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<RoleVM>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error.");
            }
            return Json(data);
        }

        public IActionResult InsertOrUpdate(RoleVM data, string id)
        {
            try
            {
                AuthController controller = new AuthController();
                data.Session = HttpContext.Session.GetString("id");
                var json = JsonConvert.SerializeObject(data);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
                if (data.Id == null)
                {
                    var result = client.PostAsync("roles/", byteContent).Result;
                    controller.SendLogs(HttpContext.Session.GetString("email") + " Create role", HttpContext.Session.GetString("email"));
                    return Json(result);
                }
                else if (data.Id == id)
                {
                    var result = client.PutAsync("roles/" + id, byteContent).Result;
                    controller.SendLogs(HttpContext.Session.GetString("email") + " Update role", HttpContext.Session.GetString("email"));
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
            var result = client.DeleteAsync("roles/" + id).Result;
            AuthController controller = new AuthController();
            controller.SendLogs(HttpContext.Session.GetString("email") + " Delete role", HttpContext.Session.GetString("email"));
            return Json(result);
        }

    }

    public class AccountController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://winarto-001-site1.dtempurl.com/api/")
            //BaseAddress = new Uri("https://localhost:44356/api/")
        };
        public IActionResult Index()
        {
            return View("~/Views/Dashboard/Account.cshtml");
        }
        public IActionResult LoadData()
        {
            IEnumerable<GetUserVM> dataVM = null;
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var resTask = client.GetAsync("users/");
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<GetUserVM>>();
                readTask.Wait();
                dataVM = readTask.Result;
            }
            else
            {
                dataVM = Enumerable.Empty<GetUserVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(dataVM);

        }

        public IActionResult GetById(string Id)
        {
            GetUserVM data = null;
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var resTask = client.GetAsync("users/" + Id);
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var json = JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result).ToString();
                data = JsonConvert.DeserializeObject<GetUserVM>(json);
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Server Error.");
            }
            return Json(data);
        }

        public IActionResult InsertOrUpdate(GetUserVM data, string id)
        {
            try
            {
                AuthController controller = new AuthController();
                data.Session = HttpContext.Session.GetString("id");
                var json = JsonConvert.SerializeObject(data);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
                if (data.Id == null)
                {
                    var result = client.PostAsync("users/", byteContent).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        controller.SendLogs(HttpContext.Session.GetString("email") + " Create Account", HttpContext.Session.GetString("email"));
                        return Json(result);
                    }
                    var getdata = result.Content.ReadAsStringAsync().Result;
                    return Json(new { result, msg = getdata });
                }
                else if (data.Id == id)
                {
                    var result = client.PutAsync("users/" + id, byteContent).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        controller.SendLogs(HttpContext.Session.GetString("email") + " Update Account", HttpContext.Session.GetString("email"));
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

        public IActionResult Delete(string id)
        {
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var result = client.DeleteAsync("users/" + id).Result;
            if (result.IsSuccessStatusCode)
            {
                AuthController controller = new AuthController();
                controller.SendLogs(HttpContext.Session.GetString("email") + " Delete Account", HttpContext.Session.GetString("email"));
                return Json(result);
            }
            var getdata = result.Content.ReadAsStringAsync().Result;
            return Json(new { result, msg = getdata });
        }

    }
}
