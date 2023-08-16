﻿using System;
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

        public async Task<bool> CreateAccountAsync(string username, string passwordHash)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("CreateAccount", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@PasswordHash", passwordHash);

                    connection.Open();
                    int result = await command.ExecuteNonQueryAsync();

                    return result > 0;
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

        public async Task<bool> CreateProfileAsync(int userId, string fullName, DateTime birthday, string gender, string city, string postalCode)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("CreateProfile", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserID", userId);
                    command.Parameters.AddWithValue("@FullName", fullName);
                    command.Parameters.AddWithValue("@Birthday", birthday);
                    command.Parameters.AddWithValue("@Gender", gender);
                    command.Parameters.AddWithValue("@City", city);
                    command.Parameters.AddWithValue("@PostalCode", postalCode);

                    connection.Open();
                    int result = await command.ExecuteNonQueryAsync();

                    return result > 0;
                }
            }
        }

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