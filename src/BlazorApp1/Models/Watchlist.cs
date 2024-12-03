using System;
using System.Collections.Generic;

namespace BlazorApp1.Models;

public partial class Watchlist
{
    public int WatchlistId { get; set; }

    public int? UserId { get; set; }

    public int? MovieId { get; set; }

    public DateTime? AddedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual User? User { get; set; }
}
