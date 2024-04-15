using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    public class MovieDetail
    {
        [Key]
        public Guid MovieDetailId { get; set; }
        public string MovieDetailName { get; set; } 

        [Required]
        public string UrlMovie { get; set; }
        public Movie? Movie { get; set; }
        public Guid MovieId { get; set; }
        
    }
}
