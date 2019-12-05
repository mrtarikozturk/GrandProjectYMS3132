using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
   public class AppUserDetail:BaseEntity
    {
        [Required, DisplayName("İsim"), MaxLength(30), DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required, DisplayName("Soyisim"), MaxLength(30), DataType(DataType.Text)]
        public string LastName { get; set; }

        [DisplayName("Adres"), MaxLength(200), DataType(DataType.MultilineText)]
        public string Address { get; set; }

        // Relational Properties

        public virtual AppUser AppUser { get; set; }
    }
}
