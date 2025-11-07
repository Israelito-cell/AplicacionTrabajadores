using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aplicacion_Web_Para_Trabajadores.Models
{
    public class Trabajador
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Nombres")]
        [Required, MaxLength(100)]
        public string? Nombres { get; set; }
        [Display(Name = "Apellidos")]
        [Required, MaxLength(100)]
        public string? Apellidos { get; set; }

        [Display(Name = "Tipo de documento")]
        [Required, MaxLength(50)]
        public string? TipoDocumento { get; set; }

        [Display(Name = "Número de documento")]
        [Required, MaxLength(50)]
        public string? NumeroDocumento { get; set; }

        [Display(Name = "Sexo")]
        [Required]
        [RegularExpression("M|F")]
        public string? Sexo { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        public byte[]? Foto { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(250)]
        public string? Direccion { get; set; }

        [NotMapped]
        [Display(Name = "Fecha de creación")]
          public DateTime? FechaCreacion { get; set; } = DateTime.UtcNow;


    }
}
