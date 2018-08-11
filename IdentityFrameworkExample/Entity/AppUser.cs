using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityFrameworkExample.Entity
{
    using Microsoft.AspNetCore.Identity;
    public class AppUser:IdentityUser<Guid>
    {
        public string FullName { get; set; }

        public DateTime? BirthDay { set; get; }

        public decimal Balance { get; set; }

        public string Avatar { get; set; }

        public DateTime DateCreated { set; get; }
        public DateTime? DateModified { set; get; }
        public DateTime? DateDeleted { set; get; }
    }
}
