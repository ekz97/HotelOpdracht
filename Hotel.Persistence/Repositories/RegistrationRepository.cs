using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {

        private string connectionString;

        public RegistrationRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
       
        public void AddRegistration(Registrationn registration, int customerId)
{
    try
    {
        string insertRegistration = "INSERT INTO Registration (activityId) VALUES (@activityId); SELECT SCOPE_IDENTITY();";

        using (SqlConnection conn = getConnection())
        using (SqlCommand cmd = new SqlCommand(insertRegistration, conn))
        {
            conn.Open();
            cmd.Parameters.AddWithValue("@activityId", registration.Activity.Id);

            int newRegistrationId = Convert.ToInt32(cmd.ExecuteScalar());

            string insertMemberRegistration = "INSERT INTO MemberRegistration (memberName, memberBirthday, memberCustomerId, registrationId) VALUES (@memberName, @memberBirthday, @memberCustomerId, @registrationId)";

          
           

            foreach (Member member in registration.Members)
            {
                cmd.CommandText = insertMemberRegistration;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@memberCustomerId", customerId);
                cmd.Parameters.AddWithValue("@registrationId", newRegistrationId);
                cmd.Parameters.AddWithValue("@memberName", member.Name);
                cmd.Parameters.AddWithValue("@memberBirthday", member.Birthday);

                cmd.ExecuteNonQuery();
             
            }
        }
    }
    catch (Exception ex)
    {
        throw new RegistrationRepositoryException("Failed to add registration", ex);
    }
}

    }
}

