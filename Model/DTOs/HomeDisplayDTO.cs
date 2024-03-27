using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class HomeDisplayDTO
    {
        public IEnumerable<Movie> movies { get; set; }
        public IEnumerable<Genre> genres { get; set; }
    }
}
