using System;
using Microsoft.Xna.Framework;

namespace Tower.Src.Models.Abstractions;

public interface IEnemy
{
    Guid Id { get;}
    
    Vector2 Position { get; }
    
    int Health { get; }

    public void TakeDamage(int damage);
    
    public void Update(Citadel citadel);
    
    public bool IsDefeated();
}