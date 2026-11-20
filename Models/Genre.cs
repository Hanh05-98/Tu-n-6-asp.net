using System.ComponentModel.DataAnnotations;

namespace week6.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên thể loại")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Tên thể loại phải từ 3 đến 100 ký tự")]
        public int Name { get; set; }

       public ICollection<Movie>? Movies { get; set; }
    }
}