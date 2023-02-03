
namespace CacheService.Repository;

/// <summary>
/// Cache Repository
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICacheService<T> where T : CacheBaseEntity
{
    /// <summary>
    /// get entity by Id
    /// </summary>
    /// <param name="Id">string Id</param>
    /// <returns></returns>
    T GetById(string Id);

    /// <summary>
    /// Get All
    /// </summary>
    /// <returns>List of entities</returns>
    List<T> GetAll();

    /// <summary>
    /// Insert entity
    /// </summary>
    /// <param name="value">entity</param>
    /// <param name="expirationTime">expire DateTime (when this time raised remove record)</param>
    /// <returns>entity added</returns>
    T Add(T value, DateTimeOffset expirationTime);

    /// <summary>
    /// Update Entity
    /// </summary>
    /// <param name="Id">entity Id</param>
    /// <param name="value">new Entity</param>
    /// <param name="expirationTime">new expire DateTime (when this time raised remove record)</param>
    /// <returns></returns>
    T Update(string Id, T value, DateTimeOffset expirationTime);

    /// <summary>
    /// Delete entity By Id
    /// </summary>
    /// <param name="Id">string Id</param>
    /// <returns>IsSuccess</returns>
    bool Delete(string Id);

    /// <summary>
    /// where condition
    /// </summary>
    /// <returns></returns>
    List<KeyValuePair<string, object>> FilteryBy(Expression<Func<KeyValuePair<string, object>, bool>> predicate);
}

