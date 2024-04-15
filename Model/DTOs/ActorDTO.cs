using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTOs
{
    public class ActorDTO
    {
        public Guid ActorId { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên của Diễn Viên")]
        [StringLength(30, ErrorMessage = "Tên của diễn viên phải ngắn hơn 30 kí tự.")]
        public string ActorName { get; set; }
    }
}
