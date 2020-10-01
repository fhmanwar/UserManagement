using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.ViewModels;

namespace Web.Controllers
{
    public class DashboardController : Controller
    {
        readonly IHostingEnvironment _appEnvironment;

        public DashboardController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://winarto-001-site1.dtempurl.com/api/")
        };

        public IActionResult Index()
        {
            if (HttpContext.Session.IsAvailable)
            {
                if (HttpContext.Session.GetString("lvl") == "Super Admin")
                {
                    return View();
                }
                else
                {
                    return View("~/Views/Dashboard/UserProfile.cshtml");
                    //return Redirect("/profile");
                }
            }
            return RedirectToAction("Login", "Auth");
        }

        [Route("user")]
        public IActionResult UserPage()
        {
            return View("~/Views/Dashboard/User.cshtml");
        }

        [Route("presence")]
        public IActionResult Presence()
        {
            return View();
        }

        [Route("profile")]
        public IActionResult Profile()
        {
            return View("~/Views/Dashboard/Profile.cshtml");
        }

        [Route("GetProfile")]
        public IActionResult GetProfile()
        {
            GetUserVM data = null;
            var id = HttpContext.Session.GetString("id");
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var resTask = client.GetAsync("users/" + id);
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

        [Route("updProfile")]
        public IActionResult UpdProfile(GetUserVM data)
        {
            var id = HttpContext.Session.GetString("id");
            try
            {
                AuthController controller = new AuthController();
                data.ProfileImages = Path.GetFileName(data.ProfileImages);
                data.Session = HttpContext.Session.GetString("id");
                var json = JsonConvert.SerializeObject(data);
                var buffer = System.Text.Encoding.UTF8.GetBytes(json);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                if (data.Id == id)
                {
                    client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
                    var result = client.PutAsync("users/" + id, byteContent).Result;
                    if (result.IsSuccessStatusCode)
                    {
                        HttpContext.Session.Remove("name");
                        HttpContext.Session.Remove("img");
                        HttpContext.Session.SetString("name", data.Name);
                        HttpContext.Session.SetString("img", data.ProfileImages);
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
                throw ex;
            }
        }

        public async Task<IActionResult> UploadImage(UploadImgVM uploadImgVM)
        {
            if (ModelState.IsValid)
            {
                var rootPath = Path.Combine(_appEnvironment.WebRootPath, "upload\\profiles");
                var files = HttpContext.Request.Form.Files;
                var changeName = "";
                // get file without loop
                //var fileName = Path.GetFileNameWithoutExtension(files[0].FileName);
                //var extension = Path.GetExtension(files[0].FileName);
                //var cek2 = files[0].FileName; 
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        // get file with loop
                        var file = Image;
                        //There is an error here
                        //var cek1 = file.FileName; 
                        var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                        var extension = Path.GetExtension(file.FileName);

                        if (file.Length > 0)
                        {
                            changeName = fileName.ToString().Replace(" ", "_")+ "_" + DateTime.Now.ToString("dd-MMM-yyyy") + extension;
                            var uploadPath = Path.Combine(rootPath, changeName);
                            using (var fileStream = new FileStream(uploadPath, FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                uploadImgVM.ImageName = changeName;
                            }

                        }
                    }
                }
                return Json(new { imgName = changeName, msg = "Change Successfully" });
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return Json(errors);
            }

        }

        public IActionResult LoadLog()
        {
            IEnumerable<LogVM> log = null;
            //var token = HttpContext.Session.GetString("token");
            //client.DefaultRequestHeaders.Add("Authorization", token);
            var resTask = client.GetAsync("logs");
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<LogVM>>();
                readTask.Wait();
                log = readTask.Result;
            }
            else
            {
                log = Enumerable.Empty<LogVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(log);
        }

        public IActionResult LoadPieURole()
        {
            IEnumerable<PieChartUserRoleVM> pieURole = null;
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var resTask = client.GetAsync("charts/pieuserrole");
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<PieChartUserRoleVM>>();
                readTask.Wait();
                pieURole = readTask.Result;
            }
            else
            {
                pieURole = Enumerable.Empty<PieChartUserRoleVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(pieURole);
        }

        public IActionResult LoadPieUDiv()
        {
            IEnumerable<PieChartUserDivVM> pieUDiv = null;
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var resTask = client.GetAsync("charts/pieuserdiv");
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<PieChartUserDivVM>>();
                readTask.Wait();
                pieUDiv = readTask.Result;
            }
            else
            {
                pieUDiv = Enumerable.Empty<PieChartUserDivVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(pieUDiv);
        }

    }
}
