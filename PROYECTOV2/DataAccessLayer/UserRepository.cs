using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccessLayer
{
    public class UsersRepository : Repository
    {
        private readonly DbContext _dbContext;

        public UsersRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Implementación del método Create<TEntity>
        public TEntity Create<TEntity>(TEntity toCreate) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(toCreate);
            _dbContext.SaveChanges();
            return toCreate;
        }

        // Implementación del método Delete<TEntity>
        public bool Delete<TEntity>(TEntity toDelete) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(toDelete);
            return _dbContext.SaveChanges() > 0;
        }

        // Implementación del método Update<TEntity>
        public bool Update<TEntity>(TEntity toUpdate) where TEntity : class
        {
            _dbContext.Entry(toUpdate).State = EntityState.Modified;
            return _dbContext.SaveChanges() > 0;
        }

        // Implementación del método Retrieve<TEntity>
        public TEntity Retrieve<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(criteria);
        }

        // Implementación del método Filter<TEntity>
        public List<TEntity> Filter<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class
        {
            return _dbContext.Set<TEntity>().Where(criteria).ToList();
        }

        // Implementación del método Dispose para liberar recursos
        public void Dispose()
        {
            if (_dbContext != null)
            {
                _dbContext.Dispose();
            }
        }

        // Métodos específicos para la entidad Users
        public Users CreateUser(Users user)
        {
            _dbContext.Set<Users>().Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        public Users RetrieveUser(int userId)
        {
            return _dbContext.Set<Users>().FirstOrDefault(u => u.UserID == userId);
        }

        public List<Users> GetAllUsers()
        {
            return _dbContext.Set<Users>().ToList();
        }

        public bool UpdateUser(Users user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            return _dbContext.SaveChanges() > 0;
        }

        public bool DeleteUser(int userId)
        {
            var user = _dbContext.Set<Users>().FirstOrDefault(u => u.UserID == userId);
            if (user == null) return false;

            _dbContext.Set<Users>().Remove(user);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
