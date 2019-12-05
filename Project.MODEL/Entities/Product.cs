using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MODEL.Entities
{
    public class Product : BaseEntity
    {
        public Product()
        {
            Categories = new List<ProductCategory>();
        }

        [Required, DisplayName("Ürün Adı"), MaxLength(50), DataType(DataType.Text)]
        public string ProductName { get; set; }

        [DisplayName("Ürün Resmi")]
        public string ImagePath { get; set; }

        [Required, DisplayName("Stok Miktarı"), Range(0, ushort.MaxValue)]
        public int UnitsInStock { get; set; }

        [Required, DisplayName("Ürün Fiyatı"), Range(0, int.MaxValue), DataType(DataType.Currency)]
        public decimal UnitPrice { get; set; }

        // Relational Properties

        public virtual List<ProductCategory> Categories { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }

        public virtual List<ProductFeature> ProductFeatures { get; set; }
    }
}
