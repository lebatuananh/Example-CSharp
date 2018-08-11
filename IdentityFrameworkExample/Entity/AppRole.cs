using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityFrameworkExample.Entity
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Identity;
    public class AppRole : IdentityRole<Guid>
    {
        [StringLength(250)]
        public string Description { get; set; }
    }
}
