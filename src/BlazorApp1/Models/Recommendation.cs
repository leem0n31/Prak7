using System;
using System.Collections.Generic;

namespace BlazorApp1.Models;

public partial class Recommendation
{
    public int RecommendationId { get; set; }

    public int? UserId { get; set; }

    public int? MovieId { get; set; }

    public virtual Movie? Movie { get; set; }

    public virtual User? User { get; set; }
}
