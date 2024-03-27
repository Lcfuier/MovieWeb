using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Model.Entitys
{
    [Table("Movie")]
    public class Movie
    {
        [Key]
        public Guid MovieId { get; set; }
        [MaxLength(255)]
        public string Title { get; set; }
        [MaxLength(255)]
        public string Description { get; set; }
        [MaxLength(80)]
        public string Language { get; set; }
        [Range(1, 2024)]
        public int ReleaseYear { get; set; }
        [MaxLength(100)]
        public string Director { get; set; }
        [Required]
        public string UrlMovie { get; set; }
        public string ImageURL { get; set; }
        [MaxLength(10)]
        public string quality { get; set; }
        [MaxLength(5)]
        public string MPAA { get; set; }
        public DateTime CreateDate { get; set; }
        public int MovieDuration { get; set; }
        public int AverageRating { get; set; }

        [ForeignKey("MovieId")]
        [InverseProperty("Movies")]
        public virtual ICollection<Actor> Actors { get; set; } = new List<Actor>();

        [ForeignKey("MovieId")]
        [InverseProperty("Movies")]
        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public ICollection<Rating>? Ratings{ get; set; }
    }
}
