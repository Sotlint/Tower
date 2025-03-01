using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Models.Abstractions;

public interface IEnemy
{
    Texture2D Sprite { get; }
    Guid Id { get;}
    
    Vector2 Position { get; }
    
    int Health { get; }

    public void TakeDamage(int damage);
    
    public void Update(Citadel citadel);

    public void Draw(SpriteBatch spriteBatch);
    
    public bool IsDefeated();
}