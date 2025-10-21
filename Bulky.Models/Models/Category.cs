using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30)]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Order Must betwwen 1-100 Character")]
        public int DisplayOrder { get; set; }
        //Server Side Validation
        //   Range(1,100)
        //    minimum 1 and Maximum 100
        //    MaxLength(100)
        //Custom Error ....[ErrorMessage ="Order Must betwwen 1-100 Character"]

    }
}
