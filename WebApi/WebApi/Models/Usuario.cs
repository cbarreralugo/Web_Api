using System.ComponentModel.DataAnnotations;

namespace MiApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string Nombre { get; set; }

        [Required, Range(0, 150)]
        public int Edad { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
    }
}
