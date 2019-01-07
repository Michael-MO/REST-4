using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace RESTService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeassurementsController : ControllerBase
    {
        private const string conString = "Server=tcp:mmoserver.database.windows.net,1433;Initial Catalog=PracticeDB;Persist Security Info=False;User ID=PracticeServer;Password=Michael19800981;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        // GET: api/Meassurements
        [HttpGet]
        public List<Meassurement> GetAll()
        {
            List<Meassurement> objList = new List<Meassurement>();

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Meassurement ORDER BY Id", con))

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    objList = new List<Meassurement>();

                    while (reader.Read())
                    {
                        Meassurement obj = new Meassurement();
                        obj.ID = Convert.ToInt32(reader["Id"]);
                        obj.Pressure = Convert.ToDouble(reader["Pressure"]);
                        obj.Humidity = Convert.ToDouble(reader["Humidity"]);
                        obj.Temperature = Convert.ToDouble(reader["Temperature"]);
                        obj.TimeOfEntry = Convert.ToDateTime(reader["TimeOfEntry"]);

                        if (objList.All(I => I.ID != Convert.ToInt32(reader["Id"])))
                        {
                            objList.Add(obj);
                        }
                    }
                }

                con.Close();
            }

            return objList;
        }

        // GET: api/Meassurements/5
        [HttpGet("{id}")]
        public Meassurement GetOne(int id)
        {
            Meassurement tempObj = null;

            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM Meassurement WHERE Id = " + id + " ORDER BY Id", con))

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Meassurement obj = new Meassurement();
                        obj.ID = Convert.ToInt32(reader["Id"]);
                        obj.Pressure = Convert.ToDouble(reader["Pressure"]);
                        obj.Humidity = Convert.ToDouble(reader["Humidity"]);
                        obj.Temperature = Convert.ToDouble(reader["Temperature"]);
                        obj.TimeOfEntry = Convert.ToDateTime(reader["TimeOfEntry"]);

                        tempObj = obj;
                    }
                }

                con.Close();
            }

            return tempObj;
        }

        // POST: api/Meassurements
        [HttpPost]
        public void PostOne([FromBody] Meassurement obj)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand("INSERT INTO Meassurement (Pressure, Humidity, Temperature, TimeOfEntry)" +
                                                            "VALUES (@Pressure, @Humidity, @Temperature, GETDATE())", con))
                {
                    command.Parameters.AddWithValue("@Pressure", obj.Pressure);
                    command.Parameters.AddWithValue("@Humidity", obj.Humidity);
                    command.Parameters.AddWithValue("@Temperature", obj.Temperature);
                    command.ExecuteNonQuery();
                }

                con.Close();
            }
        }

        // PUT: api/Meassurements/5
        [HttpPut("{id}")]
        public void PutOne([FromBody] Meassurement obj, int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("UPDATE Meassurement SET Pressure = @Pressure, Humidity = @Humidity, Temperature = @Temperature WHERE Id = @Id", con);

                command.Parameters.AddWithValue("@Pressure", obj.Pressure);
                command.Parameters.AddWithValue("@Humidity", obj.Humidity);
                command.Parameters.AddWithValue("@Temperature", obj.Temperature);
                command.Parameters.AddWithValue("@Id", id);

                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
        }

        // DELETE: api/Measssurements/5
        [HttpDelete("{id}")]
        public void DeleteOne(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM Meassurement WHERE Id = @Id", con);
                command.Parameters.AddWithValue("@Id", id);

                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
