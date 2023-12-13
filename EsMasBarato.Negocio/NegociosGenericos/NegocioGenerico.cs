using EsMasBarato.Negocios.Contexto;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Linq.Expressions;


namespace EsMasBarato.Negocios.NegociosGenericos
{
    public class NegocioGenerico<T> : BLLContext, INegocioGenerico<T> where T : class
    {
        protected readonly ILogger _logger;
      

        public NegocioGenerico(ILogger logger)
        {
           _logger = logger;
        }

     

        public async Task<int> DeleteAsync(T model)
        {
            try { 

            Context.Set<T>().Remove(model);
            return await SaveAsync();
           
            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioGenerico" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En DeleteAsync(NegocioGenerico)");
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            try { 

            return await Context.Set<T>().ToListAsync();

            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioGenerico" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetAllAsync(NegocioGenerico)");
            }
        }

        public async Task<List<T>> GetAllAsync(
            Expression<Func<T, bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            try { 
            var query = Context.Set<T>().AsQueryable();

            query = PrepareQuery(query, predicate, orderBy);

            return await query.ToListAsync();
            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioGenerico" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetAllAsync(Expresion Predicate)(NegocioGenerico)");
            }
        }

        public async Task<List<T>> GetAllByConditionAsync(Expression<Func<T, bool>> where)
        {
            return await Context.Set<T>().Where(where).ToListAsync();
        }

        public async Task<T> GetByConditionAsync(Expression<Func<T, bool>> where)
        {
            try
            {

                return await Context.Set<T>().Where(where).FirstOrDefaultAsync()!;
            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioGenerico" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetByConditionAsync(Expresion Predicate)(NegocioGenerico)");
            }

        }

        public async Task<T> GetByIdAsync(int id)
        {
            try 
            { 

            return await Context.Set<T>().FindAsync(id);

            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioGenerico" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En GetByIdAsync(Expresion Predicate)(NegocioGenerico)");
            }

        }

        public async Task<int> InsertAsync(T model)
        {
            try 
            { 
            
            Context.Add(model);
            return await SaveAsync();

            }
              catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioGenerico" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En InsertAsync(Expresion Predicate)(NegocioGenerico)");
            }
        }

     
        public async Task<int> UpdateAsync(T model)
        {
            try 
            {
                
            Context.Update(model);
            return await SaveAsync();

            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioGenerico" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En UpdateAsync(Expresion Predicate)(NegocioGenerico)");
            }
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
            try 
            { 

            return await Context.SaveChangesAsync();
             
            }
            catch (Exception)
            {
                _logger.Error("ATENCION!! Capturamos Error En NegocioGenerico" +
                      " A Continuacion Encontraras Mas Informacion -> ->");
                throw new InvalidOperationException("Excepcion En SaveAsync(Expresion Predicate)(NegocioGenerico)");
            }

         }
    }

}

