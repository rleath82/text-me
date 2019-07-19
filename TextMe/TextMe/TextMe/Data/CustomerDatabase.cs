using System.Collections.Generic;
using SQLite;
using TextMe.Models;
using System.Threading.Tasks;

namespace TextMe.Data
{
    public class CustomerDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public CustomerDatabase(string DBPath)
        {
            _database = new SQLiteAsyncConnection(DBPath);
            _database.CreateTableAsync<Customer>().Wait();
        }

        public Task<List<Customer>> GetCustomersAsync()
        {
            return _database.Table<Customer>().ToListAsync();
        }

        public Task<Customer> GetCustomerAsync(int id)
        {
            return _database.Table<Customer>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        public Task<int> AddCustomerAsync(Customer customer)
        {
            if (customer.ID != 0)
            {
                return _database.UpdateAsync(customer);
            }
            else
            {
                return _database.InsertAsync(customer);
            }
        }

        public Task<int> RemoveCustomerAsync(Customer customer)
        {
            return _database.DeleteAsync(customer);
        }               
    }
}
