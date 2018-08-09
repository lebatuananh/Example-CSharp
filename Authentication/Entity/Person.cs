using Authentication.Entity.Interface;
using Authentication.Utils.Enum;
using Authentication.Utils.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Entity
{
    [Table("Persons")]
    public class Person : DomainEntity<Guid>, IDateTracking, IHasSoftDelete
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string Job { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}