using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class Order : BaseEntity
    {
        [Required, DisplayName("TC Kimlik Numarası"), MaxLength(11), MinLength(11), DataType(DataType.Text)]
        public string TC { get; set; }

        [Required, DisplayName("Adres"), MaxLength(200), DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required, DisplayName("İl"), DataType(DataType.Text)]
        public string City { get; set; }

        [Required, DisplayName("İlçe"), DataType(DataType.Text)]
        public string Town { get; set; }

        [Required, DisplayName("Mahalle"), DataType(DataType.Text)]
        public string District { get; set; }

        [Required, DisplayName("Telefon"), DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public int AppUserID { get; set; }

        // Relational Properties

        public virtual AppUser AppUser { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
