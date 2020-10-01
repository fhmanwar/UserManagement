using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class ChartVM
    {
    }

    public class PieChartUserRoleVM
    {
        public string RoleName { get; set; }
        public int Total { get; set; }
    }
    public class PieChartUserDivVM
    {
        public string DepartmentName { get; set; }
        public int Total { get; set; }
    }
    public class BarChartVM
    {
        public string Title { get; set; }
        public double star1 { get; set; }
        public double star2 { get; set; }
        public double star3 { get; set; }
        public double star4 { get; set; }
        public double star5 { get; set; }
    }

}
