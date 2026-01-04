using System;
using System.Collections.Generic;
using GameKit2D.Abstractions.Interfaces.GameObjects;
using GameKit2D.Core.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Managers;

namespace Tower.Core.Buildings;

/// <summary>
/// Цитадель - главное здание, которое защищают игроки.
/// Наследуется от CollidableGameObject и реализует интерфейс IBuilding.
/// </summary>
public class Citadel : CollidableGameObject, IBuilding, ICanDie, ICanAttack, IHaveGameLogic
{
    /// <summary>Максимальное здоровье цитадели</summary>
    public decimal MaxHealth { get; private set; }
    
    /// <summary>Текущее здоровье цитадели</summary>
    public decimal Health { get; private set; }
    
    /// <summary>Сила атаки цитадели (обычно 0, так как цитадель не атакует)</summary>
    public decimal AttackPower { get; private set; }
    
    /// <summary>Радиус атаки цитадели (обычно 0)</summary>
    public float AttackRange { get; private set; }
    
    /// <summary>Время, прошедшее с последней атаки</summary>
    public TimeSpan TimeSinceLastAttack { get; private set; }
    
    /// <summary>Задержка между атаками (обычно очень большая, так как цитадель не атакует)</summary>
    public TimeSpan AttackDelay { get; private set; }

    /// <summary>
    /// Создает новую цитадель
    /// </summary>
    /// <param name="position">Позиция цитадели</param>
    public Citadel(Vector2 position)
        : base(position, SpriteManager.CitadelSprite, 1.0f)
    {
        MaxHealth = 1000;
        Health = MaxHealth;
        AttackPower = 0; // Цитадель не атакует
        AttackRange = 0;
        AttackDelay = TimeSpan.FromHours(1); // Очень большая задержка
        TimeSinceLastAttack = TimeSpan.Zero;
    }

    /// <summary>
    /// Получение урона. Уменьшает здоровье на указанное количество.
    /// </summary>
    /// <param name="damage">Количество урона</param>
    public void TakeDamage(decimal damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    /// <summary>
    /// Проверка, уничтожена ли цитадель (здоровье <= 0).
    /// </summary>
    /// <returns>true, если цитадель уничтожена, false - иначе</returns>
    public bool IsDie()
    {
        return Health <= 0;
    }

    /// <summary>
    /// Атака целей. Цитадель не атакует, поэтому метод пустой.
    /// </summary>
    /// <param name="targets">Список целей для атаки</param>
    public void Attack(IEnumerable<ICanDie> targets)
    {
        // Цитадель не атакует
    }

    /// <summary>
    /// Обновление цитадели. Вызывается каждый кадр.
    /// </summary>
    /// <param name="gameTime">Время игры</param>
    public void Update(GameTime gameTime)
    {
        // Базовая логика обновления цитадели
        // Можно добавить регенерацию здоровья или другие эффекты
    }
}

