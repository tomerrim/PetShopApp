using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public int AnimalId { get; set; }
        public Animal? Animals { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(300)]
        [Display(Name = "Comment")]
        public string CommentText { get; set; }
    }
}
