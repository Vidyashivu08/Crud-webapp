using System;
using System.Data;
using System.Web;
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace CRUD_App
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.ContentType = "application/json";

                switch (Request.HttpMethod)
                {
                    case "GET":
                        HandleGetRequest();
                        break;
                    case "POST":
                        HandlePostRequest();
                        break;
                    case "PUT":
                        HandlePutRequest();
                        break;
                    case "DELETE":
                        HandleDeleteRequest();
                        break;
                    default:
                        Response.StatusCode = 405;
                        Response.Write(SerializeResponse(new { error = "Method Not Allowed" }));
                        break;
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                Response.Write(SerializeResponse(new { error = ex.Message }));
            }
        }

        private void HandleGetRequest()
        {
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                var record = DatabaseHelper.GetRecordById(int.Parse(id));
                Response.Write(SerializeResponse(record));
            }
            else
            {
                var records = DatabaseHelper.GetAllRecords();
                Response.Write(SerializeResponse(records));
            }
        }

        private void HandlePostRequest()
        {
            string json = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            var record = DeserializeRequest<Record>(json);

            if (ValidateRecord(record))
            {
                int newId = DatabaseHelper.CreateRecord(record);
                Response.Write(SerializeResponse(new { id = newId }));
            }
            else
            {
                Response.StatusCode = 400;
                Response.Write(SerializeResponse(new { error = "Invalid data" }));
            }
        }

        private void HandlePutRequest()
        {
            string json = new System.IO.StreamReader(Request.InputStream).ReadToEnd();
            var record = DeserializeRequest<Record>(json);

            if (ValidateRecord(record))
            {
                DatabaseHelper.UpdateRecord(record);
                Response.Write(SerializeResponse(new { success = true }));
            }
            else
            {
                Response.StatusCode = 400;
                Response.Write(SerializeResponse(new { error = "Invalid data" }));
            }
        }

        private void HandleDeleteRequest()
        {
            string id = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(id))
            {
                DatabaseHelper.DeleteRecord(int.Parse(id));
                Response.Write(SerializeResponse(new { success = true }));
            }
            else
            {
                Response.StatusCode = 400;
                Response.Write(SerializeResponse(new { error = "Missing id parameter" }));
            }
        }

        private bool ValidateRecord(Record record)
        {
            return !string.IsNullOrWhiteSpace(record.Name) &&
                   !string.IsNullOrWhiteSpace(record.Address) &&
                   !string.IsNullOrWhiteSpace(record.State) &&
                   !string.IsNullOrWhiteSpace(record.District) &&
                   record.Dob != default(DateTime) &&
                   !string.IsNullOrWhiteSpace(record.Language);
        }

        private string SerializeResponse(object obj)
        {
            return new JavaScriptSerializer().Serialize(obj);
        }

        private T DeserializeRequest<T>(string json)
        {
            return new JavaScriptSerializer().Deserialize<T>(json);
        }
    }

    public class Record
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public DateTime Dob { get; set; }
        public string Language { get; set; }
    }
}
