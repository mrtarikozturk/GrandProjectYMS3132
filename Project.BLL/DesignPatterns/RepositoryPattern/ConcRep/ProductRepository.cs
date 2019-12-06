using Project.BLL.DesignPatterns.RepositoryPattern.BaseRep;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.RepositoryPattern.ConcRep
{
    public class ProductRepository : BaseRepository<Product>
    {
        public List<Product> KategoriyeGoreUrunGetir(string item)
        {
            
           return db.Products.Where(x => x.Categories.FirstOrDefault().Category.CategoryName== item).ToList();

        }
    }
}
