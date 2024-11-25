using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace NewTeam6.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        private readonly string ConnStr = "Data Source=LAPTOP-H9ATRROJ\\SQLEXPRESS;Integrated Security=True;Persist Security Info=False;Pooling=False;";

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        // Display login page
        public IActionResult Index()
        {
            return View();
        }

        // Handle login request (POST)
        [HttpPost]
        public async Task<IActionResult> Login(string patIdno, string patPassword)
        {
            if (string.IsNullOrWhiteSpace(patIdno) || string.IsNullOrWhiteSpace(patPassword))
            {
                return Json(new { success = false, error = "請輸入身分證字號和密碼" });
            }

            var isValidUser = await VerifyUserCredentials(patIdno, patPassword);
            if (isValidUser)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, error = "登入失敗，帳號或密碼錯誤" });
            }
        }

        // Verify user credentials in the database
        private async Task<bool> VerifyUserCredentials(string patIdno, string patPassword)
        {
            bool isValid = false;

            var query = "SELECT COUNT(1) FROM NewT6.dbo.Register WHERE PatIdno = @PatIdno AND PatPassword = @PatPassword";
            using (SqlConnection connection = new SqlConnection(ConnStr))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PatIdno", patIdno);
                command.Parameters.AddWithValue("@PatPassword", patPassword);

                try
                {
                    connection.Open();
                    var count = (int)await command.ExecuteScalarAsync();
                    isValid = count > 0; // If count is greater than 0, the user exists
                }
                catch (SqlException ex)
                {
                    _logger.LogError($"SQL Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogError($"General Error: {ex.Message}");
                }
                finally
                {
                    connection.Close();
                }
            }

            return isValid;
        }
    }
}
