using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using курсачь_Олег_важно.ViewModel;

namespace курсачь_Олег_важно.Model
{
    public class OrganizersRepository
    {
        private OrganizersRepository()
        {

        }

        static OrganizersRepository instance;
        public static OrganizersRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new OrganizersRepository();
                return instance;
            }
        }

        internal IEnumerable<Organizer> GetAllOrganizers(string sql)
        {
            var result = new List<Organizer>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Organizer organizers;
                int id;
                while (reader.Read())
                {
                    organizers = new Organizer();
                    organizers.Id = reader.GetInt32("ID");
                    organizers.Lastname = reader.GetString("Lastname");
                    organizers.Name = reader.GetString("Name");
                    organizers.Surname = reader.GetString("Surname");
                    organizers.Phone = reader.GetString("Phone");
                    organizers.Speciality = reader.GetString("Speciality");             

                    result.Add(organizers);
                }
            }

            return result;
        }
        internal void AddOrganizer(Organizer organizer)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            int id = DB.Instance.GetAutoID("Organizer");

            string sql = "INSERT INTO Organizer VALUES (0, @lastname, @name, @surname, @phone, @speciality)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("lastname", organizer.Lastname));
                mc.Parameters.Add(new MySqlParameter("name", organizer.Name));
                mc.Parameters.Add(new MySqlParameter("surname", organizer.Surname));
                mc.Parameters.Add(new MySqlParameter("phone", organizer.Phone));
                mc.Parameters.Add(new MySqlParameter("speciality", organizer.Speciality));
                mc.ExecuteNonQuery();
            }

        }

        internal void UpdateOrganizer(Organizer organizer)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE Organizer SET Lastname = @lastname, Name = @name, Surname = @surname, Phone = @phone, Speciality = @speciality" + organizer.Id;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("lastname", organizer.Lastname));
                mc.Parameters.Add(new MySqlParameter("name", organizer.Name));
                mc.Parameters.Add(new MySqlParameter("surname", organizer.Surname));
                mc.Parameters.Add(new MySqlParameter("phone", organizer.Phone));
                mc.Parameters.Add(new MySqlParameter("speciality", organizer.Speciality));
                      
                
                
                mc.ExecuteNonQuery();
            }
        }
    }
}

