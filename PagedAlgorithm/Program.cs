using System;
using System.Collections.Generic;

namespace PagedAlgorithm
{
    public class Student
    {
        public string Name { get; set; }
    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var list = new List<Student>();
            for (var i = 1; i <= 100; i++)
            {
                list.Add(new Student() {Name = "NameStudent " + i});
            }
            var paginationSet=new PagedResult<Student>()
            {
                Results = list,
                CurrentPage = 2,
                RowCount = list.Count,
                PageSize = 8
            };
            Console.WriteLine(paginationSet.FirstRowOnPage);
            Console.WriteLine(paginationSet.LastRowOnPage);
            Console.WriteLine(paginationSet.PageCount);
            Console.WriteLine(paginationSet.RowCount);            
        }
    }
}