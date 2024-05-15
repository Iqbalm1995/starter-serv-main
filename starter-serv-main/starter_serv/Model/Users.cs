using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace starter_serv.Model
{
    public class Users : EntityBase
    {
        [Key]
        public Int64 Id { get; set; }
        public string UserGeneratedId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsSuperAdmin { get; set; }
        public int IsClient { get; set; }
        public int IsActive { get; set; }
        public string EmailVerified { get; set; }
        public string? EmailVerificationCode { get; set; }
        public int? OnlineStatus { get; set; }
        public string? Avatar { get; set; }
        public string? LastIp { get; set; }
        public DateTime? LastLogin { get; set; }
        public string? EmpId { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
        public string Language { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Skype { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string? Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? MaritialStatus { get; set; }
        public string? FatherName { get; set; }
        public string? MotherName { get; set; }
        public string? Permission { get; set; }
        public int? PrimaryManager { get; set; }
        public int? SecondaryManager { get; set; }
        public string? RememberToken { get; set; }
    }

    public class UserRoleDepartement
    {
        [Key]
        public Int64 Id { get; set; }
        public Int64 DepartmentId { get; set; }
        public Int64 RoleId { get; set; }
        public Int64 UserId { get; set; }
    }
}
