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

            // Сначала загружаем участников
            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                while (reader.Read())
                {
                    var participant = new Participant();
                    participant.Id = reader.GetInt32("ID");
                    participant.Lastname = reader.GetString("Lastname");
                    participant.Name = reader.GetString("Name");
                    participant.Surname = reader.GetString("Surname");
                    participant.Phone = reader.GetString("Phone");

                    // Инициализируем коллекцию EventParticipants
                    participant.EventParticipants = new List<EventParticipants>();

                    result.Add(participant);
                }
            }

            // Теперь для каждого участника загружаем связанные мероприятия
            foreach (var participant in result)
            {
                string eventSql = @"
            SELECT e.ID, e.Name 
            FROM Events e
            INNER JOIN EventsParticipants ep ON e.ID = ep.EventID
            WHERE ep.ParticipantID = @participantId";

                using (var mc = new MySqlCommand(eventSql, connect))
                {
                    mc.Parameters.AddWithValue("@participantId", participant.Id);
                    using (var reader = mc.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var ev = new Events()
                            {
                                Id = reader.GetInt32("ID"),
                                Name = reader.GetString("Name")
                            };

                            var ep = new EventParticipants()
                            {
                                EventID = ev.Id,
                                ParticipantID = participant.Id,
                                Event = ev,
                                Participant = participant
                            };

                            participant.EventParticipants.Add(ep);
                        }
                    }
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

                    result.Add(participants);
                }
            }

            return result;
        }
        internal int AddParticipantOnEvent(Participant participant, int eventid)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return 0;

            int participantId;

            string sql = "INSERT INTO Participant (Lastname, Name, Surname, Phone) VALUES (@lastname, @name, @surname, @phone)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.AddWithValue("@lastname", participant.Lastname);
                mc.Parameters.AddWithValue("@name", participant.Name);
                mc.Parameters.AddWithValue("@surname", participant.Surname);
                mc.Parameters.AddWithValue("@phone", participant.Phone);

                mc.ExecuteNonQuery();

                // Получаем последний вставленный ID
                mc.CommandText = "SELECT LAST_INSERT_ID()";
                participantId = Convert.ToInt32(mc.ExecuteScalar());
            }

            string sql2 = "INSERT INTO EventsParticipants (EventID, ParticipantID) VALUES (@EventID, @ParticipantID)";
            using (var mc = new MySqlCommand(sql2, connect))
            {
                mc.Parameters.AddWithValue("@EventID", eventid);
                mc.Parameters.AddWithValue("@ParticipantID", participantId);

                mc.ExecuteNonQuery();
            }

            return participantId;
        }


        internal void UpdateParticipant(Participant participant)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE Participants SET Lastname = @lastname, Name = @name, Surname = @surname, Phone = @phone" + participant.Id;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("lastname", participant.Lastname));
                mc.Parameters.Add(new MySqlParameter("name", participant.Name));
                mc.Parameters.Add(new MySqlParameter("surname", participant.Surname));
                mc.Parameters.Add(new MySqlParameter("phone", participant.Phone));
                mc.ExecuteNonQuery();
            }
        }

    }
}

