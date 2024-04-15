using Model.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class DisplayRating
    {
        public string fullName { get; set; }
        public int rating { get; set; }
        public string Comment { get; set; }
        public Guid? RatingId { get; set; }
    }
}
