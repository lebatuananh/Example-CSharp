using Authentication.Entity.Interface;
using Authentication.Utils.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Entity
{
    [Table("Functions")]
    public class Function : DomainEntity<Guid>, IDateTracking, IHasSoftDelete
    {
        public int SortOrder { get; set; }
        public Guid? ParentId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Url { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDelete { get; set; }
    }
}