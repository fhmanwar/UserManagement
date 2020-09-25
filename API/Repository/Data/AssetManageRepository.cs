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
    public class AssetManageRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public AssetManageRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<AssetVM>> getAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetAll_AssetMng";
                var getAll = await connection.QueryAsync<AssetVM>(procName, commandType: CommandType.StoredProcedure);
                return getAll;
            }
        }

        public AssetVM getID(string id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetID_AssetMng";
                parameters.Add("id", id);
                var getId = connection.Query<AssetVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return getId;
            }
        }
    }
}
