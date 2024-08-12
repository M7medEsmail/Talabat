using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;

namespace Talabat.Domain.ISpecification
{
    public interface ISpecification<T> where T : BaseEntity
    {
        // Criteria , Includes Known as signuture Properity
        public Expression<Func<T, bool>> Criteria { get; set; } //Represent as p => p.Id = id
        public List<Expression<Func<T, object>>> Includes { get ; set; } // Represent as Include(T => T.ProductType).Include(P => P.ProductBrand)
   
        public Expression<Func<T , object>> OrderBy { get; set; }
        public Expression<Func<T , object>> OrderByDescending { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public bool IsPagination { get; set; }
    }
}
