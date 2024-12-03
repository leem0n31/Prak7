using System;
using System.Collections.Generic;

namespace BlazorApp1.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? UserId { get; set; }

    public int? MovieId { get; set; }

    public int? Rating { get; set; }

    public string? ReviewText { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public int? DeletedByUserId { get; set; }

    public virtual User? DeletedByUser { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual User? User { get; set; }
}
