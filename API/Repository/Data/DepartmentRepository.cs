using API.Context;
using API.Models;
using API.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class DepartmentRepository : BaseRepo<Department, MyContext>
    {
        public DepartmentRepository(MyContext context) : base(context)
        {

        }
    }
}
