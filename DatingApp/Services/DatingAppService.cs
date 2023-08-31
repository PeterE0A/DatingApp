using System;
using System.Threading.Tasks;
using DatingApp.Repositories;
using System.Data.SqlClient;
using DatingApp.Services;
using DatingApp.Entities;

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


        private List<Profile> profiles = new List<Profile>();


        public Profile? GetProfile(int id)
        {
            Profile FindDBProfile = _databaseRepository.GetProfile(id);
            return FindDBProfile;
        }

        public List<Profile> GetProfiles()
        {
            var emps = _databaseRepository.GetProfiles();
            return emps;
        }


        public async Task<List<Profile>> GetProfilesByGender(string gender)
        {
            return await _databaseRepository.GetProfilesByGender(gender);
        }

        public async Task<bool> CheckForProfileAsync(string username)
        {
            return await _databaseRepository.CheckForProfileAsync(username);
          
        }


        public async Task<bool> CreateAccountAsync(string username, string password)
        {
            return await _databaseRepository.CreateAccountAsync(username, password);
        }

        public async Task<bool> DeleteAccountAsync(int userId)
        { 
            return await _databaseRepository.DeleteAccountAsync(userId);
        }

        public async Task<bool> CreateProfileAsync(string fullName, DateTime birthday, string gender, string city, string postalCode)
        {
            try
            {
                return await _databaseRepository.CreateProfileAsync(fullName, birthday, gender, city, postalCode);
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<bool> DeleteProfileAsync(int userId)
        {
            return await _databaseRepository.DeleteProfileAsync(userId);
        }


        public async Task<bool> AddLikeAsync(int likerUserId, int likedUserId)
        {
          
            return await _databaseRepository.AddLikeAsync(likerUserId, likedUserId);
        }

        public async Task<bool> CheckForMatchAsync(int userId1, int userId2)
        {
          
            return await _databaseRepository.CheckForMatchAsync(userId1, userId2);
        }


        public async Task<bool> LogoutAsync()
        {
            try
            {

                return true; 
            }
            catch (Exception)
            {
             
                return false; 
            }
        }


        public async Task<bool?> LogInAsync(string username, string password)
        {
            string storedPassword = await _databaseRepository.GetPasswordAsync(username);

            return password == storedPassword;
        }


        public async Task<int?> GetUserIdByUsername(string username)
        {
            return await _databaseRepository.GetUserIdByUsername(username);
        }


        public async Task<List<ChatMessage>> GetChatMessagesForUsers(int loggedInUserId, int otherUserId)
        {
            return await _databaseRepository.GetChatMessagesForUsers(loggedInUserId, otherUserId);
        }

        public async Task<int?> InsertChatMessage(int senderId, int receiverId, string messageContent)
        {
            return await _databaseRepository.InsertChatMessageAsync(senderId, receiverId, messageContent);
        }



    }
}
