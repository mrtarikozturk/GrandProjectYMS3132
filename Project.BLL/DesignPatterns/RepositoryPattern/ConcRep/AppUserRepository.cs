using Project.BLL.DesignPatterns.RepositoryPattern.BaseRep;
using Project.COMMON.CommonTools;
using Project.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.RepositoryPattern.ConcRep
{
    public class AppUserRepository : BaseRepository<AppUser>
    {
        /// <summary>
        /// Argüman olarak verilen kullanıcı adı ve e-mail adresinin veritabanında olup olmadığını sorgular.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="email"></param>
        /// <param name="varMi"></param>
        /// <returns></returns>
        public string CheckCredentials(string userName, string email, out bool varMi)
        {
            if (db.AppUsers.Any(x => x.UserName == userName && x.Email == email))
            {
                varMi = true;
                return "Boyle bir kullanici var";
            }
            varMi = false;
            return "Kullanici Eklendi";
        }

        public override void Add(AppUser item)
        {
            item.Password = DantexCrypt.Crypt(item.Password);
            base.Add(item);
        }
    }
}
