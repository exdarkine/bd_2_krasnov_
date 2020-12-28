using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BDF.Model
{
    public class OrderInBill : BaseModel
    {
        public int id { get; set; }
        public int bill_id { get; set; }
        public int order_id { get; set; }
        public IEnumerable<OrderInBill> Read()
        {
            return Read("");
        }

        public void Delete(int id)
        {

            base.Delete("delete from orders_in_bills where id = " + id);
        }

        public OrderInBill Find(int id)
        {
            var list = Read("where id = " + id);
            return list.FirstOrDefault();
        }

        public OrderInBill Find(string where)
        {
            return Read(where).FirstOrDefault();
        }
        public void Update(string sqlUpdate)
        {
            sqlConnection.Open();

            sqlUpdate = "update orders_in_bills " + sqlUpdate;
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

        public void Create(OrderInBill entity)
        {
            string sqlInsert = "Insert into orders_in_bills(bill_id, order_id) VALUES(@bill_id, @order_id)";
            sqlConnection.Open();

            using var cmd = new NpgsqlCommand(sqlInsert, sqlConnection);
            cmd.Parameters.AddWithValue("bill_id", entity.bill_id);
            cmd.Parameters.AddWithValue("order_id", entity.order_id);
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

            string sqlGenerate = "insert into orders_in_bills(order_id, bill_id)" +
                $"(select order.id, bill.id" +
                $" from orders, bills)";

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

        private IEnumerable<OrderInBill> Read(string whereCondition)
        {

            sqlConnection.Open();

            string sqlSelect = "select id, bill_id, order_id from orderInBill ";


            using var cmd = new NpgsqlCommand(sqlSelect + whereCondition, sqlConnection);
            List<OrderInBill> list = new List<OrderInBill>();
            try
            {
                using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var entity = new OrderInBill();
                    entity.id = rdr.GetInt32(0);
                    entity.bill_id = rdr.GetInt32(1);
                    entity.order_id = rdr.GetInt32(2);
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
