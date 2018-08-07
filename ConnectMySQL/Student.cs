using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ConnectMySQL;

namespace ConnectMySQL
{
    [Table("Students")]
    public class Student
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        
        [Required]
        public Guid ClassId { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }
        
        
        
    }
}