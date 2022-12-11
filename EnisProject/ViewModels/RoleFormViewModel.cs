using System.ComponentModel.DataAnnotations;

namespace EnisProject.ViewModels
{
    public class RoleFormViewModel
    {
        [Required, StringLength(256)]
        public string RoleName { get; set; }

    }
}
