using Microsoft.Data.Sqlite;

namespace PlaywrightTestDemo.Utilities
{
    public class DbHelperForSqlite
    {
        public static SqliteCommand CreateDbConnection()
        {
            var connectionString = "Data Source=E:\\EAApp_LocalMachine-6\\ProductAPI\\Product.db";

            var connection = new SqliteConnection(connectionString);
            connection.Open();

            return connection.CreateCommand();

        }

        public static Product GetProductByName(string productName)
        {
            var command = CreateDbConnection();

            command.CommandText = $"SELECT * FROM PRODUCTS Where Name = '{productName}'";

            using var reader = command.ExecuteReader();

            Product product = null;

            while (reader.Read())
               product = new Product(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4));

            return product;
        }

        public static List<Product> GetProducts()
        {
            var command = CreateDbConnection();

            command.CommandText = $"SELECT * FROM PRODUCTS";

            using var reader = command.ExecuteReader();

            List<Product> productsList = [];
            int row = 0;

            while (reader.Read())
            {
                productsList.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4)));
                row++;
            }

            return productsList;
        }

        public static void DeleteProductByName(string productName)
        {
            var command = CreateDbConnection();

            command.CommandText = $"DELETE FROM Products Where Name = '{productName}'";

            command.ExecuteNonQuery();
        }

       

    }
}
