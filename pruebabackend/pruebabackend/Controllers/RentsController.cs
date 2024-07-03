using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace pruebabackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentsController : ControllerBase
    {
        private readonly SqlConnection _connection;

        public RentsController(SqlConnection connection)
        {
            _connection = connection;
        }

        [HttpGet]
        public async Task<IActionResult> GetRents()
        {
            var query = "SELECT c.idcard, c.name, r.date, r.time, r.balance, ca.plate, ca.brand " +
                        "FROM rent AS r " +
                        "JOIN client AS c ON c.idclient = r.idclient " +
                        "JOIN car AS ca ON ca.plate = r.idplate";

            var dataTable = new DataTable();

            using (var command = new SqlCommand(query, _connection))
            {
                await _connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                dataTable.Load(reader);
                await _connection.CloseAsync();
            }

            return Ok(dataTable);
        }

        [HttpGet("bydate")]
        public async Task<IActionResult> GetRentsByDate([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var query = "SELECT c.idcard, c.name, r.date, r.time, r.balance, ca.plate, ca.brand " +
                        "FROM rent AS r " +
                        "JOIN client AS c ON c.idclient = r.idclient " +
                        "JOIN car AS ca ON ca.plate = r.idplate " +
                        "WHERE r.date BETWEEN @startDate AND @endDate";

            var dataTable = new DataTable();

            using (var command = new SqlCommand(query, _connection))
            {
                command.Parameters.AddWithValue("@startDate", startDate);
                command.Parameters.AddWithValue("@endDate", endDate);

                await _connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();
                dataTable.Load(reader);
                await _connection.CloseAsync();
            }

            return Ok(dataTable);
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetRentalStats()
        {
            var dailyQuery = "SELECT CONVERT(date, date) AS Date, COUNT(*) AS Count FROM rent GROUP BY CONVERT(date, date)";
            var monthlyQuery = "SELECT YEAR(date) AS Year, MONTH(date) AS Month, COUNT(*) AS Count FROM rent GROUP BY YEAR(date), MONTH(date)";

            var dailyRentsTable = new DataTable();
            var monthlyRentsTable = new DataTable();

            await _connection.OpenAsync();

            using (var dailyCommand = new SqlCommand(dailyQuery, _connection))
            {
                var reader = await dailyCommand.ExecuteReaderAsync();
                dailyRentsTable.Load(reader);
            }

            using (var monthlyCommand = new SqlCommand(monthlyQuery, _connection))
            {
                var reader = await monthlyCommand.ExecuteReaderAsync();
                monthlyRentsTable.Load(reader);
            }

            await _connection.CloseAsync();

            return Ok(new { DailyRents = dailyRentsTable, MonthlyRents = monthlyRentsTable });
        }
    }
}
