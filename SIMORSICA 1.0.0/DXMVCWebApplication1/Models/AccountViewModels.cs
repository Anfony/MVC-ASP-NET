using System.ComponentModel.DataAnnotations;

namespace DXMVCWebApplication1.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "ID Usuario")]
        public string UserName { get; set; }
    }

    public class ManageUserViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener por lo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña nueva")]
        [Compare("NewPassword", ErrorMessage = "La contraseña y la confirmación de la contraseña no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "ID usuario")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }


        [Display(Name = "Recordarme en este equipo?")]
        public bool RememberMe { get; set; }
    }

    public class RecuperaContrasenia
    {
        [Required(ErrorMessage ="Debe ingresar el usuario con que se dio de alta al sistema")]
        [Display(Name = "ID del usuario")]
        public string usuarioProporcionado { get; set; }

        [Required(ErrorMessage = "Debe de ingresar el correo electrónico con que está registrado en el sistema")]
        [RegularExpression("\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*", ErrorMessage = "El Correo es invalido")]
        public string emailProporcionado { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "ID Usuario")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener por lo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación de la contraseña no coinciden.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Rol")]
        public string dropDownRoles { get; set; }
        [Display(Name = "Estado")]
        public int dropDownEstado { get; set; }
        [Display(Name = "Instancia Ejecutora")]
        public int dropDownInstanciaEjecutora { get; set; }
        [Display(Name = "Unidad Responsable")]
        public int dropDownUnidadResponsable { get; set; }
        [Display(Name ="Campaña")]
        public int dropDownCampania { get; set; }
        [Display(Name = "Acción")]
        public int dropDownAccion { get; set; }
        [Display(Name = "Actividad")]
        public int Actividad { get; set; }
        [Display(Name = "Persona")]
        public int dropDownPersona { get; set; }

    }

    public class RegisterUpdateViewModel
    {
        [Required]
        [Display(Name = "ID Usuario")]
        public string ID { get; set; }

        [Required]
        [Display(Name = "user name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener por lo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña y la confirmación de la contraseña no coinciden.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Rol")]
        public string dropDownRoles { get; set; }

        [Display(Name = "Estado")]
        public int dropDownEstado { get; set; }

        [Display(Name = "Instancia Ejecutora")]
        public int dropDownInstanciaEjecutora { get; set; }

        [Display(Name = "Unidad Responsable")]
        public int dropDownUnidadResponsable { get; set; }
    }
}
