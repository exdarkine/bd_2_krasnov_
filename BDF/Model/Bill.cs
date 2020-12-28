using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDF.Model
{
    public class Bill : BaseModel
    {
        public int id { get; set; }
        public int tips { get; set; }
        public DateTime time { get; set; }

        public IEnumerable<Bill> Read()
        {
            return Read("");
        }

        public void Delete(int id)
        {

            base.Delete("delete from bills where id = " + id);
        }

        public Bill Find(int id)
        {
            var list = Read("where id = " + id);
            return list.FirstOrDefault();
        }
        public Bill Find(string where)
        {
            return Read(where).FirstOrDefault();
        }
        public void Update(string sqlUpdate)
        {
            sqlConnection.Open();

            sqlUpdate = "update bills " + sqlUpdate;
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

        public void Create(Bill entity)
        {
            string sqlInsert = "Insert into bills(tips, time) VALUES(@tips, @time)";
            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("tips", entity.tips);
            cmd.Parameters.AddWithValue("time", entity.time);
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

            string sqlGenerate = "insert into bills(tips, time)" +
                $"(select {sqlRandomInteger}, {sqlRandomInteger} " +
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

        private IEnumerable<Bill> Read(string whereCondition)
        {

            sqlConnection.Open();

            string sqlSelect = "select id, tips, time from bills ";


            using var cmd = new NpgsqlCommand(sqlSelect + whereCondition, sqlConnection);
            List<Bill> list = new List<Bill>();
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var entity = new Bill();
                    entity.id = rdr.GetInt32(0);
                    entity.tips = rdr.GetInt32(1);
                    entity.time = (DateTime)rdr.GetDate(2);
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
