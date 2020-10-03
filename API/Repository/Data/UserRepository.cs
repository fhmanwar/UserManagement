using API.ViewModels;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Bcrypt = BCrypt.Net.BCrypt;

namespace API.Repository.Data
{
    public class RoleRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public RoleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<GetRoleVM>> getAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetAll_Role";
                var getAll = await connection.QueryAsync<GetRoleVM>(procName, commandType: CommandType.StoredProcedure);
                return getAll;
            }
        }

        public GetRoleVM getID(string id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetID_Role";
                parameters.Add("id", id);
                var getId = connection.Query<GetRoleVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return getId;
            }
        }

        public int Create(RoleVM roleVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Create_Role";
                parameters.Add("name", roleVM.Name);
                parameters.Add("insDate", DateTimeOffset.Now);
                var insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }
        }

        public int Update(RoleVM roleVM, string id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Update_Role";
                parameters.Add("Id", id);
                parameters.Add("Mail", roleVM.Name);
                var Edit = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Edit;
            }
        }

        public int Delete(string id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Delete_Role";
                parameters.Add("id", id);
                var Delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Delete;
            }
        }

    }

    public class UserRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<GetUserVM>> getAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetAll_User";
                var getAll = await connection.QueryAsync<GetUserVM>(procName, commandType: CommandType.StoredProcedure);
                return getAll;
            }
        }

        public GetUserVM getID(string id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetID_User";
                parameters.Add("id", id);
                var getId = connection.Query<GetUserVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return getId;
            }
        }

        public int Create(UserVM userVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Create_User";
                parameters.Add("Mail", userVM.Email);
                parameters.Add("Pass", Bcrypt.HashPassword(userVM.Password));
                parameters.Add("Code", userVM.VerifyCode);
                parameters.Add("Token", userVM.Token);
                var insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }
        }
        public int Update(UserVM userVM, string id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Update_User";
                parameters.Add("Id", id);
                parameters.Add("Mail", userVM.Email);
                parameters.Add("Pass", userVM.Password);
                parameters.Add("Code", userVM.VerifyCode);
                parameters.Add("Token", userVM.Token);
                var Edit = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Edit;
            }
        }

        public int Delete(string id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Delete_User";
                parameters.Add("id", id);
                var Delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Delete;
            }
        }

    }

    public class AbsentRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public AbsentRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<GetAbsentVM>> getAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetAll_Absent";
                var getAll = await connection.QueryAsync<GetAbsentVM>(procName, commandType: CommandType.StoredProcedure);
                return getAll;
            }
        }

        public GetAbsentVM getID(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetID_Absent";
                parameters.Add("id", id);
                var getId = connection.Query<GetAbsentVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return getId;
            }
        }

        public int Create(AbsentVM dataVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Create_Absent";
                parameters.Add("userId", dataVM.UserId);
                parameters.Add("insDate", dataVM.InsDate);
                var insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }
        }

        public int Update(AbsentVM dataVM, int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Update_Absent";
                parameters.Add("id", id);
                parameters.Add("updDate", dataVM.UpdDate);
                var Edit = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Edit;
            }
        }

    }

    public class AppsRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public AppsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<AppsVM>> getAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetAll_Apps";
                var getAll = await connection.QueryAsync<AppsVM>(procName, commandType: CommandType.StoredProcedure);
                return getAll;
            }
        }

        public AppsVM getID(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetID_Apps";
                parameters.Add("id", id);
                var getId = connection.Query<AppsVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return getId;
            }
        }

        public int Create(AppsVM dataVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Create_Apps";
                parameters.Add("name", dataVM.Name);
                parameters.Add("insDate", DateTimeOffset.Now);
                var insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }
        }

        public int Update(AppsVM dataVM, int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Update_Apps";
                parameters.Add("Id", id);
                parameters.Add("name", dataVM.Name);
                parameters.Add("updDate", DateTimeOffset.Now);
                var Edit = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Edit;
            }
        }

        public int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Delete_Apps";
                parameters.Add("id", id);
                parameters.Add("DelDate", DateTimeOffset.Now);
                var Delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Delete;
            }
        }

    }
}
