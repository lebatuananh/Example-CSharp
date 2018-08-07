using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConnectMySQL
{
    public class DbInitializer
    {
        private readonly AppDbContext _appDbContext;

        public DbInitializer(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task Seed()
        {
            Guid guidScholl1 = new Guid();
            Guid guidScholl2 = new Guid();
            Guid guidScholl3 = new Guid();
            Guid guidScholl4 = new Guid();
            Guid guidSchool5 = new Guid();

            if (!this._appDbContext.Schools.Any())
            {
                List<School> listSchools = new List<School>()
                {
                    new School() {Id = guidScholl1, Name = "School 1"},
                    new School() {Id = guidScholl1, Name = "School 2"},
                    new School() {Id = guidScholl1, Name = "School 3"},
                    new School() {Id = guidScholl1, Name = "School 4"},
                    new School() {Id = guidScholl1, Name = "School 5"}
                    
                };
                this._appDbContext.Schools.AddRange(listSchools);
            }

            if (!this._appDbContext.Classes.Any())
            {
                List<Class> listClasses = new List<Class>()
                {
                    new Class()
                    {
                        Id = new Guid(),
                        Name = "Class 1",
                        SchoolId = guidScholl1,
                        Students = new List<Student>()
                        {
                            new Student() {Id = new Guid(), Name = "Student 1"},
                            new Student() {Id = new Guid(), Name = "Student 2"},
                            new Student() {Id = new Guid(), Name = "Student 3"},
                            new Student() {Id = new Guid(), Name = "Student 4"},
                            new Student() {Id = new Guid(), Name = "Student 5"},
                            new Student() {Id = new Guid(), Name = "Student 6"},
                            new Student() {Id = new Guid(), Name = "Student 7"},
                            new Student() {Id = new Guid(), Name = "Student 8"},
                            new Student() {Id = new Guid(), Name = "Student 8"},
                            new Student() {Id = new Guid(), Name = "Student 10"},
                            new Student() {Id = new Guid(), Name = "Student 11"},
                            new Student() {Id = new Guid(), Name = "Student 12"},
                            new Student() {Id = new Guid(), Name = "Student 13"},
                            new Student() {Id = new Guid(), Name = "Student 14"},
                            new Student() {Id = new Guid(), Name = "Student 15"},
                            new Student() {Id = new Guid(), Name = "Student 16"},
                            new Student() {Id = new Guid(), Name = "Student 17"},
                            new Student() {Id = new Guid(), Name = "Student 18"},
                            new Student() {Id = new Guid(), Name = "Student 19"},
                            new Student() {Id = new Guid(), Name = "Student 20"},
                        },
                        
                    }
                };
                this._appDbContext.Classes.AddRange(listClasses);
            }

            await this._appDbContext.SaveChangesAsync();
        }
    }
}