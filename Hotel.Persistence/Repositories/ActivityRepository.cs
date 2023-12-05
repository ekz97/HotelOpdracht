using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        public IReadOnlyList<Activity> GetActivitiesByOrganiserId(int id)
        {
            try
            {
                Dictionary<int, Activity> activities = new Dictionary<int, Activity>();
                string sql = $"SELECT a.id,a.fixture,a.nrOfPlaces,d.duration,d.location,d.explanation,d.name,p.adultPrice,p.childPrice,p.discount FROM dbo.Activity a JOIN dbo.Description d ON a.descriptionId = d.id JOIN dbo.PriceInfo p ON a.priceInfoId = p.id WHERE a.organiserId = {id} AND a.status = 1";
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
    }
}
