using Model.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class MovieDetailDTO
    {
        public Guid MovieDetailId { get; set; }
        [Required]
        public string MovieDetailName { get; set; }

        [Required]
        public string UrlMovie { get; set; }
        public Guid MovieId { get; set; }
    }
}
