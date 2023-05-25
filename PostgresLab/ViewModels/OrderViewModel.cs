namespace PostgresLab.ViewModels;

public class OrderViewModel
{
    public int OrderId { get; set; }

    public int? ClientId { get; set; }

    public DateOnly? OrderDate { get; set; }

    public double? SumPrice { get; set; }

    public virtual Client? Client { get; set; }
    
    public OrderInfo? OrderInfo { get; set; }
}