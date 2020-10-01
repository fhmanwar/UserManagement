using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class RoleVM 
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
        public string ProfileImages { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string SubDistrict { get; set; }
        public string Village { get; set; }
        public string ZipCode { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string Session { get; set; }
    }

    public class GetAbsentVM
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime InsAt { get; set; }
        public DateTime UpdAt { get; set; }
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
    }

    public class EmployeeVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string NIK { get; set; }
        public string Site { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string RoleName { get; set; }
        public string DepartmentName { get; set; }
    }

    public class LogVM
    {
        public string Response { get; set; }
        public string Email { get; set; }
        public string CreateDate { get; set; }
    }

    public class ForgotVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }

    public class LocationVM
    {
        public string ProvCode { get; set; }
        public string ProvinceName { get; set; }
        public string CityId { get; set; }
        public string City { get; set; }
        public string DistrictId { get; set; }
        public string District { get; set; }
        public string UrbanId { get; set; }
        public string Urban { get; set; }
        public string ZipCode { get; set; }
    }

    public class UploadImgVM
    {
        public string ImageName { get; set; }
    }

}
