namespace E_commerce.ApplicationServices.Dtos.ProductDtos;

public class GetProductServiceDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
    public Guid SellerId { get; set; }
}