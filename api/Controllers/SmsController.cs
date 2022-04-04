using api.Models;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ISmsService _smsService;

        public SmsController(IConfiguration configuration, ISmsService smsService)
        {
            _configuration = configuration;
            _smsService = smsService;
        }

        [HttpGet]
        public JsonResult Get()
        {
            //TODO setup Entity Framework instead.
            string query = @"SELECT Id, DateReceived, Content
                             FROM dbo.ReceivedMessages";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("FullStackAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                try
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                }

            }
            var jsonString = JsonConvert.SerializeObject(table);
            var Jobject = JsonConvert.DeserializeObject<List<ReceivedMessages>>(jsonString);

            return new JsonResult(Jobject);
        }

        [HttpPost]
        public JsonResult Post([FromBody]ReceivedMessages receivedMessages)
        {
            string query = @"INSERT into dbo.ReceivedMessages 
                             VALUES (@Date, @Content)";

            string sqlDataSource = _configuration.GetConnectionString("FullStackAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                try
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Date", DateTime.UtcNow);
                        myCommand.Parameters.AddWithValue("@Content", receivedMessages.Content);
                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myCon.Close();
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                }
            }
            return new JsonResult("Added Succesfully");
        }

        [HttpPut]
        public JsonResult Put([FromBody]ReceivedMessages receivedMessages)
        {
            string query = @"UPDATE dbo.ReceivedMessages
                            SET ReceivedMessages.DateReceived = @Date,
                            ReceivedMessages.Content = @Content
                            WHERE ReceivedMessages.Id = @Id";

            string sqlDataSource = _configuration.GetConnectionString("FullStackAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                try
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Id", receivedMessages.Id);
                        myCommand.Parameters.AddWithValue("@Content", receivedMessages.Content);
                        myCommand.Parameters.AddWithValue("@Date", receivedMessages.DateReceived);
                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myCon.Close();
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                }
            }
            return new JsonResult("Record was updated");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string findQuery = @"SELECT * FROM dbo.ReceivedMessages
                                WHERE ReceivedMessages.Id = @Id";


            string query = @"DELETE FROM dbo.ReceivedMessages
                            WHERE ReceivedMessages.Id = @Id";

            string sqlDataSource = _configuration.GetConnectionString("FullStackAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                try
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(findQuery, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Id", id);
                        myReader = myCommand.ExecuteReader();
                        if (!myReader.HasRows)
                        {
                            myReader.Close();
                            myCon.Close();
                            return new JsonResult("Record not found");
                        }
                    }
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@Id", id);
                        myReader = myCommand.ExecuteReader();
                        myReader.Close();
                        myCon.Close();
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                }
            }
            return new JsonResult("Deleted Successfully");
        }


        [HttpPost]
        public HttpResponseMessage SendSms(string content, string number)
        {
            _smsService.SendSms(content, number);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
