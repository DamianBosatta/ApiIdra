using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EsMasBarato.Negocios.NegociosGenericos
{
    public interface INegocioGenerico<T> where T : class
    {
        List<T> GetAllByCondition(Expression<Func<T, bool>> where);

        T GetByCondition(Expression<Func<T, bool>> where);

        List<T> GetAll(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        T GetById(int id);

        void PartiallyInsert(T model);
        int Insert(T model);

        int Update(T model);

        int Delete(T model);

        int Save();
    }
}
