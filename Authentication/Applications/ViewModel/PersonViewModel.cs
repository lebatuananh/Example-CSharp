using Authentication.Utils.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Applications.ViewModel
{
    public class PersonViewModel
    {
        public Guid Id { get; set; }
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