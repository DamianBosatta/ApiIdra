using EsMasBarato.Negocios.Contexto;
using System.Linq.Expressions;


namespace EsMasBarato.Negocios.NegociosGenericos
{
   public class NegocioGenerico<T> : BLLContext, INegocioGenerico<T> where T : class
    {
        

        public NegocioGenerico()
        {
           
        }

        public int Delete(T model)
        {
            Context.Set<T>().Remove(model);
            return Save();
        }

        public List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            var query = Context.Set<T>().AsQueryable();

            query = PrepareQuery(query, predicate, orderBy);

            return query.ToList();
        }

        public List<T> GetAllByCondition(Expression<Func<T, bool>> where)
        {
            return Context.Set<T>().Where(where).ToList();
        }

        public T GetByCondition(Expression<Func<T, bool>> where)
        {
            return Context.Set<T>().Where(where).FirstOrDefault()!;
        }

        public T GetById(int id)
        {
            var model = Context.Set<T>().Find(id);
            if (model == null)
                throw new Exception("Objeto no encontrado en la BD");
            return model;
        }

        public int Insert(T model)
        {
            Context.Add(model);
            return Save();
        }

        public void PartiallyInsert(T model)
        {
            Context.Add(model);
        }

        public int Save()
        {
            return Context.SaveChanges();
        }

        public int Update(T model)
        {
            Context.Update(model);
            return Save();
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
    }
}
