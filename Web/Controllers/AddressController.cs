using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.ViewModels;

namespace Web.Controllers
{
    public class AddressController : Controller
    {
        readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://wilayahindo.herokuapp.com/api/")
        };

        public IActionResult Index()
        {
            return View("~/Views/Dashboard/Address.cshtml");
        }

        public IActionResult LoadProvince()
        {
            IEnumerable<LocationVM> dataVM = null;
            //AddressVM dataVM = null;
            var resTask = client.GetAsync("provinces/");
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<LocationVM>>();
                readTask.Wait();
                dataVM = readTask.Result;
            }
            else
            {
                dataVM = Enumerable.Empty<LocationVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(dataVM);

        }

        public IActionResult LoadCity(int name)
        {
            IEnumerable<LocationVM> dataVM = null;
            var resTask = client.GetAsync("kota/" + name);
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<LocationVM>>();
                readTask.Wait();
                dataVM = readTask.Result;
            }
            else
            {
                dataVM = Enumerable.Empty<LocationVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(dataVM);
        }

        public IActionResult LoadDistrict(string name)
        {
            IEnumerable<LocationVM> dataVM = null;
            var resTask = client.GetAsync("kecamatan/" + name);
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<LocationVM>>();
                readTask.Wait();
                dataVM = readTask.Result;
            }
            else
            {
                dataVM = Enumerable.Empty<LocationVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(dataVM);
        }

        public IActionResult LoadUrban(string name)
        {
            IEnumerable<LocationVM> dataVM = null;
            var resTask = client.GetAsync("kelurahan/" + name);
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<LocationVM>>();
                readTask.Wait();
                dataVM = readTask.Result;
            }
            else
            {
                dataVM = Enumerable.Empty<LocationVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(dataVM);
        }

        public IActionResult LoadZipCode(string name)
        {
            IEnumerable<LocationVM> dataVM = null;
            var resTask = client.GetAsync("zipcode/" + name);
            resTask.Wait();

            var result = resTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<LocationVM>>();
                readTask.Wait();
                dataVM = readTask.Result;
            }
            else
            {
                dataVM = Enumerable.Empty<LocationVM>();
                ModelState.AddModelError(string.Empty, "Server Error try after sometimes.");
            }
            return Json(dataVM);
        }

    }
}
