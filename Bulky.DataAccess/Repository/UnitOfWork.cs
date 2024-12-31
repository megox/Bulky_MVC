﻿using Bulky.DataAccess.Repository.IRepository;
using Bulky.DataAcess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }

        public ICompanyRepository Company { get; private set; }

        private readonly ApplicationDbContext _db;


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
            Company = new CompanyRepositpry(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
