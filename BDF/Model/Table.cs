using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDF.Model
{
    public class Table : BaseModel
    {
        public int id { get; set; }
        public int chair_quantity { get; set; }
        public IEnumerable<Table> Read()
        {
            return Read("");
        }

        public void Delete(int id)
        {

            base.Delete("delete from tables where id = " + id);
        }

        public Table Find(int id)
        {
            var list = Read("where id = " + id);
            return list.FirstOrDefault();
        }

        public Table Find(string where)
        {
            return Read(where).FirstOrDefault();
        }
        public void Update(string sqlUpdate)
        {
            sqlConnection.Open();

            sqlUpdate = "update tables " + sqlUpdate;
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

        public void Create(Table entity)
        {
            string sqlInsert = "Insert into table(chair_quantity) VALUES(@chair_quantity)";
            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("chair_quantity", entity.chair_quantity);
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

            string sqlGenerate = "insert into tables(chair_quantity)" +
                $"(select {sqlRandomInteger}" +
                $" from  generate_series(1, 1000000)  limit({recordsAmount}))";

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

        private IEnumerable<Table> Read(string whereCondition)
        {

            sqlConnection.Open();

            string sqlSelect = "select id, bill_id, order_id from orderInBill ";


            using var cmd = new NpgsqlCommand(sqlSelect + whereCondition, sqlConnection);
            List<Table> list = new List<Table>();
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var entity = new Table();
                    entity.id = rdr.GetInt32(0);
                    entity.chair_quantity = rdr.GetInt32(1);
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
