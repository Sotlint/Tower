using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;
using Tower.Models.Abstractions;
using Tower.Models.Abstractions.Base;
using Tower.Models.Abstractions.Enums;

namespace Tower.Models;

public class BasicTower : ITower
{
    public BasicTower(Guid id, Texture2D sprite, Vector2 position, TowerTypeEnum type, int health, int attackPower, float attackRange)
    {
        Id = id;
        Sprite = sprite;
        Position = position;
        Type = type;
        Health = health;
        AttackPower = attackPower;
        AttackRange = attackRange;
    }

    public Guid Id { get; private set; }
    public Texture2D Sprite { get; private set; }
    public Vector2 Position { get; private set; }
    
    public TowerTypeEnum Type { get; private set;}
    public int Health { get; private set; }
    public int AttackPower { get; private set; }
    public float AttackRange { get; private set; }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        var rotation = 0f;
        var scale = 0.05f;
        spriteBatch.Draw(
            Sprite, // текстура
            Position, // позиция
            null, // исходный прямоугольник (null = вся текстура)
            Color.White, // цвет (без изменений)
            rotation, // поворот
            Vector2.Zero, // точка привязки (верхний левый угол)
            scale, // масштаб
            SpriteEffects.None, // эффекты (например, зеркальное отражение)
            0f // слой (глубина)
        );
        spriteBatch.End();
    }

    public void Update(GameManager gameManager)
    {
        var target = gameManager.EnemyManager.GetAliveEnemies()
            .Where(e => Vector2.Distance(Position, e.Position) <= AttackRange)
            .MinBy(e => Vector2.Distance(Position, e.Position));
        
        Attack(new List<ICanDie> { target });
    }
    
    public void Attack(ICollection<ICanDie> targets)
    {
        foreach (var target in targets)
        {
            target?.TakeDamage(AttackPower);
        }
    }

    public void TakeDamage(int damage)
        => Health -= damage;


    public bool IsDie()
        => Health <= 0;
}