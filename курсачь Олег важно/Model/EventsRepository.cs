using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace курсачь_Олег_важно.Model
{
    public class EventsRepository
    {
        private EventsRepository()
        {

        }

        static EventsRepository instance;
        public static EventsRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new EventsRepository();
                return instance;
            }
        }

        internal IEnumerable<Events> GetAllEvents(string sql)
        {
            var result = new List<Events>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Events events;
                int id;
                while (reader.Read())
                {
                    events = new Events();
                    events.Id = reader.GetInt32("ID");
                    events.Name = reader.GetString("Title");
                    events.Date = reader.GetDateTime("Date");
                    events.Location = reader.GetString("Location");
                    result.Add(events);
                }
            }

            return result;
        }

        

        internal void AddEvent(Events events)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            int id = DB.Instance.GetAutoID("Event");

            string sql = "INSERT INTO Event VALUES (0, @name, @location, @date, @organizerId)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("name", events.Name));
                mc.Parameters.Add(new MySqlParameter("location", events.Location));
                mc.Parameters.Add(new MySqlParameter("date", events.Date));
                mc.Parameters.Add(new MySqlParameter("organizerId", events.OrganizerId));
                mc.ExecuteNonQuery();
            }

        }

        internal void UpdateEvent(Events events)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE Event SET Name = @name, Location = @location, Date = @date" + events.Id;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("name", events.Name));
                mc.Parameters.Add(new MySqlParameter("location", events.Location));
                mc.Parameters.Add(new MySqlParameter("date", events.Date));
                mc.Parameters.Add(new MySqlParameter("organizerId", events.OrganizerId));
                mc.ExecuteNonQuery();
            }
        }
    }
}
