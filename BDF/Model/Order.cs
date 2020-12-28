using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDF.Model
{
    public class Order : BaseModel
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int item_id { get; set; }
        public int quantity { get; set; }
        public int table_id { get; set; }

        public IEnumerable<Order> Read()
        {
            return Read("");
        }

        public void Delete(int id)
        {

            base.Delete("delete from order where id = " + id);
        }

        public Order Find(int id)
        {
            var list = Read("where id = " + id);
            return list.FirstOrDefault();
        }

        public Order Find(string where)
        {
            return Read(where).FirstOrDefault();
        }
        public void Update(string sqlUpdate)
        {
            sqlConnection.Open();

            sqlUpdate = "update orders " + sqlUpdate;
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

        public void Create(Order entity)
        {
            string sqlInsert = "Insert into orders(date, item_id, quantity, table_id) VALUES(@date, @item_id, @quantity, @table_id)";
            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("date", entity.date);
            cmd.Parameters.AddWithValue("item_id", entity.item_id);
            cmd.Parameters.AddWithValue("quantity", entity.quantity);
            cmd.Parameters.AddWithValue("table_id", entity.table_id);
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
        }

        public void Generate(int recordsAmount)
        {

            string sqlGenerate = "insert into orders(date, item_id, quantity, table_id)" +
                $"(select {sqlRandomDate}, {sqlRandomInteger} , {sqlRandomInteger}, {sqlRandomInteger}" +
                $" from generate_series(1, 1000000)  limit({recordsAmount}))";

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

        private IEnumerable<Order> Read(string whereCondition)
        {

            sqlConnection.Open();

            string sqlSelect = "select id, date, item_id, quantity, table_id from orders ";


            using var cmd = new NpgsqlCommand(sqlSelect + whereCondition, sqlConnection);
            List<Order> list = new List<Order>();
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var entity = new Order();
                    entity.id = rdr.GetInt32(0);
                    entity.date = (DateTime)rdr.GetDate(1);
                    entity.item_id = rdr.GetInt32(2);
                    entity.quantity = rdr.GetInt32(3);
                    entity.table_id = rdr.GetInt32(4);
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
