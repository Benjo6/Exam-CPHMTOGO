namespace OrderService.Domain.EntityHelper;

public interface EntityWithId<T>
{
    public T Id { get; set; }
}