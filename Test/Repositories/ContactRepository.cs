using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Test.Models;

namespace Test.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly string _connectionString;

    public ContactRepository(string connectionString)
    {
        _connectionString = connectionString;
    }


        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Contact>("GetAllContacts", commandType: CommandType.StoredProcedure);
        }

        public async Task<Contact> GetContactByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Contact>("GetContactById", new { Id = id }, commandType: CommandType.StoredProcedure);
        }

        public async Task AddContactAsync(Contact contact)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new
            {
                contact.Name,
                contact.Email,
                contact.Phone,
                contact.Address,
                contact.State,
                contact.City
            };
            await connection.ExecuteAsync("AddContact", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync("UpdateContact", contact, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteContactAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.ExecuteAsync("DeleteContact", new { Id = id }, commandType: CommandType.StoredProcedure);
        }
    }

}
