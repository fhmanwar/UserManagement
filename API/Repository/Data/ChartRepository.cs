using API.ViewModels;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class ChartRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public ChartRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<PieChartUserRoleVM>> getPieUserRole()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_ChartPie_UserRole";
                var getAll = await connection.QueryAsync<PieChartUserRoleVM>(procName, commandType: CommandType.StoredProcedure);
                return getAll;
            }
        }

        public async Task<IEnumerable<PieChartUserDivVM>> getPieUserDiv()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_ChartPie_UserDiv";
                var getAll = await connection.QueryAsync<PieChartUserDivVM>(procName, commandType: CommandType.StoredProcedure);
                return getAll;
            }
        }

        //public async Task<IEnumerable<LineChartVM>> getLine()
        //{
        //    using (MySqlConnection connection = new MySqlConnection(_configuration.GetConnectionString("myConn")))
        //    {
        //        var procName = "ChartSPLine";
        //        var getAll = await connection.QueryAsync<LineChartVM>(procName, commandType: CommandType.StoredProcedure);
        //        return getAll;
        //    }
        //}
    }
}
