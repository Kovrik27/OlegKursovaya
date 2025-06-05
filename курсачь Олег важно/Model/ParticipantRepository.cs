using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using курсачь_Олег_важно.ViewModel;

namespace курсачь_Олег_важно.Model
{
    public class ParticipantRepository
    {
        private ParticipantRepository()
        {

        }

        static ParticipantRepository instance;
        public static ParticipantRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new ParticipantRepository();
                return instance;
            }
        }

        internal IEnumerable<Participant> GetAllParticipant(string sql)
        {
            var result = new List<Participant>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Participant participants;
                int id;
                while (reader.Read())
                {
                    participants = new Participant();
                    participants.Id = reader.GetInt32("ID");
                    participants.Lastname = reader.GetString("Lastname");
                    participants.Name = reader.GetString("Name");
                    participants.Surname = reader.GetString("Surname");
                    participants.Phone = reader.GetString("Phone");
                    participants.Event = reader.GetString("Event");

                    result.Add(participants);
                }
            }

            return result;
        }

        internal IEnumerable<Participant> GetAllEventsWithParticipant(string sql, int eventId)
        {
            var result = new List<Participant>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                Participant participants;
                int id;
                mc.Parameters.AddWithValue("@EventID", eventId);
                while (reader.Read())
                {
                    participants = new Participant();
                    participants.Id = reader.GetInt32("ID");
                    participants.Lastname = reader.GetString("Lastname");
                    participants.Name = reader.GetString("Name");
                    participants.Surname = reader.GetString("Surname");
                    participants.Phone = reader.GetString("Phone");
                    participants.Event = reader.GetString("Event");

                    result.Add(participants);
                }
            }

            return result;
        }
        internal void AddParticipantOnEvent(Participant participant, int eventid)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            //переменная для добавления в кросс-таблицу
            int participantId;

            int id = DB.Instance.GetAutoID("Participant");

            string sql = "INSERT INTO Participants VALUES (0, @lastname, @name, @surname, @phone, @email)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("lastname", participant.Lastname));
                mc.Parameters.Add(new MySqlParameter("name", participant.Name));
                mc.Parameters.Add(new MySqlParameter("surname", participant.Surname));
                mc.Parameters.Add(new MySqlParameter("phone", participant.Phone));
                mc.Parameters.Add(new MySqlParameter("event", participant.Event));
                mc.ExecuteNonQuery();

                //Получение айди последнего добавленного участника     
                participantId = Convert.ToInt32(mc.ExecuteScalar());
            }


            string sql2 = "INSERT INTO EventParticipant VALUES (@EventID, @ParticipantID";
            using (var mc = new MySqlCommand(sql2, connect))
            {
                mc.Parameters.Add(new MySqlParameter("EventID", eventid));
                mc.Parameters.Add(new MySqlParameter("ParticipantID", participantId));

                mc.ExecuteNonQuery();
            }
        }

        internal void UpdateParticipant(Participant participant)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE Participants SET Lastname = @lastname, Name = @name, Surname = @surname, Phone = @phone, Email = @email" + participant.Id;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("lastname", participant.Lastname));
                mc.Parameters.Add(new MySqlParameter("name", participant.Name));
                mc.Parameters.Add(new MySqlParameter("surname", participant.Surname));
                mc.Parameters.Add(new MySqlParameter("phone", participant.Phone));
                mc.Parameters.Add(new MySqlParameter("event", participant.Event));
                mc.ExecuteNonQuery();
            }
        }

        internal IEnumerable<Participant> GetAllParticipant()
        {
            throw new NotImplementedException();
        }
    }
}

