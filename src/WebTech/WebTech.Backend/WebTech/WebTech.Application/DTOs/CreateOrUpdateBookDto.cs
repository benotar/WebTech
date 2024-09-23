﻿namespace WebTech.Application.DTOs;

public class CreateOrUpdateBookDto
{
    public string AuthorFirstName { get; set; }
    
    public string AuthorLastName { get; set; }
    
    public string Title { get; set; }

    public string Genre { get; set; }

    public int PublicationYear { get; set; }
}