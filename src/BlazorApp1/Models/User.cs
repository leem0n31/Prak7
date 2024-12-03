using System;
using System.Collections.Generic;

namespace BlazorApp1.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<Movie> Movies { get; } = new List<Movie>();

    public virtual ICollection<Recommendation> Recommendations { get; } = new List<Recommendation>();

    public virtual ICollection<Review> ReviewDeletedByUsers { get; } = new List<Review>();

    public virtual ICollection<Review> ReviewUsers { get; } = new List<Review>();

    public virtual ICollection<Watchlist> Watchlists { get; } = new List<Watchlist>();
}
