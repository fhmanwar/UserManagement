﻿using API.ViewModels;
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

    
}
