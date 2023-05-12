using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Category
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public List<Pizza> Pizze { get; set; }

        public Category(string nome)
        {
            Nome = nome;
        }
    }
}
