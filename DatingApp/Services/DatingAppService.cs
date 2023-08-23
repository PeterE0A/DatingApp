using System;
using System.Threading.Tasks;
using DatingApp.Repositories;
using System.Data.SqlClient;
using DatingApp.Services;

namespace DatingApp.Services
{
    public class DatingAppService : IDatingAppService
    {

       
        private readonly string _connectionString;
        private readonly IDatabaseRepository _databaseRepository;
        


        public DatingAppService(string connectionString, IDatabaseRepository databaseRepository)
        {
            _connectionString = connectionString;
            _databaseRepository = databaseRepository;
        }

        //public async Task<bool> VerifyCredentialsAsync(string username, string password)
        //{
        //    string storedPassword = await _databaseRepository.GetPasswordAsync(username);
        //    return password == storedPassword;
        //}





        public async Task<bool> CheckForProfileAsync(string username)
        {
            return await _databaseRepository.CheckForProfileAsync(username);
        }



        public async Task<bool> CreateAccountAsync(string username, string password)
        {
            // Call repository method to create account
            return await _databaseRepository.CreateAccountAsync(username, password);
        }

        //public async Task<bool> DeleteAccountAsync(string username, string password)
        //{
        //    try
        //    {
        //        bool credentialsValid = await VerifyCredentialsAsync(username, password);

        //        if (!credentialsValid)
        //        {
        //            return false; // Invalid credentials, account deletion failed
        //        }

        //        int userId = await GetUserIdByUsername(username);

        //        if (userId == -1)
        //        {
        //            return false; // User not found, account deletion failed
        //        }

        //        bool success = await _databaseRepository.DeleteAccountAsync(userId);

        //        return success;
        //    }
        //    catch (Exception)
        //    {
        //        return false; // Handle exceptions appropriately
        //    }
        //}

        // Implement the GetUserIdByUsername method to fetch the user's ID based on the username
        //private async Task<int> GetUserIdByUsername(string username)
        //{
        //    using (var connection = new SqlConnection(_connectionString))
        //    {
        //        await connection.OpenAsync();

        //        string query = "SELECT Id FROM Users WHERE Username = @Username";

        //        using (var command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@Username", username);

        //            var result = await command.ExecuteScalarAsync();

        //            if (result != null && result != DBNull.Value)
        //            {
        //                return Convert.ToInt32(result);
        //            }
        //            else
        //            {
        //                return -1; // Return -1 if user not found
        //            }
        //        }
        //    }
        //}

        public async Task<bool> DeleteAccountAsync(int userId)
        {
            // Call repository method to delete account
            return await _databaseRepository.DeleteAccountAsync(userId);
        }


        //public async Task<bool> CreateProfileAsync(int userId, string fullName, DateTime birthday, string gender, string city, string postalCode)
        //{
        //    // Call repository method to create profile
        //    return await _databaseRepository.CreateProfileAsync(userId, fullName, birthday, gender, city, postalCode);
        //}

        public async Task<bool> CreateProfileAsync(string fullName, DateTime birthday, string gender, string city, string postalCode)
        {
            try
            {
                return await _databaseRepository.CreateProfileAsync(fullName, birthday, gender, city, postalCode);
            }
            catch (Exception)
            {
                // Handle any exceptions that occur during profile creation
                return false; // Profile creation failed
            }
        }




        //public async Task<bool> DeleteProfileAsync(int userId)
        //{
        //    // Call repository method to delete profile
        //    return await _databaseRepository.DeleteProfileAsync(userId);
        //}


        public async Task<bool> DeleteProfileAsync()
        {
            return await _databaseRepository.DeleteProfileAsync();
        }

        public async Task<bool> AddLikeAsync(int likerUserId, int likedUserId)
        {
            // Call repository method to add a like
            return await _databaseRepository.AddLikeAsync(likerUserId, likedUserId);
        }

        public async Task<bool> CheckForMatchAsync(int userId1, int userId2)
        {
            // Call repository method to check for a match
            return await _databaseRepository.CheckForMatchAsync(userId1, userId2);
        }



        public async Task<bool> LogoutAsync()
        {
            try
            {
                // Implement your logout logic here
                // For example, clear session data or perform other necessary actions.

                // Clear session data or perform other necessary actions

                return true; // Return true if logout was successful
            }
            catch (Exception)
            {
                // Handle any exceptions during logout
                return false; // Return false if logout failed
            }
        }



        public async Task<bool?> LogInAsync(string username, string password)
        {
            // Retrieve password from database
            string storedPassword = await _databaseRepository.GetPasswordAsync(username);

            // Verify password
            return password == storedPassword;
        }









    }
}
