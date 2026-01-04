using GameKit2D.Abstractions.Interfaces.GameObjects;
using GameKit2D.Core.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;

namespace Tower.Core.Projectiles;

/// <summary>
/// Базовый класс для всех снарядов в игре.
/// Наследуется от ProjectileObject из GameKit и реализует интерфейс ITowerProjectile.
/// Предоставляет базовую функциональность движения к цели и нанесения урона.
/// </summary>
public abstract class BaseProjectile : ProjectileObject, ITowerProjectile
{
    /// <summary>
    /// Флаг, указывающий, попал ли снаряд в цель
    /// </summary>
    public new bool IsAttacked { get; protected set; }

    /// <summary>
    /// Создает новый базовый снаряд
    /// </summary>
    /// <param name="position">Начальная позиция снаряда</param>
    /// <param name="sprite">Текстура спрайта снаряда</param>
    /// <param name="target">Цель снаряда (враг)</param>
    /// <param name="targetPosition">Позиция цели</param>
    /// <param name="speed">Скорость движения снаряда</param>
    /// <param name="attackPower">Сила атаки (урон)</param>
    /// <param name="attackRange">Радиус атаки (радиус попадания)</param>
    /// <param name="spriteScale">Масштаб спрайта</param>
    protected BaseProjectile(
        Vector2 position,
        Texture2D sprite,
        ICanDie target,
        IHavePosition targetPosition,
        int speed,
        decimal attackPower,
        float attackRange,
        float spriteScale = 1.0f)
        : base(position, sprite, target, targetPosition, speed, attackPower, attackRange, spriteScale)
    {
        IsAttacked = false;
    }

    /// <summary>
    /// Обновление снаряда. Вызывается каждый кадр.
    /// Двигает снаряд к цели и проверяет попадание.
    /// </summary>
    /// <param name="gameTime">Время игры</param>
    public override void Update(GameTime gameTime)
    {
        if (IsAttacked)
            return;

        // Используем базовую логику из ProjectileObject
        base.Update(gameTime);
        
        // Обновляем наш флаг на основе состояния базового класса
        IsAttacked = base.IsAttacked;
    }

    /// <summary>
    /// Проверяет, должен ли снаряд быть удален (попал в цель или цель уничтожена)
    /// </summary>
    /// <returns>true, если снаряд должен быть удален, иначе false</returns>
    public override bool ShouldBeRemoved()
    {
        return IsAttacked || (Target != null && Target.IsDie());
    }
}

