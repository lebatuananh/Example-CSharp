using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConnectMySQL;

namespace ConnectMySQL
{
    [Table("Schools")]
    public class School
    {
        [Key]
        public Guid Id { get; set; }
        [Required] [StringLength(255)] public string Name { get; set; }
        public ICollection<Class> Classes { get; set; }
    }
}