﻿using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        
          
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            //_db.Categories == dbSet
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter , string? includeProp = null, bool tracked = false)
        {


            IQueryable<T> query;
            if (tracked)
            {
                query = dbSet;
            }
            else { 
                query = dbSet.AsNoTracking();
            }
            if (!string.IsNullOrEmpty(includeProp))
            {
                foreach (var prop in includeProp
                   .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            query = query.Where(filter);

            return query.FirstOrDefault(filter);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProp = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null) { 
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProp)) {
                foreach (var prop in includeProp
                   .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
