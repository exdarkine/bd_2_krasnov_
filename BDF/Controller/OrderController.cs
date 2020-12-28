using BDF.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDF.Controller
{
    class OrderController : IController<Order>
    {
        Order entity = new Order();

        public void Create(Order entity)
        {
            entity.Create(entity);
        }

        public void Delete(int id)
        {
            entity.Delete(id);
        }
        public Order Find(string whereString)
        {
            return entity.Find(whereString);
        }
        public Order Find(int id)
        {
            return entity.Find(id);
        }

        public void Generate(int recordsAmount)
        {
            entity.Generate(recordsAmount);
        }

        public IEnumerable<Order> Read()
        {
            return entity.Read();
        }
        public void Update(string updateString)
        {
            entity.Update(updateString);
        }
    }
}
