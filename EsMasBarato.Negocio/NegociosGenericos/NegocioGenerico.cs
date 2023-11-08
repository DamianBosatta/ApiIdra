using EsMasBarato.Negocios.Contexto;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace EsMasBarato.Negocios.NegociosGenericos
{
    public class NegocioGenerico<T> : BLLContext, INegocioGenerico<T> where T : class
    {
        public NegocioGenerico()
        {
        }

        public async Task<int> DeleteAsync(T model)
        {
            Context.Set<T>().Remove(model);
            return await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await Context.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var query = Context.Set<T>().AsQueryable();

            query = PrepareQuery(query, predicate, orderBy);

            return await query.ToListAsync();
        }

        public async Task<List<T>> GetAllByConditionAsync(Expression<Func<T, bool>> where)
        {
            return await Context.Set<T>().Where(where).ToListAsync();
        }

        public async Task<T> GetByConditionAsync(Expression<Func<T, bool>> where)
        {
            return await Context.Set<T>().Where(where).FirstOrDefaultAsync()!;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var model = await Context.Set<T>().FindAsync(id);
            if (model == null)
                throw new Exception("Objeto no encontrado en la BD");
            return model;
        }

        public async Task<int> InsertAsync(T model)
        {
            Context.Add(model);
            return await SaveAsync();
        }

        //public void PartiallyInsert(T model)
        //{
        //    Context.Add(model);
        //}

        public async Task<int> UpdateAsync(T model)
        {
            Context.Update(model);
            return await SaveAsync();
        }

        protected IQueryable<T> PrepareQuery(
            IQueryable<T> query,
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            if (predicate != null)
                query = query.Where(predicate);

            if (orderBy != null)
                query = orderBy(query);

            return query;
        }

        public async Task<int> SaveAsync()
        {
            return await Context.SaveChangesAsync();
        }
    }

}

