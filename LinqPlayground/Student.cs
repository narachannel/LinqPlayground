using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LinqPlayground
{
    public class Student
    {
        [Key]
        public  int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Grade { get; set; }
        public int SchoolId { get; set; }
    }
}
