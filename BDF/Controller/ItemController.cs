using BDF.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDF.Controller
{
    public class ItemController : IController<Item>
    {
        Item entity = new Item();

        public void Create(Item entity)
        {
            entity.Create(entity);
        }

        public void Delete(int id)
        {
            entity.Delete(id);
        }
        public Item Find(string whereString)
        {
            return entity.Find(whereString);
        }
        public Item Find(int id)
        {
            return entity.Find(id);
        }

        public void Generate(int recordsAmount)
        {
            entity.Generate(recordsAmount);
        }

        public IEnumerable<Item> Read()
        {
            return entity.Read();
        }
        public void Update(string updateString)
        {
            entity.Update(updateString);
        }
    }
}
