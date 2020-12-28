using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDF.Model
{
    public abstract class BaseModel
    {
        public readonly string sqlRandomString = "chr(trunc(65 + random() * 50)::int) || chr(trunc(65 + random() * 25)::int) || chr(trunc(65 + random() * 25)::int) || chr(trunc(65 + random() * 25)::int)";
        public readonly string sqlRandomInteger = "trunc(random()*1000)::int";
        public readonly string sqlRandomDate = "timestamp '2014-01-10 20:00:00' + random() * (timestamp '2014-01-20 20:00:00' - timestamp '2014-01-10 10:00:00')";
        public readonly string sqlRandomBoolean = "trunc(random()*2)::int::boolean";

        protected NpgsqlConnection sqlConnection = new NpgsqlConnection("Host = localhost; Username=postgres;Password=p;Database=BDF");
        protected void Delete(string sqlDelete)
        {


            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlDelete, sqlConnection);

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


        protected void Update(string sqlUpdate)
        {
            sqlConnection.Open();

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

        protected void Generate(string sqlGenerate)
        {

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
    }
}
