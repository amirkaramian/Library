using LibraryApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace LibraryApi.Repositories
{
    public class LibararyDbContext : DbContext
    {
        public LibararyDbContext(DbContextOptions<LibararyDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Author> Authors { get; set; }

        public virtual async Task<List<Category>> GetCategory()
        {
            await using var connection = this.Database.GetDbConnection();

            var command = new SqlCommand
            {
                Connection = new SqlConnection(connection.ConnectionString),
                CommandType = CommandType.StoredProcedure,
                CommandText = "dbo.GetCategory",
            };

            command.Connection.Open();
            var reader = await command.ExecuteReaderAsync();

            return await MapData(reader);
        }

        private static async Task<List<Category>> MapData(SqlDataReader reader)
        {
            var item = new List<Category>();
            while (await reader.ReadAsync())
            {
                var cat = new Category()
                {
                    Id = await reader.GetFieldValueAsync<int>(0),
                    CategoryName = await reader.GetFieldValueAsync<string>(1),
                    CreateAt = await reader.GetFieldValueAsync<DateTime>(2),
                    EditAt = reader.IsDBNull(3) ? (DateTime?) null : (DateTime?) reader.GetDateTime(3),
                };

                item.Add(cat);
            }

            return item;
        }
    }
}