using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using MySqlConnector;

namespace курсачь_Олег_важно.Model
{
    public class FeedbacksRepository
    {
        private FeedbacksRepository()
        {

        }

        static FeedbacksRepository instance;
        public static FeedbacksRepository Instance
        {
            get
            {
                if (instance == null)
                    instance = new FeedbacksRepository();
                return instance;
            }
        }

        internal IEnumerable<Feedback> GetAllFeedbacks(string sql)
        {
            var result = new List<Feedback>();
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return result;

            using (var mc = new MySqlCommand(sql, connect))
            using (var reader = mc.ExecuteReader())
            {
                while (reader.Read())
                {
                    var feedbacks = new Feedback();
                    feedbacks.Id = reader.GetInt32("FeedbackId");
                    feedbacks.Comment = reader.GetString("Comment");
                    feedbacks.Rating = reader.GetInt32("Rating");

                    feedbacks.Event = new Events()
                    {
                        Id = reader.GetInt32("EventId"),
                        Name = reader.GetString("EventName"),
                    };

                    feedbacks.Participant = new Participant()
                    {
                        Id = reader.GetInt32("ParticipantId"),
                        Lastname = reader.GetString("ParticipantLastname"),
                        Name = reader.GetString("ParticipantName"),
                        Surname = reader.GetString("ParticipantSurname"),
                        Phone = reader.GetString("ParticipantPhone"),
                    };

                    result.Add(feedbacks);
                }
            }

            return result;
        }

        internal void AddFeedback(Feedback feedback)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return; 
                
            int id = DB.Instance.GetAutoID("Feedback");

            string sql = "INSERT INTO Feedback VALUES (0, @eventid, @participantid, @rating, @comment)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("eventid", feedback.EventId));
                mc.Parameters.Add(new MySqlParameter("participantid", feedback.ParticipantId));
                mc.Parameters.Add(new MySqlParameter("rating", feedback.Rating));
                mc.Parameters.Add(new MySqlParameter("comment", feedback.Comment));
                mc.ExecuteNonQuery();
            }

        }
             
        internal void UpdateFeedback(Feedback feedback)
        {
            var connect = DB.Instance.GetConnection();
            if (connect == null)
                return;

            string sql = "UPDATE Feedback SET Comment = @comment, Rating = @rating" + feedback.Id;
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("comment", feedback.Comment));
                mc.Parameters.Add(new MySqlParameter("rating", feedback.Rating));
                mc.ExecuteNonQuery();
            }
        }
    }
}
