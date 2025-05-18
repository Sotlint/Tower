using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Base;
using Tower.Core.Helpers;
using Tower.Managers;

namespace Tower.Core;

public partial class BasicEnemy : IEnemy
{
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
            SetPosition(direction * Speed);
        }
    }

    public void Update(GameTime gameTime)
    {
        UpdateBounds();
        ResolveCollision();
        
        if (IsDie())
            return;
        
        var citadel = GameManager.CitadelManager.GetCitadel();
        if (Vector2.Distance(Position, citadel.Position) <= AttackRange)
        {
            TimeSinceLastAttack += gameTime.ElapsedGameTime;
            if (TimeSinceLastAttack < AttackDelay)
                return;

            Attack(new List<ICanDie>() { citadel });
            TimeSinceLastAttack = TimeSpan.Zero;

            return;
        }
        
        Move(citadel.Position);
        
        TimeSinceLastAttack += gameTime.ElapsedGameTime;
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        spriteBatch.Begin();
        DebugBorderDrawer.DrawDebug(spriteBatch, Bounds);
        spriteBatch.Draw(
            Sprite, // текстура
            Position, // позиция
            null, // исходный прямоугольник (null = вся текстура)
            Color.White, // цвет (без изменений)
            0f, // поворот
            new Vector2(Sprite.Width/2, Sprite.Height/2), // точка привязки (верхний левый угол)
            SpriteScale, // масштаб
            SpriteEffects.None, // эффекты (например, зеркальное отражение)
            1f // слой (глубина)
        );
        HealthBar.Draw(
            spriteBatch,
            gameTime,
            this
        );
        spriteBatch.End();
    }

    public void Attack(IEnumerable<ICanDie> targets)
    {
        foreach (var target in targets)
        {
            target.TakeDamage(AttackPower);
        }
    }

    public void ResolveCollision()
    {
        ResolveEnemyCollision();
        ResolveBuildingsCollision();
    }

    private void ResolveBuildingsCollision()
    {
        var towers = GameManager.TowerManager.GetTowers().Select(x=>(IBuilding)x).ToList();
        var citadel = (IBuilding)GameManager.CitadelManager.GetCitadel();

        foreach (var building in towers.Concat(new List<IBuilding>() { citadel }))
        {
            if (Bounds.Intersects(building.Bounds))
            {
                var moveDirection = Position - building.Position;
                if (moveDirection == Vector2.Zero)
                {
                    moveDirection = new Vector2(
                        (float)(0.5 - Random.Shared.NextDouble()),
                        (float)(0.5 - Random.Shared.NextDouble())
                    );
                }
                
                moveDirection = Vector2.Normalize(moveDirection);
                var overlap = (Bounds.Width / 2f + building.Bounds.Width / 2f) -
                              Vector2.Distance(Position, building.Position);
                Position += moveDirection * (overlap / 2);

                SetPosition(moveDirection * (overlap / 2));
            }
        }
    }
    
    private void ResolveEnemyCollision()
    {
        var enemies = GameManager.EnemyManager.GetEnemies().Where(x => x.Id != Id).ToList();
        foreach (var enemy in enemies)
        {
            var enemyBounds = enemy.Bounds;
            // Проверяем пересечение
            if (Bounds.Intersects(enemyBounds))
            {
                // Вычисляем направление, чтобы раздвинуть врагов
                var moveDirection = Position - enemy.Position;
                // Если враги находятся на одном месте, создаем случайное направление
                if (moveDirection == Vector2.Zero)
                {
                    moveDirection = new Vector2(
                        (float)(0.5 - Random.Shared.NextDouble()),
                        (float)(0.5 - Random.Shared.NextDouble())
                    );
                }

                moveDirection = Vector2.Normalize(moveDirection);
                // Сдвигаем врагов в разные стороны
                var overlap = (Bounds.Width / 2f + enemyBounds.Width / 2f) -
                              Vector2.Distance(Position, enemy.Position);
                Position += moveDirection * (overlap / 2);

                SetPosition(moveDirection * (overlap / 2));
            }
        }
    }

    public void UpdateBounds()
    {
        var width = (int)(Sprite.Width * SpriteScale);
        var height = (int)(Sprite.Height * SpriteScale);

        Bounds = new Rectangle(
            (int)(Position.X - width/2 ), // Центрирование по X
            (int)(Position.Y - height/2 ), // Центрирование по Y
            width,
            height
        );
    }


    public void SetPosition(Vector2 direction)
        => Position += direction;
}