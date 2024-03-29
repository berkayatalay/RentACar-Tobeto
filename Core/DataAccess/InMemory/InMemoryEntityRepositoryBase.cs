﻿using Core.Entities;

namespace Core.DataAccess.InMemory
{
    public abstract class InMemoryEntityRepositoryBase<TEntity, TEntityId> : IEntityRepository<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>, new()
    {
        protected readonly HashSet<TEntity> Entities = new();
        protected abstract TEntityId generateId();
        public TEntity Add(TEntity entity)
        {
            entity.Id = generateId();
            entity.CreatedAt = DateTime.UtcNow;
            Entities.Add(entity);
            return entity;
        }

        public TEntity Delete(TEntity entity, bool isSoftDelete = true)
        {
            entity.DeletedAt = DateTime.UtcNow; //Soft delete
            if (!isSoftDelete)
                Entities.Remove(entity); //Hard delete
            return entity;
        }

        public TEntity? Get(Func<TEntity, bool> predicate)
        {
            TEntity? entity = Entities.FirstOrDefault(predicate);
            //(e => e.Id.Equals(id) && e.DeletedAt.HasValue == false);

            return entity;
        }

        public IList<TEntity> GetList(Func<TEntity, bool>? predicate = null)
        {
            IEnumerable<TEntity> query = Entities;
            if (predicate is not null)
            {
                query = query.Where(predicate);
            }
            //Queryable ef tarafında sqle bağlandıktan sonra kullanılacak. kendimiz veri oluşturmayıp onları sırayla çekme aşamasında 
            //IQueryable<TEntity> query =_entities.AsQueryable();
            //if(predicate is not null)
            //{
            //    query=query.Where(predicate).AsQueryable();
            //}
            //IList<TEntity> entities = _entities.Where(e => e.DeletedAt.HasValue == false).ToList();
            return query.ToArray();
            //return query.ToImmutableArray();
            //Immutable : değiştilemez
        }

        public TEntity Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            return entity;
        }


    }
}
