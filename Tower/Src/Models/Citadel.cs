using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;
using Tower.Models.Abstractions;
using Tower.Models.Abstractions.Base;
using Tower.Models.Abstractions.Enums;

namespace Tower.Models;

public class Citadel : IBuilding
{
    public Citadel(Guid id, Vector2 position, int health, int attackPower, float attackRange)
    {
        Id = id;
        Position = position;
        Health = health;
        AttackPower = attackPower;
        AttackRange = attackRange;
    }

    public Guid Id { get; private set; }
    public Texture2D Sprite { get; private set; }
    public Vector2 Position { get; private set; }
    public int Health { get; private set; }
    public int AttackPower { get; private set; }
    public float AttackRange { get; private set; }

    public void Attack(ICollection<ICanDie> targets)
    {
        throw new NotImplementedException();
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }

    public bool IsDie()
        => Health <= 0;

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        var rotation = 0f;
        var scale = 0.1f;
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

    public void SetSprite(Texture2D sprite)
    {
        Sprite = sprite;
    }

    public void Attack(ICanDie target)
    {
        throw new NotImplementedException();
    }

    public void Update(GameManager gameManager)
    {
        if (IsDie())
        {
            gameManager.GameStateManager.ChangeState(GameStateEnum.GameOver);
        }
    }
}