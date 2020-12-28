using System;
using System.Collections.Generic;
using System.Text;

namespace BDF.Controller
{
    public interface IController<T>
    {
        public IEnumerable<T> Read();
        public void Delete(int id);
        public void Create(T entity);
        public void Update(string updateString);
        public T Find(int id);
        public void Generate(int recordsAmount);
    }
}
