using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ChartsController : ControllerBase
    {
        ChartRepository _chartRepo;
        public ChartsController(ChartRepository chartRepo)
        {
            _chartRepo = chartRepo;
        }
        [HttpGet]
        [Route("pieuserrole")]
        public async Task<IEnumerable<PieChartUserRoleVM>> GetPieURole() => await _chartRepo.getPieUserRole();

        [HttpGet]
        [Route("pieuserdiv")]
        public async Task<IEnumerable<PieChartUserDivVM>> GetPieUDiv() => await _chartRepo.getPieUserDiv();

        //[HttpGet]
        //[Route("bar")]
        //public async Task<List<BarChartVM>> GetBar()
        //{ // total feedback pada title
        //    var getTitle = await _context.Feedbacks
        //                            .Include("Question")
        //                            .Include(x => x.Question.Training)
        //                            .GroupBy(grup => grup.Question.Training.Title)
        //                            .Select(y => new PieChartVM
        //                            {
        //                                Title = y.Key,
        //                                Total = y.Count()
        //                            })
        //                            .ToListAsync();

        //    if (getTitle.Count == 0)
        //    {
        //        return null;
        //    }
        //    List<BarChartVM> list = new List<BarChartVM>();
        //    var getCount5 = 0.0;
        //    var getCount4 = 0.0;
        //    var getCount3 = 0.0;
        //    var getCount2 = 0.0;
        //    var getCount1 = 0.0;
        //    foreach (var item in getTitle)
        //    {
        //        var getLengthTitle = await _context.Feedbacks
        //                           .Include("Question")
        //                           .Include(x => x.Question.Training)
        //                           .Where(x => x.Question.Training.Title == item.Title)
        //                           .ToListAsync();
        //        foreach (var item2 in getLengthTitle)
        //        {
        //            if (item2.Rate > 4.0)
        //            {
        //                getCount5++;
        //            }
        //            else if (item2.Rate > 3.0)
        //            {
        //                getCount4++;
        //            }
        //            else if (item2.Rate > 2.0)
        //            {
        //                getCount3++;
        //            }
        //            else if (item2.Rate > 1.0)
        //            {
        //                getCount2++;
        //            }
        //            else if (item2.Rate == 1.0)
        //            {
        //                getCount1++;
        //            }
        //        }

        //        var top = new BarChartVM()
        //        {
        //            Title = item.Title,
        //            star1 = getCount5,
        //            star2 = getCount4,
        //            star3 = getCount3,
        //            star4 = getCount2,
        //            star5 = getCount1,
        //        };
        //        list.Add(top);
        //    }
        //    return list;
        //}
    }
}
