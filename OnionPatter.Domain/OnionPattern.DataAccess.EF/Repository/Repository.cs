﻿using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using OnionPattern.Domain.Entities;
using OnionPattern.Domain.Repository;

namespace OnionPattern.DataAccess.EF.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : VideoGameEntity
    {
        private readonly IDbContext context;
        private readonly DbSet<TEntity> dataSet;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context"></param>
        public Repository(IDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException($"{nameof(context)} cannot be null.");
            dataSet = context.Set<TEntity>();
        }

        #region Queries

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public TEntity Single(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        #endregion Queries

        #region Gets

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                dataSet.AsNoTracking();
                var data = dataSet.Where(f => true);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting records: {ex.Message}", ex);
            }
        }

        #endregion Gets

        public void Create(TEntity entity)
        {
            dataSet.Add(entity);
            SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            dataSet.Remove(entity);
            SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (!dataSet.Local.Contains(entity)) { dataSet.Attach(entity); }
            context.Entry(entity).State = EntityState.Modified;
            SaveChanges();
        }

        private void SaveChanges()
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new Exception($"Concurrency Error: {ex.Message}", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new Exception($"Database Update Error: {ex.Message}", ex);
            }
            catch (DbException ex)
            {
                throw new Exception($"Entity Validation Errors: {ex.Message}", ex);
            }
        }
    }
}