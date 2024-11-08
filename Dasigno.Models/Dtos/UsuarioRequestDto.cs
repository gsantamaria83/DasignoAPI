using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dasigno.Models.Dtos
{
    public class UsuarioRequestDto
    {
        public string IdUsuario { get; set; }

        [Required(ErrorMessage = "El primer nombre es obligatorio")]
        [MaxLength(50, ErrorMessage = "El primer nombre no puede tener más de 50 caracteres")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "El primer nombre no puede contener números")]
        public string PrimerNombre { get; set; }

        [MaxLength(50, ErrorMessage = "El segundo nombre no puede tener más de 50 caracteres")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "El segundo nombre no puede contener números")]
        public string SegundoNombre { get; set; }

        [Required(ErrorMessage = "El primer apellido es obligatorio")]
        [MaxLength(50, ErrorMessage = "El primer apellido no puede tener más de 50 caracteres")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "El primer apellido no puede contener números")]
        public string PrimerApellido { get; set; }

        [MaxLength(50, ErrorMessage = "El segundo apellido no puede tener más de 50 caracteres")]
        [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "El segundo apellido no puede contener números")]
        public string SegundoApellido { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El sueldo es obligatorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El sueldo debe ser mayor que 0")]
        public decimal Sueldo { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [DataType(DataType.DateTime)]
        public DateTime FechaModificacion { get; set; } = DateTime.Now;

        // Método para actualizar la fecha de modificación
        public void ActualizarFechaModificacion()
        {
            FechaModificacion = DateTime.Now;
        }

        public List<ValidationResult> Validar()
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(this, null, null);
            Validator.TryValidateObject(this, validationContext, validationResults, true);

            return validationResults;
        }
    }
}
