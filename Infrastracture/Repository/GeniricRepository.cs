using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastracture.Repository
{
   public class GeniricRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _context;
        private DbSet<T> table = null;

        public GeniricRepository(DataContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }


        public void Delete(object id)
        {
            T exisiting = GetById(id);
            table.Remove(exisiting);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public T GetById(object id)
        {
            return table.Find(id);
        }

        public void Insert(T entity)
        {
            table.Add(entity);
        }

        public void Update(T entity)
        {
            table.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
