namespace OrderService.Domain.Model;

public class StartOrderStatusModel
{
    public StartOrderStatusModel(Guid EmployeeId, Guid OrderId)
    {
        this.EmployeeId = EmployeeId;
        this.OrderId = OrderId;
    }

    public Guid EmployeeId { get; set; }
    public Guid OrderId { get; set; }

    public void Deconstruct(out Guid EmployeeId, out Guid OrderId)
    {
        EmployeeId = this.EmployeeId;
        OrderId = this.OrderId;
    }
}