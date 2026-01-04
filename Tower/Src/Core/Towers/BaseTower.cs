#nullable enable
using System;
using System.Collections.Generic;
using GameKit2D.Abstractions.Interfaces.GameObjects;
using GameKit2D.Core.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;

namespace Tower.Core.Towers;

/// <summary>
/// Базовый класс для всех башен в игре.
/// Наследуется от CollidableGameObject и реализует интерфейс ITower.
/// Предоставляет базовую функциональность атаки врагов, здоровья и коллизий.
/// </summary>
public abstract class BaseTower : CollidableGameObject, ITower
{
    /// <summary>Тип башни</summary>
    public abstract TowerTypeEnum Type { get; }
    
    /// <summary>Максимальное здоровье башни</summary>
    public decimal MaxHealth { get; protected set; }
    
    /// <summary>Текущее здоровье башни</summary>
    public decimal Health { get; protected set; }
    
    /// <summary>Сила атаки башни</summary>
    public decimal AttackPower { get; protected set; }
    
    /// <summary>Радиус атаки башни</summary>
    public float AttackRange { get; protected set; }
    
    /// <summary>Время, прошедшее с последней атаки</summary>
    public TimeSpan TimeSinceLastAttack { get; protected set; }
    
    /// <summary>Задержка между атаками</summary>
    public TimeSpan AttackDelay { get; protected set; }
    
    /// <summary>Текущая цель для атаки</summary>
    protected IEnemy? CurrentTarget { get; set; }

    /// <summary>
    /// Создает новую базовую башню
    /// </summary>
    /// <param name="position">Позиция башни</param>
    /// <param name="sprite">Текстура спрайта башни</param>
    /// <param name="maxHealth">Максимальное здоровье</param>
    /// <param name="attackPower">Сила атаки</param>
    /// <param name="attackRange">Радиус атаки</param>
    /// <param name="attackDelay">Задержка между атаками</param>
    /// <param name="spriteScale">Масштаб спрайта</param>
    /// <param name="collisionSize">Размер коллизии</param>
    /// <param name="collisionOffset">Смещение коллизии</param>
    protected BaseTower(
        Vector2 position,
        Texture2D sprite,
        decimal maxHealth,
        decimal attackPower,
        float attackRange,
        TimeSpan attackDelay,
        float spriteScale = 1.0f,
        Vector2? collisionSize = null,
        Vector2? collisionOffset = null)
        : base(position, sprite, spriteScale, collisionSize, collisionOffset)
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
    /// Проверка, уничтожена ли башня (здоровье <= 0).
    /// </summary>
    /// <returns>true, если башня уничтожена, false - иначе</returns>
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
    /// Обновление башни. Вызывается каждый кадр.
    /// Обрабатывает поиск целей и атаку врагов.
    /// </summary>
    /// <param name="gameTime">Время игры</param>
    public virtual void Update(GameTime gameTime)
    {
        if (IsDie())
            return;

        // Обновляем время с последней атаки
        TimeSinceLastAttack += gameTime.ElapsedGameTime;

        // Поиск цели и атака будет реализована в наследниках или через системы
        // Здесь базовая логика - если есть цель и можем атаковать
        if (CurrentTarget != null && !CurrentTarget.IsDie())
        {
            var distanceToTarget = Vector2.Distance(Position, CurrentTarget.Position);
            
            if (distanceToTarget <= AttackRange && TimeSinceLastAttack >= AttackDelay)
            {
                Attack(new[] { CurrentTarget });
            }
        }
    }

    /// <summary>
    /// Устанавливает цель для атаки
    /// </summary>
    /// <param name="target">Враг-цель</param>
    public virtual void SetTarget(IEnemy? target)
    {
        CurrentTarget = target;
    }
}

