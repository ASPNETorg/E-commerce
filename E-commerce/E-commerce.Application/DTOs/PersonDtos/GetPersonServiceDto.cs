﻿namespace E_commerce.ApplicationServices.Dtos.PersonDtos;

public class GetPersonServiceDto
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }
    public string Role { get; set; }
}