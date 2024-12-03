using System;
using System.Collections.Generic;

namespace BlazorApp1.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public DateTime? ReleaseDate { get; set; }

    public string? Genre { get; set; }

    public string? Description { get; set; }

    public int? CreatedByUserId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual User? CreatedByUser { get; set; }

    public virtual ICollection<Recommendation> Recommendations { get; } = new List<Recommendation>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();

    public virtual ICollection<Watchlist> Watchlists { get; } = new List<Watchlist>();
}
