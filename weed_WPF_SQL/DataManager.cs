using System.Collections.Generic;
using System.Linq;

namespace weed_WPF_SQL
{
    class DataManager
    {
        public static List<Cultivator> GetCultivators()
        {
            using (var my_WeedDB = new WeedDBEntities())
            {
                return my_WeedDB.Cultivators.ToList();
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
                    check = my_WeedDB.SaveChanges();
                }
                return check;

            }
        }
        public static int InsertCultivator(Cultivator cultivatorObj)
        {
            int check = 0;
            using (var my_WeedDB = new WeedDBEntities())
            {
                my_WeedDB.Cultivators.Add(cultivatorObj);
                if (0 < my_WeedDB.SaveChanges())
                {
                    check = my_WeedDB.SaveChanges();
                }
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
