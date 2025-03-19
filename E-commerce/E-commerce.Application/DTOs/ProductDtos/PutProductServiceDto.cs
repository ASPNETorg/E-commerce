namespace E_commerce.ApplicationServices.Dtos.ProductDtos;

public class PutProductServiceDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal Stock {get; set; }
    public Guid CategoryId { get; set; }
    public Guid SellerId { get; set; }
}