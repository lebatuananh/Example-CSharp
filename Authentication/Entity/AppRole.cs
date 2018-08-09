using Authentication.Entity.Interface;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Entity
{
    [Table("AppRoles")]
    public class AppRole : IdentityRole<Guid>, IDateTracking, IHasSoftDelete
    {
        public AppRole() : base()
        {
        }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}