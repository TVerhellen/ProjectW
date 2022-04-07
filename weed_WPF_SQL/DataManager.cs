using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace weed_WPF_SQL
{
    public class DataManager
    {
        //READ existing Libraries
        public static List<Login> GetLogins()
        {
            //Gather All Entitites Related To Type From DB
            using (var weedDBEntities = new WeedDBEntities())
            {
                var query = from Login in weedDBEntities.Login
                            where Login.LoginID >= 0
                            orderby Login.LoginID descending
                            select Login;

                return query.ToList();
            }
        }

        public static List<Character> GetCharacters(int loginID)
        {
            //Gather All Entitites Related To Type From DB
            using (var weedDBEntities = new WeedDBEntities())
            {
                var query = from Character in weedDBEntities.Characters
                            where Character.LoginID == loginID
                            orderby Character.Name
                            select Character;

                return query.ToList();
            }
        }

        //CREATE new Entitites
        public static int InsertLogin(Login l)
        {
            return 0;
        }
        public static int InsertCharacter(Character c)
        {
            return 0;
        }

        //UPDATE existing Entities
        public static int UpdateLogin(Login lUpdate)
        {
            return 0;
        }
        public static int UpdateCharacter(Character cUpdate)
        {
            return 0;
        }

        //DELETE existing Entities & Relationships
        public static int DeleteLogin(Login lDelete)
        {
            return 0;
        }
        public static int DeleteCharacter(Character cDelete)
        {
            return 0;
        }
    }
}
