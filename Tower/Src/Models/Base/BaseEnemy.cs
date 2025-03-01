using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Models.Abstractions;

namespace Tower.Models.Base;

public abstract class BaseEnemy : IEnemy
{
    public Guid Id { get; init; }
    public Texture2D Sprite { get; init; }
    public int CurrentWaypointIndex { get; set; }
    public Vector2 Position { get; set; }

    public int Health { get; set; }
    public int Speed { get; set; }
    public int Reward { get; set; }

    private EnemyTypeEnum Type { get; set; }
    
    protected BaseEnemy(int health, int speed, int reward, EnemyTypeEnum type, Texture2D sprite)
    {
        Id = Guid.NewGuid();
        CurrentWaypointIndex = 0;
        Health = health;
        Speed = speed;
        Reward = reward;
        Type = type;
        Sprite = sprite;
    }

    public abstract void TakeDamage(int damage);
    public abstract bool IsDefeated();
    public abstract void Move(Vector2 targetPosition);
    public abstract void Update(Citadel citadel);
    public abstract void Draw(SpriteBatch spriteBatch);

}