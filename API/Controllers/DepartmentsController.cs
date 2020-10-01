using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Base;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController<Department, DepartmentRepository>
    {
        readonly DepartmentRepository _departRepo;
        public DepartmentsController(DepartmentRepository departRepo) : base(departRepo)
        {
            _departRepo = departRepo;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<int>> Update(string id, Department entity)
        {
            var getId = await _departRepo.GetID(id);
            getId.Name = entity.Name;
            var data = await _departRepo.Update(getId);
            if (data.Equals(null))
            {
                return BadRequest("Something Wrong! Please check again");
            }
            return data;
        }
    }
}
