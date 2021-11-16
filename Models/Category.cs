using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rocy.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display order")]
        [Range(1,int.MaxValue,ErrorMessage ="It must be Number graterthan 0")]
        public int Displayorder { get; set; }


    }
}