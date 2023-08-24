using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

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





        public async Task<bool> DeleteProfileAsync()
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("DeleteProfile", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        int result = await command.ExecuteNonQueryAsync();

                        return result > 0;
                    }
                }
            }
            catch (Exception)
            {
                // Handle exception
                return false;
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

        public async Task<List<dynamic>> GetAllProfilesAsync()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var query = "SELECT * FROM UserProfiles";

                using (var command = new SqlCommand(query, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var profiles = new List<dynamic>();
                    while (await reader.ReadAsync())
                    {
                        var profile = new
                        {
                            UserId = reader.GetInt32(0),
                            FullName = reader.GetString(1),
                            Birthday = reader.GetDateTime(2),
                            Gender = reader.GetString(3),
                            City = reader.GetString(4),
                            PostalCode = reader.GetString(5)
                        };
                        profiles.Add(profile);
                    }

                    return profiles;
                }
            }
        }


        public async Task<dynamic> GetProfileByIdAsync(int userId)
        {
            dynamic profile = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (SqlCommand command = new SqlCommand("SELECT UserID, FullName, Birthday, Gender, City, PostalCode FROM UserProfiles WHERE UserID = @UserId", connection))
                {
                    command.Parameters.AddWithValue("@UserId", userId);

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            profile = new
                            {
                                UserID = reader.GetInt32(0),
                                FullName = reader.GetString(1),
                                Birthday = reader.GetDateTime(2),
                                Gender = reader.GetString(3),
                                City = reader.GetString(4),
                                PostalCode = reader.GetString(5)
                            };
                        }
                    }
                }
            }

            return profile ?? new { UserID = -1, FullName = "", Birthday = DateTime.MinValue, Gender = "", City = "", PostalCode = "" };
        }










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
    }
}
