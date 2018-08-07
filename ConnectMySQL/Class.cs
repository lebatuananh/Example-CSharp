using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConnectMySQL
{
    [Table("Classes")]
    public class Class
    {
        public Class()
        {
            Students=new List<Student>();
        }
        [Key]
        public Guid Id { get; set; }

        [Required] [StringLength(255)] public string Name { get; set; }

        [Required]
        public Guid SchoolId { get; set; }

        [ForeignKey("SchoolId")] public virtual School School { get; set; }

        public ICollection<Student> Students { set; get; }
    }
}