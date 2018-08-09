using System;
using System.ComponentModel.DataAnnotations;

namespace Authentication.Applications.ViewModel
{
    public class FunctionViewModel
    {
        public Guid Id { get; set; }
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