using Authentication.Utils.Enum;
using System;
using System.Collections.Generic;

namespace Authentication.Applications.ViewModel
{
    public class AppUserViewModel
    {
        public AppUserViewModel()
        {
            Roles = new List<AppRoleViewModel>();
        }

        public Guid Id { set; get; }
        public string FullName { set; get; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string UserName { set; get; }
        public string Address { get; set; }
        public string PhoneNumber { set; get; }
        public string Avatar { get; set; }
        public Status Status { get; set; }
        public string Gender { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDelete { get; set; }
        public ICollection<AppRoleViewModel> Roles { get; set; }
    }
}