using Microsoft.EntityFrameworkCore.Query.Internal;
using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class DetailViewDTO
    {
         public Movie movie { get; set; }
        public IEnumerable<Movie> movies { get; set; }
        public IEnumerable<DisplayRating> ratings { get; set; }

    }
}
