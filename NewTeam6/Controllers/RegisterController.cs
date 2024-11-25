using Microsoft.AspNetCore.Mvc;
using NewTeam6.Models;
using NewTeam6.Views;
using System.Data.SqlClient;

namespace NewTeam6.Controllers
{
    public class RegisterController : Controller
    {
        private readonly string ConnStr = "Data Source=LAPTOP-H9ATRROJ\\SQLEXPRESS;Integrated Security=True;Persist Security Info=False;Pooling=False;";
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
        }

        // Save Registration (only insert)
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Save([FromForm] RegisterViewModel registerViewModel)
        {
            try
            {
                // 檢查密碼是否一致
                if (registerViewModel.PatPassword != registerViewModel.ConfirmPassword)
                {
                    return BadRequest("密碼和確認密碼不匹配");
                }

                var dbResult = false;
                var registerDBModel = ConvertRegisterViewModelToDBModel(registerViewModel);

                // 儲存註冊資料
                dbResult = await InsertRegister(registerDBModel);

                if (!dbResult)
                {
                    return BadRequest("儲存失敗");
                }

                return Ok("儲存成功"); // 成功時返回 "儲存成功"
            }
            catch (Exception ex)
            {
                return Json(new { Status = "Error", Error = ex.Message });
            }
        }


        // Convert Register ViewModel to DBModel (only PatIdno and PatPassword)
        private RegisterDBModel ConvertRegisterViewModelToDBModel(RegisterViewModel viewModel)
        {
            return new RegisterDBModel()
            {
                PatIdno = viewModel.PatIdno, // Only PatIdno
                PatPassword = viewModel.PatPassword, // Only PatPassword
            };
        }

        // Insert New Registration into DB
        public async Task<bool> InsertRegister(RegisterDBModel register)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(ConnStr);

            var insertStr = @"INSERT INTO NewT6.dbo.Register
                       (PatIdno, PatPassword)
                       VALUES (@PatIdno, @PatPassword)";

            SqlCommand command = new SqlCommand(insertStr, connection);

            command.Parameters.Add(new SqlParameter("@PatIdno", register.PatIdno));
            command.Parameters.Add(new SqlParameter("@PatPassword", register.PatPassword));

            try
            {
                connection.Open();
                int rowsAffected = await command.ExecuteNonQueryAsync();
                connection.Close();

                Console.WriteLine($"Rows Affected: {rowsAffected}");  // Debugging line to check if any rows were inserted
                result = rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                // Capture SQL errors and return a detailed message
                Console.WriteLine($"SQL Error: {ex.Message}");
                result = false;
            }
            catch (Exception ex)
            {
                // Capture general errors and return a detailed message
                Console.WriteLine($"General Error: {ex.Message}");
                result = false;
            }

            return result;
        }

    }
}
