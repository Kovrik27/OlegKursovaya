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
                Feedback feedbacks;
                int id;
                while (reader.Read())
                {
                    feedbacks = new Feedback();
                    feedbacks.Id = reader.GetInt32("ID");
                    feedbacks.Comment = reader.GetString("Comment");
                    feedbacks.Rating = reader.GetInt32("Rating");
                    feedbacks.Event = new Events()
                    {
                        Name = reader.GetString("Title"),
                    };
                    feedbacks.Participant = new Participant()
                    {
                        Lastname = reader.GetString("Lastname"),
                        Name = reader.GetString("Name"),
                        Surname = reader.GetString("Surname"),
                        Event = reader.GetString("Event"),
                        Phone = reader.GetString("Lastname"),
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

            string sql = "INSERT INTO Feedback VALUES (0, @eventid, @participantid, @participantName,  @rating, @comment)";
            using (var mc = new MySqlCommand(sql, connect))
            {
                mc.Parameters.Add(new MySqlParameter("eventid", feedback.EventId));
                mc.Parameters.Add(new MySqlParameter("participantid", feedback.ParticipantId));
                mc.Parameters.Add(new MySqlParameter("rating", feedback.Rating));
                mc.Parameters.Add(new MySqlParameter("comment", feedback.Comment));
                mc.Parameters.Add(new MySqlParameter("participantName", feedback.Participant.Name));
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
