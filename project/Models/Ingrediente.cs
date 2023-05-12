using System.ComponentModel.DataAnnotations;

namespace project.Models
{
    public class Ingrediente
    {
        [Key] 
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public Ingrediente(string nome) { Nome = nome; }

        public List<Pizza>? Pizze { get; set; }
    }
}
