using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entitys
{
    public class Rating
    {
        [Key]
        public Guid RatingId { get; set; }
        [Range(1, 5)]
        public int Stars { get; set; }
        [MaxLength(255)]
        public string comment { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid MovieId { get; set; }
        public Movie? Movie { get; set; }
        [BindNever]
        public string UserId { get; set; }
        public User? User { get; set; }

    }
}
