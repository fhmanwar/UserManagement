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
    public class ReimbursRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public ReimbursRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<ReimbursVM>> getAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetAll_Reimburs";
                var getAll = await connection.QueryAsync<ReimbursVM>(procName, commandType: CommandType.StoredProcedure);
                return getAll;
            }
        }

        public ReimbursVM getID(string id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetID_Reimburs";
                parameters.Add("id", id);
                var getId = connection.Query<ReimbursVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return getId;
            }
        }
    }
}
