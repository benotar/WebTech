﻿using System.Text.Json.Serialization;

namespace Authors.Entities.Database;

public class Book
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime PublicAt { get; set; }
    public string Genre { get; set; }
    
    public int AuthorId { get; set; }
    [JsonIgnore] public Author Author { get; set; }
}