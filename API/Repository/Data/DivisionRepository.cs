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
    public class DivisionRepository : BaseRepo<Division, MyContext>
    {
        public DivisionRepository(MyContext context) : base(context)
        {

        }
    }
}
