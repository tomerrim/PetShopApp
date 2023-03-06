using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetShop.Models
{
    public class Animal
    {
        [Key]
        public int AnimalId { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public int Age { get; set; }

        [ValidateNever]
        [Display(Name = "Image")]
        public string PictureName { get; set; }

        [Required]
        public string Description { get; set;}

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        [Required]
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();


    }
}
