using System;
using System.IO;
using RentACar.Entities;
using SQLite;

namespace RentACar.Database
{

    public static class Database
    {
        private static readonly string dbFileName = "database.db3";

        public static SQLiteConnection GetConnection()
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), dbFileName);
                SQLiteConnection conn = new SQLiteConnection(dpPath);

                // create tables if not exists
                conn.CreateTable<OrderEntity>();

                return conn;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR SQLITE - GetConnection: " + ex.Message);
                return null;
            }
        }
    }
}
