using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;
using Tower.Models.Abstractions;
using Tower.Models.Abstractions.Base;
using Tower.Models.Abstractions.Enums;

namespace Tower.Models;

public class BasicEnemy : IEnemy
{
    public BasicEnemy(Texture2D sprite, Guid id, Vector2 position, int health, int speed, int attackPower,
        float attackRange, EnemyTypeEnum type)
    {
        Sprite = sprite;
        Id = id;
        Position = position;
        Health = health;
        Speed = speed;
        AttackPower = attackPower;
        AttackRange = attackRange;
        Type = type;
    }

    public Texture2D Sprite { get; private set; }
    public EnemyTypeEnum Type { get; private set; }
    public Guid Id { get; private set; }
    public Vector2 Position { get; private set; }
    public int Health { get; private set; }
    public int Speed { get; private set; }
    public int AttackPower { get; private set; }
    public float AttackRange { get; private set; }


    public void TakeDamage(int damage)
        => Health -= damage;

    public bool IsDie() => Health <= 0;

    public void Move(Vector2 targetPosition)
    {
        var direction = targetPosition - Position;

        // Проверка, чтобы не делить на 0
        if (direction.Length() > 0)
        {
            direction = Vector2.Normalize(direction);
            Position += direction * Speed;
        }
    }

    public void Update(GameManager gameManager)
    {
        if (IsDie()) 
            return;
        
        var citadel = gameManager.CitadelManager.GetCitadel();
        Move(citadel.Position);
        Console.WriteLine($"Враг {Id} движется в направлении {Position}");
        if (Vector2.Distance(Position, citadel.Position) <= AttackRange)
        {
            Attack(new List<ICanDie>() { citadel });
            Console.WriteLine($"Враг {Id} ударил цитадель. У цитадели осталось {citadel.Health}");
        }
    }

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

    public void Attack(ICollection<ICanDie> targets)
    {
        foreach (var target in targets)
        {
            target.TakeDamage(AttackPower);
        }
    }
}