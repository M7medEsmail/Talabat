using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;
using Talabat.Domain.ISpecification;

namespace Talabat.Repository.Data
{
    public class SpacificationEvaluation<TEntity> where TEntity : BaseEntity
    {
        // inputQuery reperesnt Context.set<TEntity>()  
        public static IQueryable<TEntity> CreateQuery (IQueryable<TEntity> inputQuery,ISpecification<TEntity> spec)
        {
            var query = inputQuery; //Context.set<TEntity>() 


            if (spec.Criteria != null) {  // p => p.id = 10
               
                query = query.Where (spec.Criteria);
            }

            if (spec.IsPagination) { 
            
                query =query.Skip(spec.Skip).Take(spec.Take);
            
            }

            if (spec.OrderBy != null)
            {
                query = query.OrderBy (spec.OrderBy);
            }
            if(spec.OrderByDescending != null)
            {
                query = query.OrderByDescending (spec.OrderByDescending); 
            }
            // Context.Set<Product>().Include(T => T.ProductType)
            // Context.Set<Product>().Include(T => T.ProductType).Include(P => P.ProductBrand)
            query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;    
        }
    }
}
