using Model.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class MovieDTO
    {
        public Guid MovieId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tiêu đề.")]
        [StringLength(80, ErrorMessage = "Tiêu đề phải ngắn hơn 80 kí tự.")]
        public string Title { get; set; }
        [MaxLength(255)]
        [Required(ErrorMessage = "Vui lòng nhập mô tả.")]
        public string Description { get; set; }
        [MaxLength(80)]
        [Required(ErrorMessage = "Vui lòng nhập quốc gia.")]
        public string Language { get; set; }
        [Range(1, 2030)]
        [Required(ErrorMessage = "Vui lòng nhập năm phát hành.")]
        public int ReleaseYear { get; set; }
        [MaxLength(100)]
        [Required(ErrorMessage = "Vui lòng nhập đạo diễn.")]
        public string Director { get; set; }

        public string ImageURL { get; set; }
        public string PosterURL { get; set; } = "sdasdasdsd";
        [Required(ErrorMessage = "Vui lòng nhập URL traller")]
        public string TrailerURL { get; set; }
        [MaxLength(10)]
        [Required(ErrorMessage = "Vui lòng nhập chất lượng")]
        public string quality { get; set; }
        [MaxLength(5)]
        [Required(ErrorMessage = "Vui lòng nhập MPPA")]
        public string MPAA { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
        public int MovieDuration { get; set; }
        [Required]
        public Boolean Released { get; set; } = false;
        [Required]
        public Boolean IsSeriesMovie { get; set; } = false;
        [Required(ErrorMessage = "Vui lòng chọn thể loại.")]
        public Guid[] GenreIds { get; set; } = null!;
        [Required(ErrorMessage = "Vui lòng chọn diễn viên.")]
        public Guid[] ActorIds { get; set; } = null!;
        

    }
}
