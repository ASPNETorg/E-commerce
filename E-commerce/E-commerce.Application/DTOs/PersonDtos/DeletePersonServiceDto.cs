namespace E_commerce.ApplicationServices.Dtos.PersonDtos;

public class DeletePersonServiceDto
{
    public Guid Id { get; set; } // Nullable to allow for bulk deletion
}