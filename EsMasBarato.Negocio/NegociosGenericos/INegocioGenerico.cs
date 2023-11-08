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
        Task<List<T>> GetAllByConditionAsync(Expression<Func<T, bool>> where);

        Task<T> GetByConditionAsync(Expression<Func<T, bool>> where);

        Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        Task<T> GetByIdAsync(int id);

        //Task PartiallyInsertAsync(T model);

        Task<int> InsertAsync(T model);

        Task<int> UpdateAsync(T model);

        Task<int> DeleteAsync(T model);

        Task<int> SaveAsync();
    }

}
