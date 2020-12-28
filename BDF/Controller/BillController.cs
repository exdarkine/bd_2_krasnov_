using BDF.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BDF.Controller
{
    public class BillController : IController<Bill>
    {
        Bill entity = new Bill();

        public void Create(Bill entity)
        {
            entity.Create(entity);
        }

        public void Delete(int id)
        {
            entity.Delete(id);
        }

        public Bill Find(string whereString)
        {
            return entity.Find(whereString);
        }
        public Bill Find(int id)
        {
            return entity.Find(id);
        }
        public void Generate(int recordsAmount)
        {
            entity.Generate(recordsAmount);
        }

        public IEnumerable<Bill> Read()
        {
            return entity.Read();
        }
        public void Update(string updateString)
        {
            entity.Update(updateString);
        }
        public void Update(Bill entity)
        {
            throw new NotImplementedException();
        }
    }
}
