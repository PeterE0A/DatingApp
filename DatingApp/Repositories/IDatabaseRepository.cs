using System.Threading.Tasks;
using DatingApp.Entities;


namespace DatingApp.Repositories
{
    public interface IDatabaseRepository
    {
        //---------------------------------------------------------------
        Task<int?> GetUserIdByUsername(string username);
        //-----------------------------------------------------------
        Task<List<Profile>> GetProfilesByGender(string gender);
        List<Profile> GetProfiles();
        Profile GetProfile(int id);

        Task<bool> DeleteProfileAsync(int userId);


        Task<bool> CheckForProfileAsync(string username);

        Task<bool> CreateAccountAsync(string username, string password);
        Task<string> GetPasswordAsync(string username);
        Task<bool> DeleteAccountAsync(int userId);
        Task<bool> CreateProfileAsync(string fullName, DateTime birthday, string gender, string city, string postalCode);
        
        Task<bool> AddLikeAsync(int likerUserId, int likedUserId);
        Task<bool> CheckForMatchAsync(int userId1, int userId2);
    }
}
