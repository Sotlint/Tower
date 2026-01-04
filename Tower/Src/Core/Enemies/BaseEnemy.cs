#nullable enable
using System;
using System.Collections.Generic;
using GameKit2D.Abstractions.Interfaces.GameObjects;
using GameKit2D.Core.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;

namespace Tower.Core.Enemies;

/// <summary>
/// Базовый класс для всех врагов в игре.
/// Наследуется от MovableCollidableGameObject и реализует интерфейс IEnemy.
/// Предоставляет базовую функциональность движения, здоровья, атаки и коллизий.
/// </summary>
public abstract class BaseEnemy : MovableCollidableGameObject, IEnemy, ICanDie, ICanAttack, IHaveGameLogic
{
    /// <summary>Тип врага</summary>
    public abstract EnemyTypeEnum Type { get; }
    
    /// <summary>Награда за убийство врага</summary>
    public abstract int Reward { get; }
    
    /// <summary>Максимальное здоровье врага</summary>
    public decimal MaxHealth { get; protected set; }
    
    /// <summary>Текущее здоровье врага</summary>
    public decimal Health { get; protected set; }
    
    /// <summary>Сила атаки врага</summary>
    public decimal AttackPower { get; protected set; }
    
    /// <summary>Радиус атаки врага</summary>
    public float AttackRange { get; protected set; }
    
    /// <summary>Время, прошедшее с последней атаки</summary>
    public TimeSpan TimeSinceLastAttack { get; protected set; }
    
    /// <summary>Задержка между атаками</summary>
    public TimeSpan AttackDelay { get; protected set; }
    
    /// <summary>Цель для движения (обычно цитадель)</summary>
    protected IHavePosition? Target { get; set; }

    /// <summary>
    /// Создает новый базовый враг
    /// </summary>
    /// <param name="position">Начальная позиция врага</param>
    /// <param name="sprite">Текстура спрайта врага</param>
    /// <param name="speed">Скорость движения врага</param>
    /// <param name="maxHealth">Максимальное здоровье</param>
    /// <param name="attackPower">Сила атаки</param>
    /// <param name="attackRange">Радиус атаки</param>
    /// <param name="attackDelay">Задержка между атаками</param>
    /// <param name="spriteScale">Масштаб спрайта</param>
    /// <param name="collisionSize">Размер коллизии</param>
    /// <param name="collisionOffset">Смещение коллизии</param>
    protected BaseEnemy(
        Vector2 position,
        Texture2D sprite,
        int speed,
        decimal maxHealth,
        decimal attackPower,
        float attackRange,
        TimeSpan attackDelay,
        float spriteScale = 1.0f,
        Vector2? collisionSize = null,
        Vector2? collisionOffset = null)
        : base(position, sprite, speed, spriteScale, collisionSize, collisionOffset)
    {
        MaxHealth = maxHealth;
        Health = maxHealth;
        AttackPower = attackPower;
        AttackRange = attackRange;
        AttackDelay = attackDelay;
        TimeSinceLastAttack = TimeSpan.Zero;
    }

    /// <summary>
    /// Получение урона. Уменьшает здоровье на указанное количество.
    /// </summary>
    /// <param name="damage">Количество урона</param>
    public virtual void TakeDamage(decimal damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    /// <summary>
    /// Проверка, уничтожен ли враг (здоровье <= 0).
    /// </summary>
    /// <returns>true, если враг уничтожен, false - иначе</returns>
    public virtual bool IsDie()
    {
        return Health <= 0;
    }

    /// <summary>
    /// Атака целей. Наносит урон указанным целям.
    /// </summary>
    /// <param name="targets">Список целей для атаки</param>
    public virtual void Attack(IEnumerable<ICanDie> targets)
    {
        if (targets == null)
            return;

        foreach (var target in targets)
        {
            if (target != null && !target.IsDie())
            {
                target.TakeDamage(AttackPower);
            }
        }

        TimeSinceLastAttack = TimeSpan.Zero;
    }

    /// <summary>
    /// Обновление врага. Вызывается каждый кадр.
    /// Обрабатывает движение к цели, атаку и обновление времени.
    /// </summary>
    /// <param name="gameTime">Время игры</param>
    public virtual void Update(GameTime gameTime)
    {
        if (IsDie())
            return;

        // Обновляем время с последней атаки
        TimeSinceLastAttack += gameTime.ElapsedGameTime;

        // Движение к цели
        if (Target != null)
        {
            var targetPos = Target.Position;
            var distanceToTarget = Vector2.Distance(Position, targetPos);

            // Если достигли цели (в пределах радиуса атаки)
            if (distanceToTarget <= AttackRange)
            {
                // Проверяем, можем ли атаковать
                if (TimeSinceLastAttack >= AttackDelay)
                {
                    // Атакуем цель, если это ICanDie
                    if (Target is ICanDie targetCanDie)
                    {
                        Attack(new[] { targetCanDie });
                    }
                }
            }
            else
            {
                // Двигаемся к цели с учетом времени кадра
                var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
                Move(targetPos, deltaTime);
            }
        }
    }

    /// <summary>
    /// Устанавливает цель для движения и атаки
    /// </summary>
    /// <param name="target">Цель (обычно цитадель)</param>
    public virtual void SetTarget(IHavePosition target)
    {
        Target = target;
    }
}

