using DatingApp.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;

public class DatabaseService : IDatabaseService
{
    private readonly string _connectionString;

    public DatabaseService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public bool CreateAccount(string username, string passwordHash)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("CreateAccount", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                connection.Open();
                int result = command.ExecuteNonQuery();

                return result > 0;
            }
        }
    }

    public bool DeleteAccount(int userId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("DeleteAccount", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                int result = command.ExecuteNonQuery();

                return result > 0;
            }
        }
    }


    public bool CreateProfile(int userId, string fullName, DateTime birthday, string gender, string city, string postalCode)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("CreateProfile", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userId);
                command.Parameters.AddWithValue("@FullName", fullName);
                command.Parameters.AddWithValue("@Birthday", birthday);
                command.Parameters.AddWithValue("@Gender", gender);
                command.Parameters.AddWithValue("@City", city);
                command.Parameters.AddWithValue("@PostalCode", postalCode);

                connection.Open();
                int result = command.ExecuteNonQuery();

                return result > 0;
            }
        }
    }


    public bool DeleteProfile(int userId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("DeleteProfile", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                int result = command.ExecuteNonQuery();

                return result > 0;
            }
        }
    }


    public bool Login(string username, string passwordHash)
    {
        // Implement logic to perform login
        return false;
    }

    public bool Logout()
    {
        // Implement logic to perform logout
        return false;
    }

    public bool AddLike(int likerUserId, int likedUserId)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            using (var command = new SqlCommand("AddLike", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@LikerUserId", likerUserId);
                command.Parameters.AddWithValue("@LikedUserId", likedUserId);

                connection.Open();
                int result = command.ExecuteNonQuery();

                return result > 0;
            }
        }
    }


    public bool CheckForMatch(int userId1, int userId2)
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
                command.ExecuteNonQuery();

                return (bool)command.Parameters["@Match"].Value;
            }
        }
    }



    // Implement the methods from IDatabaseService using SqlConnection and SqlCommand
    // Example: CreateAccount, DeleteAccount, CreateProfile, DeleteProfile, Login, Logout, AddLike, CheckForMatch
}
