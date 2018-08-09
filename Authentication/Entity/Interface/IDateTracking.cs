using System;

namespace Authentication.Entity.Interface
{
    internal interface IDateTracking
    {
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
    }
}