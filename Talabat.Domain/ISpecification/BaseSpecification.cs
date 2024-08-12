using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;

namespace Talabat.Domain.ISpecification
{
    public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
    {
        // Criteria , Includes Known as Automatic Properity

        public Expression<Func<T, bool>> Criteria { get ; set; }
        public List<Expression<Func<T, object>>> Includes { get; set ; }
        public Expression<Func<T, object>> OrderBy { get ; set; }
        public Expression<Func<T, object>> OrderByDescending { get; set; }
        public int Take {  get; set; }
        public int Skip {  get; set; }
        public bool IsPagination { get ; set; }

        public BaseSpecification()
        {
            Includes = new List<Expression<Func<T, object>>>() ;  
        }

        public BaseSpecification( Expression<Func<T, bool>> criteria )  // P => P.id =10 
        {
            Criteria = criteria;
            Includes = new List<Expression<Func<T, object>>>();
          
        }

        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        public void AddOrderByDescending(Expression<Func<T, object>> orderByDescending)
        {
            OrderByDescending = orderByDescending;
        }

        public void ApplyPagination( int skip , int take)
        {
            IsPagination = true;
            Skip = skip;
            Take = take;
        }
    }   
}
