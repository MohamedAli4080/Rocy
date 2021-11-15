using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rocy.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        [DisplayName("Display order")]
        public int Displayorder { get; set; }


    }
}