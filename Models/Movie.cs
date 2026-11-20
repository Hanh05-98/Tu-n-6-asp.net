using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace week6.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên phim")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "Tên phim từ 3 đến 200 ký tự")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Vui lòng chọn ngày phát hành")]
        [DataType(DataType.Date)]
        public DateTime? ReleaseDate { get; set; }
        
        [Range(1, 600,
            ErrorMessage = "Thời lượng phải từ 1 đến 600 phút.")]
        public int Duration { get; set; }
        
        [Display(Name = "Thể loại")]
        public int GenreId { get; set; }

        [Range(1, int.MaxValue,
            ErrorMessage = "Vui lòng chọn thể loại")]
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }        
    }
}