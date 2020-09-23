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
    public class UserRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Create(UserVM userVM)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Create_User";
                parameters.Add("Mail", userVM.Email);
                parameters.Add("Pass", Bcrypt.HashPassword(userVM.Password));
                parameters.Add("Code", userVM.VerifyCode);
                var insert = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return insert;
            }
        }

        public int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Delete_User";
                parameters.Add("id", id);
                var Delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Delete;
            }
        }

        public async Task<IEnumerable<UserVM>> getAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetAll_User";
                var getAll = await connection.QueryAsync<UserVM>(procName, commandType: CommandType.StoredProcedure);
                return getAll;
            }
        }

        public UserVM getID(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetID_User";
                parameters.Add("id", id);
                var getId = connection.Query<UserVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return getId;
            }
        }

        public int Update(UserVM userVM, int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Update_User";
                parameters.Add("Id", id);
                parameters.Add("Mail", userVM.Email);
                parameters.Add("Pass", Bcrypt.HashPassword(userVM.Password));
                parameters.Add("Code", userVM.VerifyCode);
                var Edit = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Edit;
            }
        }
    }

    public class RoleRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public RoleRepository(IConfiguration configuration)
        {
            _configuration = configuration;
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

        public int Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Delete_Role";
                parameters.Add("id", id);
                var Delete = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Delete;
            }
        }

        public async Task<IEnumerable<UserVM>> getAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetAll_Role";
                var getAll = await connection.QueryAsync<UserVM>(procName, commandType: CommandType.StoredProcedure);
                return getAll;
            }
        }

        public UserVM getID(int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetID_Role";
                parameters.Add("id", id);
                var getId = connection.Query<UserVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return getId;
            }
        }

        public int Update(UserVM userVM, int id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_Update_Role";
                parameters.Add("Id", id);
                parameters.Add("Mail", userVM.Email);
                parameters.Add("Pass", Bcrypt.HashPassword(userVM.Password));
                parameters.Add("Code", userVM.VerifyCode);
                var Edit = connection.Execute(procName, parameters, commandType: CommandType.StoredProcedure);
                return Edit;
            }
        }
    }
}
