using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class GenreDTO
    {
        public Guid GenreId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên của Thể loại.")]
        public string GenreName { get; set; }
    }
}
