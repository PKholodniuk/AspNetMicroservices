using Dapper;
using Discount.API.Enteties;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("Insert into Coupon (ProductName, Description, Amount) Values (@ProductName, @Description, @Amount)",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("Delete Coupon where ProductName = @ProductName",
                new { ProductName = productName });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>("" +
                "Select * from Coupon where ProductName = @ProductName", new { ProductName = productName });
            if (coupon == null)
            {
                return new Coupon { ProductName = "No discount", Amount = 0, Description = "No Discount desc" };
            }
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var affected = await connection.ExecuteAsync("Update Coupon set ProductName = @ProductName, Description = @Description, Amount = @Amount, Id = @Id",
                new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });
            if (affected == 0)
            {
                return false;
            }
            return true;
        }
    }
}
