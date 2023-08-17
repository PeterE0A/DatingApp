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


        public async Task<bool> CreateAccountAsync(string username, string password)
        {
            // Call repository method to create account
            return await _databaseRepository.CreateAccountAsync(username, password);
        }

        public async Task<bool> DeleteAccountAsync(int userId)
        {
            // Call repository method to delete account
            return await _databaseRepository.DeleteAccountAsync(userId);
        }

        public async Task<bool> CreateProfileAsync(int userId, string fullName, DateTime birthday, string gender, string city, string postalCode)
        {
            // Call repository method to create profile
            return await _databaseRepository.CreateProfileAsync(userId, fullName, birthday, gender, city, postalCode);
        }

        public async Task<bool> DeleteProfileAsync(int userId)
        {
            // Call repository method to delete profile
            return await _databaseRepository.DeleteProfileAsync(userId);
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


        


        public async Task<bool?> LogInAsync(string username, string password)
        {
            // Retrieve password from database
            string storedPassword = await _databaseRepository.GetPasswordAsync(username);

            // Verify password
            return password == storedPassword;
        }










    }
}
