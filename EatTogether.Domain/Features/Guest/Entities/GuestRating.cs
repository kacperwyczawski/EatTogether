﻿using EatTogether.Domain.Shared.ValueObjects;

namespace EatTogether.Domain.Features.Guest.Entities;

public class GuestRating : Entity
{
    public Rating Rating { get; }
    
    public GuestRating(Guid id, Rating rating) : base(id)
    {
        Rating = rating;
    }
}