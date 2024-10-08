﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Domain.Entities;
using Talabat.Domain.IRepositories;
using Talabat.Repository.Data;

namespace Talabat.Repository
{
    public class UniteOfWork : IUniteOfWork
    {
        private readonly TalabatContext _context;
        private Hashtable _repositories;
        public UniteOfWork(TalabatContext context)
        {
            _context = context;
        }
        public async Task<int> Complete()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
          _context.Dispose();   
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable();
            var type = typeof(TEntity).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repository);    

            }
            return (IGenericRepository<TEntity>) _repositories[type];

            
        }
    }
}
