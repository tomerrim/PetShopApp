using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetShop.Models;

namespace PetShop.DataAccess
{
    public class PetDbContext : DbContext
    {
        public PetDbContext(DbContextOptions<PetDbContext> options) :base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public DbSet<Comment> Comments { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Animal>().HasData(
        //        new Animal { AnimalId = 1, Name = "Eagle", Age = 7, PictureName = @"\images\animals\Eagle.jpg", Description = "The king of the birds", CategoryId = 4 },
        //        new Animal { AnimalId = 2, Name = "Macaw", Age = 3, PictureName = @"\images\animals\Macaw.jpg", Description = "Macaws are a group of New World parrots that are long-tailed and often colorful.", CategoryId = 4 },
        //        new Animal { AnimalId = 3, Name = "Rabbit", Age = 5, PictureName = @"\images\animals\Rabbit.webp", Description = "Rabbits, also known as bunnies or bunny rabbits, are small mammals in the family Leporidae (which also contains the hares) of the order Lagomorpha (which also contains the pikas).", CategoryId = 1 },
        //        new Animal { AnimalId = 4, Name = "Dog", Age = 3, PictureName = @"\images\animals\Dog.jpg", Description = "The best friend of human", CategoryId = 1 },
        //        new Animal { AnimalId = 5, Name = "Chameleon", Age = 5, PictureName = @"\images\animals\Chameleon.webp", Description = " chameleons are a distinctive and highly specialized clade of Old World lizards with 202 species described as of June 2015. The members of this family  are best known for their distinct range of colors, being capable of shifting to different hues and degrees of brightness", CategoryId = 2 },
        //        new Animal { AnimalId = 6, Name = "Snake", Age = 2, PictureName = @"\images\animals\Snake.webp", Description = "Snakes are elongated, limbless, carnivorous reptiles of the suborder Serpentes . Like all other squamates, snakes are ectothermic, amniote vertebrates covered in overlapping scales. ", CategoryId = 2 },
        //        new Animal { AnimalId = 7, Name = "Clownfish", Age = 2, PictureName = @"\images\animals\Clownfish.jpg", Description = "Clownfish or anemonefish are fishes from the subfamily Amphiprioninae in the family Pomacentridae.", CategoryId = 3 },
        //        new Animal { AnimalId = 8, Name = "Guppy", Age = 2, PictureName = @"\images\animals\Guppy.jpg", Description = "The guppy (Poecilia reticulata), also known as millionfish and rainbow fish, is one of the world's most widely distributed tropical fish and one of the most popular freshwater aquarium fish species.", CategoryId = 3 },
        //        new Animal { AnimalId = 9, Name = "Cat", Age = 6, PictureName = @"\images\animals\Cat.jpg", Description = "The cat (Felis catus) is a domestic species of small carnivorous mammal. It is the only domesticated species in the family Felidae and is commonly referred to as the domestic cat or house cat to distinguish it from the wild members of the family.", CategoryId = 1 },
        //        new Animal { AnimalId = 10, Name = "Shark", Age = 1, PictureName = @"\images\animals\Shark.jpg", Description = "king of the sea", CategoryId = 3 },
        //        new Animal { AnimalId = 11, Name = "Fox", Age = 2, PictureName = @"\images\animals\fox.png", Description = "Foxes are small to medium-sized, omnivorous mammals belonging to several genera of the family Canidae. They have a flattened skull, upright, triangular ears, a pointed, slightly upturned snout, and a long bushy tail (or brush).", CategoryId = 1 },
        //        new Animal { AnimalId = 12, Name = "Hamster", Age = 3, PictureName = @"\images\animals\Hamster.jpg", Description = "Hamsters are rodents (order Rodentia) belonging to the subfamily Cricetinae, which contains 19 species classified in seven genera.", CategoryId = 1 }

        //        );

        //    modelBuilder.Entity<Category>().HasData(
        //        new Category { CategoryId = 1, Name = "Mammal" },
        //        new Category { CategoryId = 2, Name = "Reptile" },
        //        new Category { CategoryId = 3, Name = "Aquatic" },
        //        new Category { CategoryId = 4, Name = "Bird" }
        //        );

        //    modelBuilder.Entity<Comment>().HasData(
        //        new Comment { CommentId = 1, AnimalId = 1, CommentText = "A great bird" },
        //        new Comment { CommentId = 2, AnimalId = 1, CommentText = "Nesher" },
        //        new Comment { CommentId = 3, AnimalId = 1, CommentText = "Eagle" },
        //        new Comment { CommentId = 4, AnimalId = 1, CommentText = "king of birds" },
        //        new Comment { CommentId = 5, AnimalId = 1, CommentText = "hhhhhh" },
        //        new Comment { CommentId = 6, AnimalId = 1, CommentText = "bla bla bla" },
        //        new Comment { CommentId = 7, AnimalId = 1, CommentText = "sssssss" },
        //        new Comment { CommentId = 8, AnimalId = 1, CommentText = "eagle" },

        //        new Comment { CommentId = 9, AnimalId = 2, CommentText = "A colorful bird" },
        //        new Comment { CommentId = 10, AnimalId = 2, CommentText = "macaw" },
        //        new Comment { CommentId = 11, AnimalId = 2, CommentText = "tuki" },
        //        new Comment { CommentId = 12, AnimalId = 2, CommentText = "Macaw" },
        //        new Comment { CommentId = 13, AnimalId = 2, CommentText = "hhhhhhhh" },
        //        new Comment { CommentId = 14, AnimalId = 2, CommentText = "sssssss" },
        //        new Comment { CommentId = 15, AnimalId = 2, CommentText = "bla bla" },

        //        new Comment { CommentId = 16, AnimalId = 3, CommentText = "Rabbit" },
        //        new Comment { CommentId = 17, AnimalId = 3, CommentText = "arnav" },

        //        new Comment { CommentId = 18, AnimalId = 4, CommentText = "kelev" },
        //        new Comment { CommentId = 19, AnimalId = 4, CommentText = "woof woof" },

        //        new Comment { CommentId = 20, AnimalId = 5, CommentText = "zikit" },
        //        new Comment { CommentId = 21, AnimalId = 5, CommentText = "chameleon" },

        //        new Comment { CommentId = 22, AnimalId = 6, CommentText = "snake" },
        //        new Comment { CommentId = 23, AnimalId = 6, CommentText = "ssssssss" },

        //        new Comment { CommentId = 24, AnimalId = 7, CommentText = "nemo" },
        //        new Comment { CommentId = 25, AnimalId = 7, CommentText = "blop blop" },

        //        new Comment { CommentId = 26, AnimalId = 8, CommentText = "guppy" },
        //        new Comment { CommentId = 27, AnimalId = 8, CommentText = "blop!!" },

        //        new Comment { CommentId = 28, AnimalId = 9, CommentText = "cat" },
        //        new Comment { CommentId = 29, AnimalId = 9, CommentText = "hatool" },
        //        new Comment { CommentId = 30, AnimalId = 9, CommentText = "meowwww" },

        //        new Comment { CommentId = 31, AnimalId = 10, CommentText = "karish" },
        //        new Comment { CommentId = 32, AnimalId = 10, CommentText = "white shark" },

        //        new Comment { CommentId = 33, AnimalId = 11, CommentText = "fox" },
        //        new Comment { CommentId = 34, AnimalId = 11, CommentText = "shual" },

        //        new Comment { CommentId = 35, AnimalId = 12, CommentText = "hamster" },
        //        new Comment { CommentId = 36, AnimalId = 12, CommentText = "oger" }
        //        );
        //}
    }
}
