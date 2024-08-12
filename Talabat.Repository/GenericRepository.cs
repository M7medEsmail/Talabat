using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;
using Talabat.Domain.IRepositories;
using Talabat.Domain.ISpecification;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly TalabatContext Context ;
        public GenericRepository(TalabatContext context )
        {
            Context = context;
        }

        public async Task CreateAsync(T entity)
        {
           await Context.Set<T>().AddAsync( entity );
        }

        public void DeleteAsync(T entity)
        {
            Context.Set<T>().Remove( entity );
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
            //{
            //    if (typeof(T) == typeof(Product)  // Mosken solved by specisificaiton Design Pattern  that use to build this query Context.Set<Product>().Include(T => T.ProductType).Include(P => P.ProductBrand).ToListAsync();!
            //       return await Context.Set<Product>().Include(T => T.ProductType).Include(P => P.ProductBrand).ToListAsync();

            //    return await Context.Set<T>().ToListAsync();
            //}

            => await Context.Set<T>().ToListAsync();

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)
        { 
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)

            => await Context.Set<T>().FindAsync(id);

        public async Task<T> GetByIdWithSpecAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public void UpdateAsync(T entity)
            =>  Context.Set<T>().Update(entity); // Update things which change 
            // Context.Entity(entity).State = EntityState.Modified; // Update All Which Change and NotChange

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {

            return SpacificationEvaluation<T>.CreateQuery(Context.Set<T>(), spec);
        }

    }
}
