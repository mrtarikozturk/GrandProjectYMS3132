using Project.MODEL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
   public class AppUser:BaseEntity
    {
        public AppUser()
        {
            Role = UserRole.Member;
            ActivationCode = Guid.NewGuid();
        }

        [Required, DisplayName("Kullanıcı Adı"), MaxLength(30), DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required, DisplayName("Şifre"), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DisplayName("E-Posta"), MaxLength(60), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public Guid? ActivationCode { get; set; }

        [Required, DisplayName("Rol")]
        public UserRole Role { get; set; }

        // Relational properties

        public virtual AppUserDetail Profile { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
