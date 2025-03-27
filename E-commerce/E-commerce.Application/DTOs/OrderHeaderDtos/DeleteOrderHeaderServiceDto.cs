namespace E_commerce.Application.DTOs.OrderHeaderDtos
{
    public class DeleteOrderHeaderServiceDto
    {
        public Guid Id { get; set; } // Nullable to allow for bulk deletion
    }
}
