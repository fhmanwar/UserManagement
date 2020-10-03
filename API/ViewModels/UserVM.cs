using API.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class RoleVM 
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset DeleteDate { get; set; }
        public bool isDelete { get; set; }
    }

    public class UserVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string VerifyCode { get; set; }
        public string Token { get; set; }
        public string RoleName { get; set; }
        public string DepartmentName { get; set; }
    }

    public class EmployeeVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NIK { get; set; }
        public string Site { get; set; }
        public string Phone { get; set; }
        public string ProfileImages { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string SubDistrict { get; set; }
        public string Village { get; set; }
        public string ZipCode { get; set; }
        public string RoleName { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string AppsID { get; set; }
        public string AppsName { get; set; }
    }

    public class ForgotVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LogVM
    {
        public string Response { get; set; }
        public string Email { get; set; }
    }

    public class AbsentVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset InsDate { get; set; }
        public DateTimeOffset UpdDate { get; set; }
    }

    public class AppsVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTimeOffset InsAt { get; set; }
        public DateTimeOffset UpdAt { get; set; }
        public DateTimeOffset DelAt { get; set; }
        public bool isDelete { get; set; }
    }

}
