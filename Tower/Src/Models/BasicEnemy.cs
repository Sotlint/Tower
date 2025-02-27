using System;
using Microsoft.Xna.Framework;
using Tower.Src.Managers;
using Tower.Src.Models.Abstractions;
using Tower.Src.Models.Base;

namespace Tower.Src.Models;

public class BasicEnemy : BaseEnemy, IEnemy
{
    public BasicEnemy(int health, int speed, int reward, EnemyTypeEnum type) : base(health, speed, reward, type)
    {
    }

    public override void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public override bool IsDefeated() => Health <= 0;

    public override void Move(Vector2 targetPosition)
    {
        var direction = targetPosition - Position;

        // Проверка, чтобы не делить на 0
        if (direction.Length() > 0)
        {
            direction = Vector2.Normalize(direction);
            Position += direction * Speed;
        }
    }

    public override void Update(Citadel citadel)
    {
        Move(citadel.Position);
        Console.WriteLine($"Враг {Id} движется в направлении {Position}");
        if (Vector2.Distance(Position, citadel.Position) < 5f)
        {
            CurrentWaypointIndex++;
            AttackCitadel(citadel);
            Console.WriteLine($"Враг {Id} ударил цитадель. У цитадели осталось {citadel.Health}");
        }
    }

    private void AttackCitadel(Citadel citadel)
    {
        citadel.TakeDamage(10);
    }
}