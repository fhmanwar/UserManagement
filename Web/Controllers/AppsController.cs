using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class AppsController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://winarto-001-site1.dtempurl.com/api/")
        };

        public IActionResult Asset()
        {
            if (HttpContext.Session.GetString("lvl") == "Super Admin")
            {
                return View("~/Views/Apps/AssetManagement.cshtml");
            }
            else
            {
                return Redirect("/profile");
            }
        }

        public IActionResult GetAsset()
        {
            IEnumerable<AssetVM> dataVM = null;
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var resTask = client.GetAsync("assetmanages/");
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<AssetVM>>();
                readTask.Wait();
                dataVM = readTask.Result;
            }
            else
            {
                dataVM = Enumerable.Empty<AssetVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(dataVM);
        }

        public IActionResult Exam()
        {
            if (HttpContext.Session.GetString("lvl") == "Super Admin")
            {
                return View("~/Views/Apps/ExamOnline.cshtml");
            }
            else
            {
                return Redirect("/profile");
            }
        }

        public IActionResult GetExam()
        {
            IEnumerable<ExamVM> dataVM = null;
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var resTask = client.GetAsync("exams/");
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<ExamVM>>();
                readTask.Wait();
                dataVM = readTask.Result;
            }
            else
            {
                dataVM = Enumerable.Empty<ExamVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(dataVM);
        }

        public IActionResult Interview()
        {
            if (HttpContext.Session.GetString("lvl") == "Super Admin")
            {
                return View("~/Views/Apps/Interview.cshtml");
            }
            else
            {
                return Redirect("/profile");
            }
        }

        public IActionResult GetInterview()
        {
            IEnumerable<InterviewVM> dataVM = null;
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var resTask = client.GetAsync("interviews/");
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<InterviewVM>>();
                readTask.Wait();
                dataVM = readTask.Result;
            }
            else
            {
                dataVM = Enumerable.Empty<InterviewVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(dataVM);
        }

        public IActionResult Reimbursement()
        {
            if (HttpContext.Session.GetString("lvl") == "Super Admin")
            {
                return View("~/Views/Apps/Reimbursement.cshtml");
            }
            else
            {
                return Redirect("/profile");
            }
        }

        public IActionResult GetReimbursement()
        {
            IEnumerable<ReimbursVM> dataVM = null;
            client.DefaultRequestHeaders.Add("Authorization", HttpContext.Session.GetString("token"));
            var resTask = client.GetAsync("reimburs/");
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<ReimbursVM>>();
                readTask.Wait();
                dataVM = readTask.Result;
            }
            else
            {
                dataVM = Enumerable.Empty<ReimbursVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(dataVM);
        }
    }
}
