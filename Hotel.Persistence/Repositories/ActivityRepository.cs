using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private string connectionString;
        public ActivityRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        public IReadOnlyList<Activity> GetActivitiesByOrganiserId(int organiserId)
        {
            try
            {
                Dictionary<int, Activity> activities = new Dictionary<int, Activity>();
                string sql = $"SELECT a.id,a.fixture,a.nrOfPlaces,d.duration,d.location,d.explanation,d.name,p.adultPrice,p.childPrice,p.discount FROM dbo.Activity a JOIN dbo.Description d ON a.descriptionId = d.id JOIN dbo.PriceInfo p ON a.priceInfoId = p.id WHERE a.organiserId = {organiserId} AND a.status = 1";
                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int ActivityId = Convert.ToInt32(reader["id"]);
                            if (!activities.ContainsKey(ActivityId))
                            {
                                Activity activity = new Activity(ActivityId, (DateTime)reader["fixture"], (int)reader["nrofPlaces"], new Description((int)reader["duration"], (string)reader["location"], (string)reader["explanation"], (string)reader["name"]), new PriceInfo((int)reader["adultPrice"], (int)reader["childPrice"], (int)reader["discount"]));
                                activities.Add(ActivityId, activity);
                            }
                        }
                    }
                }
                return activities.Values.ToList();
            }
            catch (Exception ex)
            {
                throw new ActivityRepositoryException("GetActivitiesByOrganiserId", ex);
            }
        }

        public IReadOnlyList<Description> GetAllDescriptions()
        {
            try
            {
                List<Description> descriptions = new List<Description>();
                string sql = "SELECT d.duration, d.explanation, d.location, d.name FROM dbo.Description d WHERE d.status = 1";
                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Description description = new Description((int)(reader["duration"]), (string)reader["location"], (string)reader["explanation"], (string)reader["name"]);
                            descriptions.Add(description);
                        }
                    }
                }
                return descriptions;
            }
            catch( Exception ex)
            {
                throw new ActivityRepositoryException("GetAllDescriptions",ex);
            }
        }

        public IReadOnlyList<PriceInfo> GetAllPriceInfos()
        {
            try
            {
                List<PriceInfo> priceInfos = new List<PriceInfo>();
                string sql = "SELECT p.adultPrice, p.childPrice, p.discount FROM dbo.PriceInfo p WHERE p.status = 1";
                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PriceInfo priceInfo = new PriceInfo((int)reader["adultPrice"], (int)reader["childPrice"], (int)reader["discount"]);
                            priceInfos.Add(priceInfo);
                        }
                    }
                }
                return priceInfos;
            }
            catch (Exception ex)
            {
                throw new ActivityRepositoryException("GetAllPriceInfos", ex);
            }
        }
        public void AddActivity(Activity activity, int organiserId)
        {
            try
            {
                var descriptions = GetAllDescriptions();
                var priceInfos = GetAllPriceInfos();

                if (!descriptions.Contains(activity.Description))
                {
                    AddDescription(activity.Description);
                }
                if (!priceInfos.Contains(activity.PriceInfo))
                {
                    AddPriceInfo(activity.PriceInfo);
                }
                int descriptionId = GetDescriptionId(activity.Description);
                int priceinfoId = GetPriceInfoId(activity.PriceInfo);

                string insertActivitySql = "INSERT INTO dbo.Activity(fixture,nrOfPlaces,organiserId,descriptionId,priceInfoId,status ) VALUES(@fixture,@nrOfPlaces,@organiserId,@descriptionId,@priceInfoId,@status)";
;
                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = new SqlCommand(insertActivitySql, conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("fixture", activity.Fixture);
                    cmd.Parameters.AddWithValue("nrOfPlaces", activity.NrOfPlaces);
                    cmd.Parameters.AddWithValue("organiserId", organiserId);
                    cmd.Parameters.AddWithValue("descriptionId", descriptionId);
                    cmd.Parameters.AddWithValue("priceInfoId", priceinfoId);
                    cmd.Parameters.AddWithValue("status", 1);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new ActivityRepositoryException("AddActivity", ex);
            }
        }
        public void UpdateActivity(Activity activity)
        {

        }
        private void AddDescription(Description description)
        {
            try
            {
                string insertDescriptionSql = "INSERT INTO dbo.Description(duration,location,explanation,name,status) VALUES(@duration,@location,@explanation,@name,@status)";
                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    try
                    {
                        cmd.CommandText = insertDescriptionSql;

                        cmd.Parameters.AddWithValue("@duration", description.Duration);
                        cmd.Parameters.AddWithValue("@location", description.Location);
                        cmd.Parameters.AddWithValue("@explanation", description.Explanation);
                        cmd.Parameters.AddWithValue("@name",description.Name);
                        cmd.Parameters.AddWithValue("@status", 1);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                 }
            }
            catch(Exception ex)
            {
                throw new ActivityRepositoryException("AddDescription", ex);
            }
        }
        private void AddPriceInfo(PriceInfo priceinfo)
        {
            try
            {
                string insertDescriptionSql = "INSERT INTO dbo.PriceInfo(adultPrice,childPrice,discount,status) VALUES(@adultPrice,@childPrice,@discount,@status)";
                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    try
                    {
                        cmd.CommandText = insertDescriptionSql;

                        cmd.Parameters.AddWithValue("@adultPrice",priceinfo.AdultPrice);
                        cmd.Parameters.AddWithValue("@childPrice",priceinfo.ChildPrice);
                        cmd.Parameters.AddWithValue("@discount",priceinfo.Discount);
                        cmd.Parameters.AddWithValue("@status", 1);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ActivityRepositoryException("AddPriceInfo", ex);
            }
        }
        private int GetDescriptionId(Description description)
        {
            try
            {
                string selectDescriptionSql = "SELECT d.id FROM dbo.Description d WHERE d.duration = @duration AND d.location = @location AND d.explanation = @explanation AND d.name = @name";

                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = new SqlCommand(selectDescriptionSql, conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@duration", description.Duration);
                    cmd.Parameters.AddWithValue("@location", description.Location);
                    cmd.Parameters.AddWithValue("@explanation", description.Explanation);
                    cmd.Parameters.AddWithValue("@name", description.Name);

                    var result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int descriptionId))
                    {
                        return descriptionId;
                    }

                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw new ActivityRepositoryException("GetDescriptionId", ex);
            }
        }

        private int GetPriceInfoId(PriceInfo priceInfo)
        {
            try
            {
                string selectPriceInfoSql = "SELECT p.id FROM dbo.PriceInfo p WHERE p.adultPrice = @adultPrice AND p.childPrice = @childPrice AND p.discount = @discount";

                using (SqlConnection conn = getConnection())
                using (SqlCommand cmd = new SqlCommand(selectPriceInfoSql, conn))
                {
                    conn.Open();

                    cmd.Parameters.AddWithValue("@adultPrice", priceInfo.AdultPrice);
                    cmd.Parameters.AddWithValue("@childPrice", priceInfo.ChildPrice);
                    cmd.Parameters.AddWithValue("@discount", priceInfo.Discount);

                    var result = cmd.ExecuteScalar();

                    if (result != null && int.TryParse(result.ToString(), out int priceInfoId))
                    {
                        return priceInfoId;
                    }

                    return -1;
                }
            }
            catch (Exception ex)
            {
                throw new ActivityRepositoryException("GetPriceInfoId", ex);
            }
        }
    }  
}
