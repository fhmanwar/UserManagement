using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class GetRoleVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Session { get; set; }
        public DateTimeOffset InsAt { get; set; }
        public DateTimeOffset UpdAt { get; set; }
    }

    public class GetUserVM
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string NIK { get; set; }
        public string Site { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string SubDistrict { get; set; }
        public string Village { get; set; }
        public string ZipCode { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string DivisionID { get; set; }
        public string DivisionName { get; set; }
        public string Session { get; set; }
    }
}
