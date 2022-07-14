using System;
namespace ChealCore.Models.ViewModels
{
    public class RoleClaimViewModel
    {
        public string RoleId { get; set; }
        public List<RoleClaims> Claims { get; set; } = new List<RoleClaims>();
    }
}
