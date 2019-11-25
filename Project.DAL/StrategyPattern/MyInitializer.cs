using Bogus.DataSets;
using Project.DAL.Context;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.DAL.StrategyPattern
{
    public class MyInitializer : CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {

            #region 
            //for (int i = 0; i < 10; i++)
            //{
            //    AppUser ap = new AppUser();
            //    ap.UserName = new Internet("tr").UserName();
            //    ap.Password = new Internet("tr").Password();
            //    ap.Email = new Internet("tr").Email();
            //    context.AppUsers.Add(ap);

            //    AppUserDetail apd = new AppUserDetail();
            //    apd.ID = ap.ID;
            //    apd.FirstName = new Name("tr").FirstName();
            //    apd.LastName = new Name("tr").LastName();
            //    apd.Address = new Address("tr").Locale;
            //    context.AppUserDetails.Add(apd);
            //    context.SaveChanges();


            //} 
            #endregion


            for (int i = 0; i < 10; i++)
            {
                AppUser ap = new AppUser();
                ap.UserName = new Internet("tr").UserName();
                ap.Password = new Internet("tr").Password();
                ap.Email = new Internet("tr").Email();

                context.AppUsers.Add(ap);

                context.SaveChanges();


            }



            for (int i = 1; i < 11; i++)
            {
                AppUserDetail apd = new AppUserDetail();

                apd.ID = i; //Birebir ilişkisi oldugundan dolayı id'leri bu sekilde verdik..

                apd.FirstName = new Name("tr").FirstName();

                apd.LastName = new Name("tr").LastName();

                apd.Address = new Address("tr").Locale;

                context.AppUserDetails.Add(apd);

                context.SaveChanges();

            }



            #region VeriGetirme1
            //Random rnd = new Random();
            //List<Category> cat = new List<Category>();
            //for (int i = 0; i < 5; i++)
            //{
            //    Category c = new Category();

            //    c.CategoryName = new Commerce("tr").Categories(1)[0];
            //    c.Description = new Lorem("tr").Sentence(100);
            //    context.Categories.Add(c);
            //    context.SaveChanges();
            //    cat.Add(c);


            //    for (int x = 0; x < 10; x++)
            //    {
            //        Product p = new Product();

            //        p.ProductName = new Commerce("tr").ProductName();
            //        p.UnitPrice = Convert.ToDecimal(new Commerce("tr").Price());
            //        p.UnitsInStock = rnd.Next(5, 500);
            //        p.ImagePath = new Images().Nightlife();
            //        p.Categories = cat;

            //        context.Products.Add(p);

            //        context.SaveChanges();



            //    }
            //    //cat = null; tek kategori gelsi n diye.

            //} 
            #endregion

            Random rnd = new Random();

            for (int i = 0; i < 5; i++)
            {
                Category c = new Category();

                c.CategoryName = new Commerce("tr").Categories(1)[0];
                c.Description = new Lorem("tr").Sentence(100);
                context.Categories.Add(c);
                context.SaveChanges();
                for (int j = 0; j < 20; j++)
                {
                    Product p = new Product();

                    p.ProductName = new Commerce("tr").ProductName();
                    p.UnitPrice = Convert.ToDecimal(new Commerce("tr").Price());
                    p.UnitsInStock = rnd.Next(5, 500);
                    p.ImagePath = new Images().Nightlife();


                    context.Products.Add(p);
                    context.SaveChanges();


                    ProductCategory pc = new ProductCategory();
                    pc.ProductID = p.ID;
                    pc.CategoryID = c.ID;
                    context.ProductCategories.Add(pc);
                    context.SaveChanges();
                    if (i == 4)
                    {
                        ProductCategory pc2 = new ProductCategory();
                        pc2.ProductID = p.ID;
                        pc2.CategoryID = c.ID - 1;
                        context.ProductCategories.Add(pc2);
                    }
                    context.SaveChanges();

                }

            }       //Category eklendi.

            //Product Eklendi.


        }


    }
}
