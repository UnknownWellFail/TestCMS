using System.Collections.Generic;
using TestCMS.Models;

namespace TestCMS.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        void Create(T t);
        void Delete(int id);
        T Get(int id);
        IEnumerable<T> GetAll();
        void Update(T t);
        
    }
}