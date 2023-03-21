using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using TaskManagementAPI.Data.Repository.Intefaces;


namespace TaskManagementAPI.Data.Repository
{
    public class GenericRepository<Entity> : IGenericRepository<Entity> where Entity : class
    {
        private readonly TaskManagementDbContext _dbContext;
        public GenericRepository(TaskManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IQueryable<Entity> GetByCondition(Expression<Func<Entity, bool>> condition) => _dbContext.Set<Entity>().Where(condition);
        public IQueryable<Entity> GetByConditionWithIncludes(Expression<Func<Entity, bool>> expression, string? includeRelations = null)
        {
            var query = _dbContext.Set<Entity>().Where(expression);

            if (!string.IsNullOrEmpty(includeRelations))
            {
                var relations = includeRelations.Split(", ");

                foreach (var relation in relations)
                {
                    query = query.Include(relation);
                }
            }

            return query;
        }
        public IQueryable<Entity> GetAll() => _dbContext.Set<Entity>();
        public IQueryable<Entity> GetById(Expression<Func<Entity, bool>> condition) => _dbContext.Set<Entity>().Where(condition);
        public async Task CreateAsync(Entity entity) => await _dbContext.Set<Entity>().AddAsync(entity);
        public async Task CreateRangeAsync(List<Entity> entities) => await _dbContext.Set<Entity>().AddRangeAsync(entities);
        public void Create(Entity entity) => _dbContext.Set<Entity>().Add(entity);
        public void CreateRange(List<Entity> entities) => _dbContext.Set<Entity>().AddRange(entities);
        public void Delete(Entity entity) => _dbContext.Set<Entity>().Remove(entity);
        public void DeleteRange(List<Entity> entities) => _dbContext.Set<Entity>().RemoveRange(entities);
        public void Update(Entity entity) => _dbContext.Set<Entity>().Update(entity);
        public void UpdateRange(List<Entity> entities) => _dbContext.Set<Entity>().UpdateRange(entities);
    }
}
