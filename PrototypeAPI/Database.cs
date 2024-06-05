using Npgsql;
using PrototypeAPI.Model;
using System.Threading.Tasks;

namespace PrototypeAPI
{
    public class Database
    {
        NpgsqlConnection con = new NpgsqlConnection(Const.Connect);

        public async Task InsertDataCourseAsync(DateCourse dateCourse, string valCode, string exchangeDate)
        {
            var sql = "INSERT INTO \"date_course\" (\"name\", \"rate\", \"exchangedate\") VALUES (@name, @rate, @exchangedate)";
            NpgsqlCommand comm = new NpgsqlCommand(sql, con);

            comm.Parameters.AddWithValue("name", valCode);
            comm.Parameters.AddWithValue("rate", dateCourse.rate);
            comm.Parameters.AddWithValue("exchangedate", exchangeDate);

            await con.OpenAsync();
            await comm.ExecuteNonQueryAsync();
            await con.CloseAsync();
        }

        public async Task InsertFavoriteValcodeAsync(string valCode)
        {
            if (await CheckIfValcodeExistsAsync(valCode))
            {
                
                return;
            }

            var sql = "INSERT INTO \"favorite_currency\" (\"valcode\") VALUES (@valcode)";
            NpgsqlCommand comm = new NpgsqlCommand(sql, con);

            comm.Parameters.AddWithValue("valcode", valCode);

            await con.OpenAsync();
            await comm.ExecuteNonQueryAsync();
            await con.CloseAsync();
        }

        private async Task<bool> CheckIfValcodeExistsAsync(string valCode)
        {
            var sql = "SELECT COUNT(1) FROM \"favorite_currency\" WHERE \"valcode\" = @valcode";
            NpgsqlCommand comm = new NpgsqlCommand(sql, con);

            comm.Parameters.AddWithValue("valcode", valCode);

            await con.OpenAsync();
            var count = (long)await comm.ExecuteScalarAsync();
            await con.CloseAsync();

            return count > 0;
        }
        public async Task DeleteFavoriteValcodeAsync(string valcode)
        {
            var sql = "DELETE FROM \"favorite_currency\" WHERE \"valcode\" = @valcode";
            NpgsqlCommand comm = new NpgsqlCommand(sql, con);

            comm.Parameters.AddWithValue("valcode", valcode);

            await con.OpenAsync();
            await comm.ExecuteNonQueryAsync();
            await con.CloseAsync();
        }

    }
}

