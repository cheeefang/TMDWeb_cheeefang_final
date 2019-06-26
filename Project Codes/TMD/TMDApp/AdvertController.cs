using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace TMDApp
{
    public class Advert
    {
        public int id;
        public string name;
        public AdvertType type;
        public string path;
        public int duration;
    }

    public class AdvertController
    {
        private Database db = new Database();
        private int _billboard;
        private string advertMode;
        private readonly string VIDEO = "Video";
        private readonly string IMAGE = "Image";

        public AdvertController(int billboard)
        {
            _billboard = billboard;
            advertMode = ConfigurationManager.AppSettings["ADVERT_MODE"];
        }

        public void UpdateFeedback(Watcher[] watchers, int advId)
        {
            if (watchers == null || watchers.Length == 0) return;

            int[] femaleChild = new int[5], femaleYoungAdult = new int[5], femaleAdult = new int[5], femaleSenior = new int[5];
            int[] maleChild = new int[5], maleYoungAdult = new int[5], maleAdult = new int[5], maleSenior = new int[5];

            foreach (Watcher w in watchers)
            {
                if (w.gender == Gender.Female)
                {
                    switch (w.ageGroup)
                    {
                        case AgeBracket.Child:
                            calMood(w.mood, ref femaleChild);
                            break;
                        case AgeBracket.YoungAdult:
                            calMood(w.mood, ref femaleYoungAdult);
                            break;
                        case AgeBracket.Adult:
                            calMood(w.mood, ref femaleAdult);
                            break;
                        case AgeBracket.Senior:
                            calMood(w.mood, ref femaleSenior);
                            break;
                    }
                }
                else
                {
                    switch (w.ageGroup)
                    {
                        case AgeBracket.Child:
                            calMood(w.mood, ref maleChild);
                            break;
                        case AgeBracket.YoungAdult:
                            calMood(w.mood, ref maleYoungAdult);
                            break;
                        case AgeBracket.Adult:
                            calMood(w.mood, ref maleAdult);
                            break;
                        case AgeBracket.Senior:
                            calMood(w.mood, ref maleSenior);
                            break;
                    }
                }
            }

            for (int mood = 0; mood < 5; mood++)
            {
                if (femaleChild[mood] > 0) insertFeedback(advId, femaleChild[mood], "F", "1", (mood + 1).ToString());
                if (femaleYoungAdult[mood] > 0) insertFeedback(advId, femaleYoungAdult[mood], "F", "2", (mood + 1).ToString());
                if (femaleAdult[mood] > 0) insertFeedback(advId, femaleAdult[mood], "F", "3", (mood + 1).ToString());
                if (femaleSenior[mood] > 0) insertFeedback(advId, femaleSenior[mood], "F", "4", (mood + 1).ToString());

                if (maleChild[mood] > 0) insertFeedback(advId, maleChild[mood], "M", "1", (mood + 1).ToString());
                if (maleYoungAdult[mood] > 0) insertFeedback(advId, maleYoungAdult[mood], "M", "2", (mood + 1).ToString());
                if (maleAdult[mood] > 0) insertFeedback(advId, maleAdult[mood], "M", "3", (mood + 1).ToString());
                if (maleSenior[mood] > 0) insertFeedback(advId, maleSenior[mood], "M", "4", (mood + 1).ToString());
            }
        }

        private void insertFeedback(int advId, int pax, string genderCode, string ageCode, string emoCode)
        {
            SqlCommand cmd = new SqlCommand("insert into AdvertisementFeedback(advId, BillboardId, TimeStamp, NoOfPax, GenderId, AgeId, Emotion) "
                + "values (@aid, @bil, getDate(), @pax, @gender, @age, @emotion)");
            cmd.Parameters.AddWithValue("@aid", advId);
            cmd.Parameters.AddWithValue("@bil", _billboard);
            cmd.Parameters.AddWithValue("@pax", pax);
            cmd.Parameters.AddWithValue("@gender", genderCode);
            cmd.Parameters.AddWithValue("@age", ageCode);
            cmd.Parameters.AddWithValue("@emotion", emoCode);

            db.executeNonQuery(cmd);
        }

        private void calMood(Mood m, ref int[] emotions)
        {
            switch (m)
            {
                case Mood.VeryHappy:
                    emotions[0]++;
                    break;
                case Mood.Happy:
                    emotions[1]++;
                    break;
                case Mood.Neutral:
                    emotions[2]++;
                    break;
                case Mood.Unhappy:
                    emotions[3]++;
                    break;
                case Mood.VeryUnhappy:
                    emotions[4]++;
                    break;
            }
        }

        public Advert GetAdvert(Watcher[] watchers, int[] lastAdv)
        {
            Advert adv = null;
            DataSet ds;
            DataRow dr;
            SqlCommand cmd = new SqlCommand();

            #region actual
            //adv to avoid cos shown before
            string avoidAdv = " and a.advId not in (";
            foreach (int id in lastAdv) avoidAdv += id + ",";
            avoidAdv = avoidAdv.Substring(0, avoidAdv.Length - 1) + ") ";

            if (watchers==null || watchers.Length == 0)
            {
                //random find advert to display
                return randomAdvert(avoidAdv);
            }

            if (watchers.Length == 1)
            {
                //find advert that fit that 1 person to display
                cmd = new SqlCommand("select top 1 * from ( "
                    //find a specific advert that fits the age and gender
                    + "select top 1 a.AdvID, a.Name, a.Item, a.ItemType, a.Duration from Advertisement a "
                    + "inner join AdvertisementAudience au on a.AdvId=au.advId and au.AgeId=@age and au.GenderId=@gender "
                    + "where a.status=1 and (getDate() between a.StartDate and a.EndDate or getDate() >= a.StartDate) "
                    + "and exists (select 1 from AdvertisementLocation l where l.AdvId=a.advId and l.BillboardId=@billboard) "
                    + avoidAdv
                    + "order by NEWID() "
                    + "union all "
                    //find a specific advert that fits the age
                    + "select top 1 a.AdvID, a.Name, a.Item, a.ItemType, a.Duration from Advertisement a "
                    + "inner join AdvertisementAudience au on a.AdvId=au.advId and au.AgeId=@age "
                    + "where a.status=1 and (getDate() between a.StartDate and a.EndDate or getDate() >= a.StartDate) "
                    + "and exists (select 1 from AdvertisementLocation l where l.AdvId=a.advId and l.BillboardId=@billboard) "
                    + avoidAdv
                    + "order by NEWID() "
                    + "union all "
                    //find a specific advert that fits the gender
                    + "select top 1 a.AdvID, a.Name, a.Item, a.ItemType, a.Duration from Advertisement a "
                    + "inner join AdvertisementAudience au on a.AdvId=au.advId and au.GenderId=@gender "
                    + "where a.status=1 and (getDate() between a.StartDate and a.EndDate or getDate() >= a.StartDate) "
                    + "and exists (select 1 from AdvertisementLocation l where l.AdvId=a.advId and l.BillboardId=@billboard) "
                    + avoidAdv
                    + "order by NEWID() "
                    + "union all "
                    //find a random advert that avoid previously shown adverts
                    + "select top 1 a.AdvID, a.Name, a.Item, a.ItemType, a.Duration from Advertisement a "
                    + "where a.status=1 and (getDate() between a.StartDate and a.EndDate or getDate() >= a.StartDate) "
                    + "and exists (select 1 from AdvertisementLocation l where l.AdvId=a.advId and l.BillboardId=@billboard) "
                    + avoidAdv
                    + "order by NEWID() "
                    + "union all "
                    //find a random advert that ignores previously shown adverts (can happen when there are not much records so have to repeat)
                    + "select top 1 a.AdvID, a.Name, a.Item, a.ItemType, a.Duration from Advertisement a "
                    + "where a.status=1 and (getDate() between a.StartDate and a.EndDate or getDate() >= a.StartDate) "
                    + "and exists (select 1 from AdvertisementLocation l where l.AdvId=a.advId and l.BillboardId=@billboard) "
                    + "order by NEWID() "
                    + ") tbl");

                ds = db.getDataSet(cmd);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    adv = new Advert() { id = (int)dr["AdvId"], name = dr["Name"].ToString(), path = dr["Item"].ToString(), duration = (dr["Duration"] == DBNull.Value ? 0 : (int)dr["Duration"]) };
                    if (advertMode == "WEB")
                    {
                        if (dr["ItemType"].ToString() == VIDEO) adv.type = AdvertType.WEB_VID;
                        if (dr["ItemType"].ToString() == IMAGE) adv.type = AdvertType.WEB_IMG;
                    }
                    else
                    {
                        if (dr["ItemType"].ToString() == VIDEO) adv.type = AdvertType.LOCAL_VID;
                        if (dr["ItemType"].ToString() == IMAGE) adv.type = AdvertType.LOCAL_VID;
                    }
                }

                return adv;
            }

            //mulitple audience
            int femaleChild, femaleYoungAdult, femaleAdult, femaleSenior, maleChild, maleYoungAdult, maleAdult, maleSenior;
            int female, male, child, youngAdult, adult, senior;

            //find the statistics of the watchers
            getWatcherStats(watchers, out femaleChild, out femaleYoungAdult, out femaleAdult, out femaleSenior, out maleChild, out maleYoungAdult, out maleAdult, out maleSenior,
                out female, out male, out child, out youngAdult, out adult, out senior);

            int high = 0, tries = 0;
            string condition = "";

            #region combined stats
            //loop through to find a suitable advert based on the combined stats of gender and age
            do
            {
                cmd = new SqlCommand();

                high = max(high, femaleChild, femaleYoungAdult, femaleAdult, femaleSenior, maleChild, maleYoungAdult, maleAdult, maleSenior);

                condition = "";
                if (femaleChild == high)
                {
                    condition += "or (au.AgeId=@age1 and au.GenderId=@gender1) ";
                    cmd.Parameters.AddWithValue("@age1", "1");
                    cmd.Parameters.AddWithValue("@gender1", "F");
                }
                if (femaleYoungAdult == high)
                {
                    condition += "or (au.AgeId=@age2 and au.GenderId=@gender2) ";
                    cmd.Parameters.AddWithValue("@age2", "2");
                    cmd.Parameters.AddWithValue("@gender2", "F");
                }
                if (femaleAdult == high)
                {
                    condition += "or (au.AgeId=@age3 and au.GenderId=@gender3) ";
                    cmd.Parameters.AddWithValue("@age3", "3");
                    cmd.Parameters.AddWithValue("@gender3", "F");
                }
                if (femaleSenior == high)
                {
                    condition += "or (au.AgeId=@age4 and au.GenderId=@gender4) ";
                    cmd.Parameters.AddWithValue("@age4", "4");
                    cmd.Parameters.AddWithValue("@gender4", "F");
                }
                if (maleChild == high)
                {
                    condition += "or (au.AgeId=@age5 and au.GenderId=@gender5) ";
                    cmd.Parameters.AddWithValue("@age5", "1");
                    cmd.Parameters.AddWithValue("@gender5", "M");
                }
                if (maleYoungAdult == high)
                {
                    condition += "or (au.AgeId=@age6 and au.GenderId=@gender6) ";
                    cmd.Parameters.AddWithValue("@age6", "2");
                    cmd.Parameters.AddWithValue("@gender6", "M");
                }
                if (maleAdult == high)
                {
                    condition += "or (au.AgeId=@age7 and au.GenderId=@gender7) ";
                    cmd.Parameters.AddWithValue("@age7", "3");
                    cmd.Parameters.AddWithValue("@gender7", "M");
                }
                if (maleSenior == high)
                {
                    condition += "or (au.AgeId=@age8 and au.GenderId=@gender8) ";
                    cmd.Parameters.AddWithValue("@age8", "4");
                    cmd.Parameters.AddWithValue("@gender8", "M");
                }

                cmd.CommandText = ("select top 1 a.AdvID, a.Name, a.Item, a.ItemType, a.Duration from Advertisement a "
                    + "inner join AdvertisementAudience au on a.AdvId=au.advId and (" + condition.Substring(2) + ") "
                    + "where a.status=1 and (getDate() between a.StartDate and a.EndDate or getDate() >= a.StartDate) "
                    + "and exists (select 1 from AdvertisementLocation l where l.AdvId=a.advId and l.BillboardId=@billboard) "
                    + avoidAdv
                    + "order by NEWID() ");
                cmd.Parameters.AddWithValue("@billboard", _billboard);

                ds = db.getDataSet(cmd);

                if(ds!=null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    adv = new Advert() { id = (int)dr["AdvId"], name = dr["Name"].ToString(), path = dr["Item"].ToString(), duration = (dr["Duration"] == DBNull.Value ? 0 : (int)dr["Duration"]) };
                    if (advertMode == "WEB")
                    {
                        if (dr["ItemType"].ToString() == VIDEO) adv.type = AdvertType.WEB_VID;
                        if (dr["ItemType"].ToString() == IMAGE) adv.type = AdvertType.WEB_IMG;
                    }
                    else
                    {
                        if (dr["ItemType"].ToString() == VIDEO) adv.type = AdvertType.LOCAL_VID;
                        if (dr["ItemType"].ToString() == IMAGE) adv.type = AdvertType.LOCAL_VID;
                    }
                }

                tries++;
            } while (adv == null && tries < 8 && high != 0);
            //continue to try if current loop cannot find suitable advert and total no of loop is less than 8 (means haven go through all the combi of stats) and the highest value of the stats
            //is not zero (can be zero when the audience don fit into all diff combi stat)

            if (adv != null) return adv;
            #endregion

            #region age
            //since using combi stats cannot find advert, try using age only
            high = 0;
            tries = 0;
            do
            {
                cmd = new SqlCommand();
                high = max(high, child, youngAdult, adult, senior);

                condition = "";
                if (child == high)
                {
                    condition += "or au.AgeId=@age1 ";
                    cmd.Parameters.AddWithValue("@age1", "1");
                }
                if (youngAdult == high)
                {
                    condition += "or au.AgeId=@age2 ";
                    cmd.Parameters.AddWithValue("@age2", "2");
                }
                if (adult == high)
                {
                    condition += "or au.AgeId=@age3 ";
                    cmd.Parameters.AddWithValue("@age3", "3");
                }
                if (senior == high)
                {
                    condition += "or au.AgeId=@age4 ";
                    cmd.Parameters.AddWithValue("@age4", "4");
                }

                cmd.CommandText = ("select top 1 a.AdvID, a.Name, a.Item, a.ItemType, a.Duration from Advertisement a "
                    + "inner join AdvertisementAudience au on a.AdvId=au.advId and (" + condition.Substring(2) + ") "
                    + "where a.status=1 and (getDate() between a.StartDate and a.EndDate or getDate() >= a.StartDate) "
                    + "and exists (select 1 from AdvertisementLocation l where l.AdvId=a.advId and l.BillboardId=@billboard) "
                    + avoidAdv
                    + "order by NEWID() ");
                cmd.Parameters.AddWithValue("@billboard", _billboard);

                ds = db.getDataSet(cmd);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    adv = new Advert() { id = (int)dr["AdvId"], name = dr["Name"].ToString(), path = dr["Item"].ToString(), duration = (dr["Duration"] == DBNull.Value ? 0 : (int)dr["Duration"]) };
                    if (advertMode == "WEB")
                    {
                        if (dr["ItemType"].ToString() == VIDEO) adv.type = AdvertType.WEB_VID;
                        if (dr["ItemType"].ToString() == IMAGE) adv.type = AdvertType.WEB_IMG;
                    }
                    else
                    {
                        if (dr["ItemType"].ToString() == VIDEO) adv.type = AdvertType.LOCAL_VID;
                        if (dr["ItemType"].ToString() == IMAGE) adv.type = AdvertType.LOCAL_VID;
                    }
                }

                tries++;
            } while (adv == null && tries < 4 && high != 0);
            //continue to try if current loop cannot find suitable advert and total no of loop is less than 4 (means haven go through all ages) and the highest value of the age
            //is not zero (can be zero when the audience don fit into all diff ages)

            if (adv != null) return adv;
            #endregion

            #region gender
            //since using age also cannot find advert, try using gender only
            high = max(0, male, female);

            cmd = new SqlCommand("select top 1 a.AdvID, a.Name, a.Item, a.ItemType, a.Duration from Advertisement a "
                    + "inner join AdvertisementAudience au on a.AdvId=au.advId and au.GenderId=@gender "
                    + "where a.status=1 and (getDate() between a.StartDate and a.EndDate or getDate() >= a.StartDate) "
                    + "and exists (select 1 from AdvertisementLocation l where l.AdvId=a.advId and l.BillboardId=@billboard) "
                    + avoidAdv
                    + "order by NEWID() ");
            cmd.Parameters.AddWithValue("@billboard", _billboard);
            cmd.Parameters.AddWithValue("@gender", female==high ? "F" : "M");

            ds = db.getDataSet(cmd);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dr = ds.Tables[0].Rows[0];
                adv = new Advert() { id = (int)dr["AdvId"], name = dr["Name"].ToString(), path = dr["Item"].ToString(), duration = (dr["Duration"] == DBNull.Value ? 0 : (int)dr["Duration"]) };
                if (advertMode == "WEB")
                {
                    if (dr["ItemType"].ToString() == VIDEO) adv.type = AdvertType.WEB_VID;
                    if (dr["ItemType"].ToString() == IMAGE) adv.type = AdvertType.WEB_IMG;
                }
                else
                {
                    if (dr["ItemType"].ToString() == VIDEO) adv.type = AdvertType.LOCAL_VID;
                    if (dr["ItemType"].ToString() == IMAGE) adv.type = AdvertType.LOCAL_VID;
                }
            }
            else
            {
                cmd = new SqlCommand("select top 1 a.AdvID, a.Name, a.Item, a.ItemType, a.Duration from Advertisement a "
                    + "inner join AdvertisementAudience au on a.AdvId=au.advId and au.GenderId=@gender "
                    + "where a.status=1 and (getDate() between a.StartDate and a.EndDate or getDate() >= a.StartDate) "
                    + "and exists (select 1 from AdvertisementLocation l where l.AdvId=a.advId and l.BillboardId=@billboard) "
                    + avoidAdv
                    + "order by NEWID() ");
                cmd.Parameters.AddWithValue("@billboard", _billboard);
                cmd.Parameters.AddWithValue("@gender", female == high ? "M" : "F"); //find the opposite from above

                ds = db.getDataSet(cmd);

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    dr = ds.Tables[0].Rows[0];
                    adv = new Advert() { id = (int)dr["AdvId"], name = dr["Name"].ToString(), path = dr["Item"].ToString(), duration = (dr["Duration"] == DBNull.Value ? 0 : (int)dr["Duration"]) };
                    if (advertMode == "WEB")
                    {
                        if (dr["ItemType"].ToString() == VIDEO) adv.type = AdvertType.WEB_VID;
                        if (dr["ItemType"].ToString() == IMAGE) adv.type = AdvertType.WEB_IMG;
                    }
                    else
                    {
                        if (dr["ItemType"].ToString() == VIDEO) adv.type = AdvertType.LOCAL_VID;
                        if (dr["ItemType"].ToString() == IMAGE) adv.type = AdvertType.LOCAL_VID;
                    }
                }
            }

            if (adv != null) return adv;
            #endregion

            //still cannot find, return random advert excluding avoid list then
            adv = randomAdvert(avoidAdv);
            //if cannot find, then find random advert without the avoid list
            if (adv == null) adv = randomAdvert();
            #endregion

            //test
            //string[] images = { "Hydrangeas.jpg", "Lighthouse.jpg", "Penguins.jpg", "sakura.jpg", "Tulips.jpg" };
            //adv = new Advert() { id = 1, name = "test", path = images[(new Random()).Next(5)], type = AdvertType.WEB_IMG, duration = 0 };

            return adv;
        }

        private Advert randomAdvert(string avoidAvd="")
        {
            Advert adv = null;
            DataSet ds;
            DataRow dr;
            SqlCommand cmd = new SqlCommand("select top 1 a.AdvID, a.Name, a.Item, a.ItemType, a.Duration from Advertisement a "
                    + "where a.status=1 and (getDate() between a.StartDate and a.EndDate or getDate() >= a.StartDate) "
                    + "and exists (select 1 from AdvertisementLocation l where l.AdvId=a.advId and l.BillboardId=@billboard) "
                    + avoidAvd
                    + "order by NEWID() ");

            ds = db.getDataSet(cmd);

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                dr = ds.Tables[0].Rows[0];
                adv = new Advert() { id = (int)dr["AdvId"], name = dr["Name"].ToString(), path = dr["Item"].ToString(), duration = (dr["Duration"] == DBNull.Value ? 0 : (int)dr["Duration"]) };
                if (advertMode == "WEB")
                {
                    if (dr["ItemType"].ToString() == VIDEO) adv.type = AdvertType.WEB_VID;
                    if (dr["ItemType"].ToString() == IMAGE) adv.type = AdvertType.WEB_IMG;
                }
                else
                {
                    if (dr["ItemType"].ToString() == VIDEO) adv.type = AdvertType.LOCAL_VID;
                    if (dr["ItemType"].ToString() == IMAGE) adv.type = AdvertType.LOCAL_VID;
                }
            }

            return adv;
        }

        //find the highest no, with the option to exclude an existing no.
        private int max(int exclude, int w1, int w2, int w3 = 0, int w4 = 0, int w5 = 0, int w6 = 0, int w7 = 0, int w8 = 0, int w9 = 0, int w10 = 0, int w11 = 0, int w12 = 0, int w13 = 0, int w14 = 0)
        {
            int max = 0;

            if (w1 > max && w1 != exclude) max = w1;
            if (w2 > max && w2 != exclude) max = w2;
            if (w3 > max && w3 != exclude) max = w3;
            if (w4 > max && w4 != exclude) max = w4;
            if (w5 > max && w5 != exclude) max = w5;
            if (w6 > max && w6 != exclude) max = w6;
            if (w7 > max && w7 != exclude) max = w7;
            if (w8 > max && w8 != exclude) max = w8;
            if (w9 > max && w9 != exclude) max = w9;
            if (w10 > max && w10 != exclude) max = w10;
            if (w11 > max && w11 != exclude) max = w11;
            if (w12 > max && w12 != exclude) max = w12;
            if (w13 > max && w13 != exclude) max = w13;
            if (w14 > max && w14 != exclude) max = w14;

            return max;
        }

        private void getWatcherStats(Watcher[] watchers, out int femaleChild, out int femaleYoungAdult, out int femaleAdult, out int femaleSenior, out int maleChild, out int maleYoungAdult
            , out int maleAdult, out int maleSenior, out int female, out int male, out int child, out int youngAdult, out int adult, out int senior)
        {
            femaleChild = 0;
            femaleYoungAdult = 0;
            femaleAdult = 0;
            femaleSenior = 0;
            maleChild = 0;
            maleYoungAdult = 0;
            maleAdult = 0;
            maleSenior = 0;
            female = 0;
            male = 0;
            child = 0;
            youngAdult = 0;
            adult = 0;
            senior = 0;

            foreach (Watcher w in watchers)
            {
                if (w.gender == Gender.Female)
                {
                    female++;

                    switch (w.ageGroup)
                    {
                        case AgeBracket.Child:
                            femaleChild++;
                            child++;
                            break;
                        case AgeBracket.YoungAdult:
                            femaleYoungAdult++;
                            youngAdult++;
                            break;
                        case AgeBracket.Adult:
                            femaleAdult++;
                            adult++;
                            break;
                        case AgeBracket.Senior:
                            femaleSenior++;
                            senior++;
                            break;
                    }
                }
                else
                {
                    male++;

                    switch (w.ageGroup)
                    {
                        case AgeBracket.Child:
                            maleChild++;
                            child++;
                            break;
                        case AgeBracket.YoungAdult:
                            maleYoungAdult++;
                            youngAdult++;
                            break;
                        case AgeBracket.Adult:
                            maleAdult++;
                            adult++;
                            break;
                        case AgeBracket.Senior:
                            maleSenior++;
                            senior++;
                            break;
                    }
                }
            }
        }
    }
}
