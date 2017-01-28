using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LinqPlayground
{
    public class School
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
