using BDF.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDF.Controller
{
    public class OrderInBillController : IController<OrderInBill>
    {
        OrderInBill entity = new OrderInBill();

        public void Create(OrderInBill entity)
        {
            entity.Create(entity);
        }

        public void Delete(int id)
        {
            entity.Delete(id);
        }
        public OrderInBill Find(string whereString)
        {
            return entity.Find(whereString);
        }
        public OrderInBill Find(int id)
        {
            return entity.Find(id);
        }

        public void Generate(int recordsAmount)
        {
            entity.Generate(recordsAmount);
        }

        public IEnumerable<OrderInBill> Read()
        {
            return entity.Read();
        }
        public void Update(string updateString)
        {
            entity.Update(updateString);
        }
    }
}
