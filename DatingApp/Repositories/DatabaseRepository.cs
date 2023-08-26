using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DatingApp.Entities;


namespace DatingApp.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly string _connectionString;

        public DatabaseRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<bool> CreateAccountAsync(string username, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
            }
        }


        public async Task<string> GetPasswordAsync(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                string query = "SELECT Password FROM Users WHERE Username = @Username";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    var result = await command.ExecuteScalarAsync();

                    if (result != null && result != DBNull.Value)
                    {
                        return result.ToString() ?? string.Empty;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
            }
        }






        public async Task<bool> DeleteAccountAsync(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("DeleteAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userId);

                    connection.Open();
                    int result = await command.ExecuteNonQueryAsync();

                    return result > 0;
                }
            }
        }

        //public async Task<bool> CreateProfileAsync(int userId, string fullName, DateTime birthday, string gender, string city, string postalCode)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        using (var command = new SqlCommand("CreateProfile", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@UserID", userId);
        //            command.Parameters.AddWithValue("@FullName", fullName);
        //            command.Parameters.AddWithValue("@Birthday", birthday);
        //            command.Parameters.AddWithValue("@Gender", gender);
        //            command.Parameters.AddWithValue("@City", city);
        //            command.Parameters.AddWithValue("@PostalCode", postalCode);

        //            connection.Open();
        //            int result = await command.ExecuteNonQueryAsync();

        //            return result > 0;
        //        }
        //    }
        //}

        public async Task<bool> CreateProfileAsync(string fullName, DateTime birthday, string gender, string city, string postalCode)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var query = "INSERT INTO UserProfiles (FullName, Birthday, Gender, City, PostalCode) VALUES (@FullName, @Birthday, @Gender, @City, @PostalCode)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@FullName", fullName);
                        command.Parameters.AddWithValue("@Birthday", birthday);
                        command.Parameters.AddWithValue("@Gender", gender);
                        command.Parameters.AddWithValue("@City", city);
                        command.Parameters.AddWithValue("@PostalCode", postalCode);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return true; // Profile creation succeeded
            }
            catch (Exception)
            {
                // Handle any exceptions that occur during profile creation
                return false; // Profile creation failed
            }
        }







        //public async Task<bool> DeleteProfileAsync(int userId)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        using (var command = new SqlCommand("DeleteProfile", connection))
        //        {
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.Parameters.AddWithValue("@UserID", userId);

        //            connection.Open();
        //            int result = await command.ExecuteNonQueryAsync();

        //            return result > 0;
        //        }
        //    }
        //}





        public async Task<bool> DeleteProfileAsync(int userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("DeleteProfile", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userId);

                    connection.Open();
                    int result = await command.ExecuteNonQueryAsync();

                    return result > 0;
                }
            }
        }




        public async Task<bool> AddLikeAsync(int likerUserId, int likedUserId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("AddLike", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@LikerUserId", likerUserId);
                    command.Parameters.AddWithValue("@LikedUserId", likedUserId);

                    connection.Open();
                    int result = await command.ExecuteNonQueryAsync();

                    return result > 0;
                }
            }
        }





        public async Task<bool> CheckForProfileAsync(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT COUNT(*) FROM Users u JOIN UserProfiles up ON u.Username = up.FullName WHERE u.Username = @Username", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    var result = await command.ExecuteScalarAsync();
                    var profileCount = Convert.ToInt32(result);

                    return profileCount > 0;
                }
            }
        }



        //public async Task<List<dynamic>> GetAllProfilesAsync()
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        using (var command = new SqlCommand("SELECT UserID, FullName FROM UserProfiles", connection))
        //        {
        //            var profiles = new List<dynamic>();
        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                while (await reader.ReadAsync())
        //                {
        //                    profiles.Add(new
        //                    {
        //                        UserId = reader.GetInt32(0),
        //                        FullName = reader.GetString(1)
        //                    });
        //                }
        //            }
        //            return profiles;
        //        }
        //    }
        //}


        //public async Task<dynamic> GetProfileByIdAsync(int userId)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        using (var command = new SqlCommand("SELECT FullName, Birthday, Gender, City, PostalCode FROM UserProfiles WHERE UserID = @UserId", connection))
        //        {
        //            command.Parameters.AddWithValue("@UserId", userId);

        //            using (var reader = await command.ExecuteReaderAsync())
        //            {
        //                if (await reader.ReadAsync())
        //                {
        //                    return new
        //                    {
        //                        FullName = reader.GetString(0),
        //                        Birthday = reader.GetDateTime(1),
        //                        Gender = reader.GetString(2),
        //                        City = reader.GetString(3),
        //                        PostalCode = reader.GetString(4)
        //                    };
        //                }
        //                return null;
        //            }
        //        }
        //    }
        //}

   



        public async Task<bool> CheckForMatchAsync(int userId1, int userId2)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("CheckForMatch", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID1", userId1);
                    command.Parameters.AddWithValue("@UserID2", userId2);
                    command.Parameters.Add("@Match", SqlDbType.Bit).Direction = ParameterDirection.Output;

                    connection.Open();
                    await command.ExecuteNonQueryAsync();

                    return (bool)command.Parameters["@Match"].Value;
                }
            }
        }





        //--------------------------------------------------------------

        public Profile GetProfile(int id)
        {
            Profile profile = new Profile();
            profile.UserID = id;

            SqlConnection sqlCon = null;
            String SqlconString = _connectionString;
            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("usp_GetUserProfile", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                sql_cmnd.Parameters.AddWithValue("@UserID", SqlDbType.UniqueIdentifier).Value = profile.UserID;
                using (SqlDataReader sdr = sql_cmnd.ExecuteReader())
                {
                    while (sdr.Read())
                    {

                        profile.UserID = int.Parse(sdr["UserID"].ToString());
                        profile.FullName = Convert.ToString(sdr["FullName"]);
                        profile.Birthday = Convert.ToDateTime(sdr["Birthday"]);
                        profile.Gender = Convert.ToString(sdr["Gender"]);
                        profile.City = Convert.ToString(sdr["City"]);
                        profile.PostalCode = Convert.ToString(sdr["PostalCode"]);

                    }
                }
                sqlCon.Close();
                return profile;
            }
        }



        public List<Profile> GetProfiles()
        {
            List<Profile> profiles = new List<Profile>();
            SqlConnection sqlCon = null;
            string SqlconString = _connectionString;
            using (sqlCon = new SqlConnection(SqlconString))
            {
                sqlCon.Open();
                SqlCommand sql_cmnd = new SqlCommand("usp_AllProfiles", sqlCon);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader sdr = sql_cmnd.ExecuteReader())
                {

                    while (sdr.Read())
                    {
                        profiles.Add(new Profile
                        {
                            UserID = int.Parse(sdr["UserID"].ToString()),
                            FullName = Convert.ToString(sdr["FullName"]),
                            Birthday = Convert.ToDateTime(sdr["Birthday"]),
                            Gender = Convert.ToString(sdr["Gender"]),
                            City = Convert.ToString(sdr["City"]),
                            PostalCode = Convert.ToString(sdr["PostalCode"]),
                        });
                    }
                }
                sqlCon.Close();
                return profiles;
            }
        }



        public async Task<List<Profile>> GetProfilesByGender(string gender)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("usp_GetProfilesByGender", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Gender", gender);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<Profile> profiles = new List<Profile>();

                        while (await reader.ReadAsync())
                        {
                            profiles.Add(new Profile
                            {
                                UserID = (int)reader["UserID"],
                                FullName = (string)reader["FullName"],
                                Birthday = (DateTime)reader["Birthday"],
                                Gender = (string)reader["Gender"],
                                City = (string)reader["City"],
                                PostalCode = (string)reader["PostalCode"]
                            });
                        }

                        return profiles;
                    }
                }
            }
        }







        //---------------------------------------------------------



        public async Task<int?> GetUserIdByUsername(string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("SELECT UserID FROM Users WHERE Username = @Username", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);

                    var result = await command.ExecuteScalarAsync();
                    if (result != null && result != DBNull.Value)
                    {
                        return (int)result;
                    }
                    else
                    {
                        return null; // User not found
                    }
                }
            }
        }











    }
}
