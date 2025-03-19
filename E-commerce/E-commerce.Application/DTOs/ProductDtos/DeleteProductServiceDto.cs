namespace E_commerce.ApplicationServices.Dtos.ProductDtos;

public class DeleteProductServiceDto
{
    public Guid Id { get; set; } // Nullable to allow for bulk deletion
}