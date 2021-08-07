using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EasyNoSQL
{
    public interface ICollection<T>
    {
        List<T> Find(Expression<Func<T, bool>> predicate);
        bool IsExists(Expression<Func<T, bool>> predicate);
        void Insert(T model);
        void Update(T model);
        void Delete(string id);
    }
}
