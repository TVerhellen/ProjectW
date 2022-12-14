using System.Collections.Generic;
using System.Linq;

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
        public static Character GetCharacter(int loginID)
        {
            //Gather All Entitites Related To Type From DB
            using (var weedDBEntities = new WeedDBEntities())
            {
                var query = from Character in weedDBEntities.Characters
                            where Character.LoginID == loginID
                            orderby Character.Name
                            select Character;

                var x = query.FirstOrDefault();
                return x;
            }
        }
        public static List<Character> GetCharacters()
        {
            //Gather All Entitites Related To Type From DB
            using (var weedDBEntities = new WeedDBEntities())
            {
                var query = from Character in weedDBEntities.Characters
                            orderby Character.Money
                            select Character;

                List<Character> list = query.ToList();
                return list.OrderByDescending(x => x.Money).ToList();
            }
        }
        public static List<Cultivator> GetCultivators(Character myCharacter)
        {
            using (var my_WeedDB = new WeedDBEntities())
            {
                var query = from cultivators in my_WeedDB.Cultivators
                            where cultivators.FarmID == myCharacter.FarmID
                            select cultivators;

                return query.ToList();
            }
        }
        public static List<CultType> GetCultTypes()
        {
            using (var my_WeedDB = new WeedDBEntities())
            {
                return my_WeedDB.CultTypes.ToList();
            }
        }
        public static List<Lamp> GetLamps()
        {
            using (var my_WeedDB = new WeedDBEntities())
            {
                return my_WeedDB.Lamps.ToList();
            }
        }
        public static List<Soil> GetSoils()
        {
            using (var my_WeedDB = new WeedDBEntities())
            {
                return my_WeedDB.Soils.ToList();
            }
        }
        public static List<Fertilizer> GetFertilizers()
        {
            using (var my_WeedDB = new WeedDBEntities())
            {
                return my_WeedDB.Fertilizers.ToList();
            }
        }
        public static List<WaterSystem> GetWatersystems()
        {
            using (var my_WeedDB = new WeedDBEntities())
            {
                return my_WeedDB.WaterSystem.ToList();
            }
        }
        public static List<Name> GetStrainNames()
        {
            using (var my_WeedDB = new WeedDBEntities())
            {
                return my_WeedDB.Names.ToList();
            }
        }

        public static string GetStrainNameofCultivator(Cultivator cultivatorObj)
        {
            using (var my_WeedDB = new WeedDBEntities())
            {
                var query = from name in my_WeedDB.Names
                            where name.NameID == cultivatorObj.NameID
                            select name;
                var x = query.FirstOrDefault();
                return x.StrainName;
            }
        }

        public static Cultivator GetCultivatorByObj(Cultivator cultivatorObj)
        {
            using (var my_WeedDB = new WeedDBEntities())
            {
                var query = from cultivator in my_WeedDB.Cultivators
                            where cultivator.CultivatorID == cultivatorObj.CultivatorID
                            select cultivator;
                var x = query.FirstOrDefault();
                return x;
            }
        }
        public static Cultivator GetCultivatorByCharacter(Character characterobj)
        {
            using (var my_WeedDB = new WeedDBEntities())
            {
                var query = from cultivator in my_WeedDB.Cultivators
                            where cultivator.FarmID == characterobj.CharacterID
                            select cultivator;
                var x = query.FirstOrDefault();
                return x;
            }
        }

        public static List<Cultivator> GetCultivatorsByFarmID(Farm farm)
        {
            using (var my_WeedDB = new WeedDBEntities())
            {
                var query = from cultivator in my_WeedDB.Cultivators
                            where cultivator.FarmID == farm.FarmID
                            select cultivator;
                var x = query.ToList();
                return x;
            }
        }
        public static Cultivator GetCultivatorByFarmID(Farm farm)
        {
            using (var my_WeedDB = new WeedDBEntities())
            {
                var query = from cultivator in my_WeedDB.Cultivators
                            where cultivator.FarmID == farm.FarmID
                            select cultivator;
                var x = query.FirstOrDefault();
                return x;
            }
        }
        public static Farm GetFarmByCharacterID(Character character)
        {
            using (var my_WeedDB = new WeedDBEntities())
            {
                var query = from farm in my_WeedDB.Farms
                            where farm.CharacterID == character.CharacterID
                            select farm;
                var x = query.FirstOrDefault();
                return x;
            }
        }

        //CREATE new Entitites
        public static int InsertLogin(Login l)
        {
            int check = 0;

            using (var weedDBEntities = new WeedDBEntities())
            {
                weedDBEntities.Login.Add(l);
                if(0 < weedDBEntities.SaveChanges())
                {
                    check = weedDBEntities.SaveChanges();
                }
            }

            return check;
        }
        public static int InsertCharacter(Character c)
        {
            int check = 0;

            using(var weedDBEntities = new WeedDBEntities())
            {
                weedDBEntities.Characters.Add(c);
                if(0 < weedDBEntities.SaveChanges())
                {
                    check = c.CharacterID;
                }
            }

            return check;
        }

        public static int InsertFarm(Farm farmObj)
        {
            int check = 0;

            using (var weedDBEntities = new WeedDBEntities())
            {
                weedDBEntities.Farms.Add(farmObj);
                if (0 < weedDBEntities.SaveChanges())
                {
                    check = farmObj.FarmID;
                }
            }

            return check;
        }
        public static int InsertCultivator(Cultivator cultivatorObj)
        {
            int check = 0;
            using (var my_WeedDB = new WeedDBEntities())
            {
                my_WeedDB.Cultivators.Add(cultivatorObj);
                if (0 < my_WeedDB.SaveChanges())
                {
                    check = cultivatorObj.CultivatorID;
                }
            }
            return check;
        }

        //UPDATE existing Entities
        public static int UpdateLogin(Login lUpdate)
        {
            int check = 0;

            using(var weedDBEntities = new WeedDBEntities())
            {
                var query = from login in weedDBEntities.Login
                            where login.LoginID == lUpdate.LoginID
                            select login;
                var x = query.FirstOrDefault();
                if(x != null)
                {
                    x.Username = lUpdate.Username;
                    x.Password = lUpdate.Password;
                    x.CharacterID = lUpdate.CharacterID;
                    check = weedDBEntities.SaveChanges();
                }

                return check;
            }
        }
        public static int UpdateCharacter(Character cUpdate)
        {
            int check = 0;

            using (var weedDBEntities = new WeedDBEntities())
            {
                var query = from character in weedDBEntities.Characters
                            where character.CharacterID == cUpdate.CharacterID
                            select character;
                var x = query.FirstOrDefault();
                if (x != null)
                {
                    x.LoginID = cUpdate.LoginID;
                    x.FarmID = cUpdate.FarmID;
                    x.Name = cUpdate.Name;
                    x.Money = cUpdate.Money;
                    x.Weed = cUpdate.Weed;
                    x.Reputation = cUpdate.Reputation;

                    x.HasBike = cUpdate.HasBike;
                    x.HasStressCapUp = cUpdate.HasStressCapUp;
                    x.HasStressRecovUp = cUpdate.HasStressRecovUp;

                    x.TotalCycles = cUpdate.TotalCycles;
                    x.LastTimeCaught = cUpdate.LastTimeCaught;
                    x.LongestStreak = cUpdate.LongestStreak;
                    check = weedDBEntities.SaveChanges();
                }

                return check;
            }
        }

        public static int UpdateFarm(Farm farmObj)
        {
            int check = 0;
            using(var my_weedDB = new WeedDBEntities())
            {
                var query = from farm in my_weedDB.Farms
                            where farm.FarmID == farm.FarmID
                            select farm;
                var x = query.FirstOrDefault();
                if (x != null)
                {
                    x.LightingID = farmObj.LightingID;
                    x.HeatingID = farmObj.HeatingID;
                    x.HumidityID = farmObj.HumidityID;
                    check = my_weedDB.SaveChanges();
                }
                return check;
            }
        }

        public static int UpdateCultivator(Cultivator cultivatorObj)
        {
            int check = 0;
            using (var my_WeedDB = new WeedDBEntities())
            {
                var query = from cultivator in my_WeedDB.Cultivators
                            where cultivator.CultivatorID == cultivatorObj.CultivatorID
                            select cultivator;
                var x = query.FirstOrDefault();
                if (x != null)
                {
                    x.CyclesPassed = cultivatorObj.CyclesPassed;
                    x.CultID = cultivatorObj.CultID;
                    x.FertilizerID = cultivatorObj.FertilizerID;
                    x.LampID = cultivatorObj.LampID;
                    x.SoilID = cultivatorObj.SoilID;
                    x.WaterID = cultivatorObj.WaterID;
                    x.NameID = cultivatorObj.NameID;
                    x.WaterSupply = cultivatorObj.WaterSupply;
                    x.FertilizerSupply = cultivatorObj.FertilizerSupply;
                    x.RendementValue = cultivatorObj.RendementValue;
                    check = my_WeedDB.SaveChanges();
                }
                return check;
            }
        }

        //DELETE existing Entities & Relationships
        public static int DeleteLogin(Login lDelete)
        {
            int check = 0;

            using(var weedDBEntities = new WeedDBEntities())
            {
                var query = from login in weedDBEntities.Login
                            where login.LoginID == lDelete.LoginID
                            select login;
                var x = query.FirstOrDefault();
                weedDBEntities.Login.Remove(x);
                check = weedDBEntities.SaveChanges();
            }

            return check;
        }
        public static int DeleteCharacter(Character cDelete)
        {
            int check = 0;

            using(var weedDBEntities = new WeedDBEntities())
            {
                var query = from character in weedDBEntities.Characters
                            where character.CharacterID == cDelete.CharacterID
                            select character;
                var x = query.FirstOrDefault();
                weedDBEntities.Characters.Remove(x);
                check = weedDBEntities.SaveChanges();
            }

            return check;
        }
        public static int DeleteCultivator(Cultivator cultivatorObj)
        {
            int check = 0;
            using (var my_WeedDB = new WeedDBEntities())
            {
                var query = from cultivator in my_WeedDB.Cultivators
                            where cultivator.CultivatorID == cultivatorObj.CultivatorID
                            select cultivator;
                var x = query.FirstOrDefault();
                my_WeedDB.Cultivators.Remove(x);
                check = my_WeedDB.SaveChanges();
            }
            return check;
        }
    }
}
