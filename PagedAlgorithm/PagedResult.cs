using System.Collections.Generic;

namespace PagedAlgorithm
{
    public class PagedResult<T>:PagedResultBase where T:class
    {
        public PagedResult()
        {
            Results=new List<T>();
        }

        public IList<T> Results { get; set; }
    }
    
}