﻿using API.ViewModels;
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
    public class InterviewRepository
    {
        IConfiguration _configuration;
        DynamicParameters parameters = new DynamicParameters();
        public InterviewRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<InterviewVM>> getAll()
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetAll_Exam";
                var getAll = await connection.QueryAsync<InterviewVM>(procName, commandType: CommandType.StoredProcedure);
                return getAll;
            }
        }

        public InterviewVM getID(string id)
        {
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myConnection")))
            {
                var procName = "SP_GetID_Exam";
                parameters.Add("id", id);
                var getId = connection.Query<InterviewVM>(procName, parameters, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return getId;
            }
        }
    }
}