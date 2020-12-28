using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDF.Model
{
    public class Item : BaseModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int quantity {get;set;}

        public IEnumerable<Item> Read()
        {
            return Read("");
        }

        public void Delete(int id)
        {

            base.Delete("delete from items where id = " + id);
        }

        public Item Find(int id)
        {
            var list = Read("where id = " + id);
            return list.FirstOrDefault();
        }

        public Item Find(string where)
        {
            return Read(where).FirstOrDefault();
        }
        public void Update(string sqlUpdate)
        {
            sqlConnection.Open();

            sqlUpdate = "update items " + sqlUpdate;
            using var cmd = new NpgsqlCommand(sqlUpdate, sqlConnection);

            try
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public void Create(Item entity)
        {
            string sqlInsert = "Insert into items(name, price, quantity) VALUES(@name, @price, @quantity)";
            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("name", entity.name);
            cmd.Parameters.AddWithValue("price", entity.price);
            cmd.Parameters.AddWithValue("quantity", entity.quantity);
            cmd.Prepare();

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            finally
            {
                sqlConnection.Close();
            }
            Console.ReadLine();
        }

        public void Generate(int recordsAmount)
        {

            string sqlGenerate = "insert into items(name, price, quantity)" +
                $"(select {sqlRandomString}, {sqlRandomInteger} , {sqlRandomInteger}" +
                $" from generate_series(1, {recordsAmount}))";

            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlGenerate, sqlConnection);

            try
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private IEnumerable<Item> Read(string whereCondition)
        {

            sqlConnection.Open();

            string sqlSelect = "select id, name, price, quantity from items ";


            using var cmd = new NpgsqlCommand(sqlSelect + whereCondition, sqlConnection);
            List<Item> list = new List<Item>();
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var entity = new Item();
                    entity.id = rdr.GetInt32(0);
                    entity.name = rdr.GetString(1);
                    entity.price = rdr.GetInt32(2);
                    entity.quantity = rdr.GetInt32(3);
                    list.Add(entity);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
            finally
            {
                sqlConnection.Close();
            }

            return list;
        }
    }
}
