using BDF.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDF.Controller
{
    public class TableController : IController<Table>
    {
        Table entity = new Table();

        public void Create(Table entity)
        {
            entity.Create(entity);
        }

        public void Delete(int id)
        {
            entity.Delete(id);
        }
        public Table Find(string whereString)
        {
            return entity.Find(whereString);
        }
        public Table Find(int id)
        {
            return entity.Find(id);
        }

        public void Generate(int recordsAmount)
        {
            entity.Generate(recordsAmount);
        }

        public IEnumerable<Table> Read()
        {
            return entity.Read();
        }
        public void Update(string updateString)
        {
            entity.Update(updateString);
        }
    }
}
