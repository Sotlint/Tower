using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
using Tower.Core.Helpers;

namespace Tower.Core;

/// <summary>
/// Огненная башня. Реализует интерфейс ITower и предоставляет функциональность
/// огненной башни: атака врагов с огненным уроном, отрисовка, обновление.
/// </summary>
public partial class FireTower : ITower
{
    /// <summary>
    /// Конструктор огненной башни. Инициализирует все свойства башни.
    /// </summary>
    /// <param name="id">Уникальный идентификатор башни</param>
    /// <param name="sprite">Текстура спрайта башни</param>
    /// <param name="position">Позиция башни на карте</param>
    /// <param name="type">Тип башни</param>
    /// <param name="health">Здоровье башни</param>
    /// <param name="attackPower">Сила атаки (урон)</param>
    /// <param name="attackRange">Радиус атаки</param>
    /// <param name="attackDelay">Задержка между атаками</param>
    public FireTower(Guid id, Texture2D sprite, Vector2 position, TowerTypeEnum type, int health, int attackPower,
        float attackRange, TimeSpan attackDelay)
    {
        Id = id;
        Sprite = sprite;
        Position = position;
        Type = type;
        Health = health;
        MaxHealth = health;
        AttackPower = attackPower;
        AttackRange = attackRange;
        TimeSinceLastAttack = TimeSpan.Zero;
        AttackDelay = attackDelay;
        SpriteScale = this.GetSpriteScale(); // Получаем масштаб спрайта для данного типа башни
        UpdateBounds(); // Вычисляем границы коллизий
    }

    /// <summary>Уникальный идентификатор башни</summary>
    public Guid Id { get; private set; }
    
    /// <summary>Масштаб спрайта башни (для отрисовки)</summary>
    public float SpriteScale { get; init; }
    
    /// <summary>Текстура спрайта башни</summary>
    public Texture2D Sprite { get; private set; }
    
    /// <summary>Позиция башни на карте</summary>
    public Vector2 Position { get; private set; }
    
    /// <summary>Тип башни</summary>
    public TowerTypeEnum Type { get; private set; }
    
    /// <summary>Максимальное здоровье башни</summary>
    public int MaxHealth { get; init; }
    
    /// <summary>Текущее здоровье башни</summary>
    public int Health { get; private set; }
    
    /// <summary>Сила атаки башни (урон, наносимый врагам)</summary>
    public int AttackPower { get; private set; }
    
    /// <summary>Радиус атаки башни (максимальное расстояние до цели)</summary>
    public float AttackRange { get; private set; }
    
    /// <summary>Задержка между атаками</summary>
    public TimeSpan AttackDelay { get; private set; }
    
    /// <summary>Время, прошедшее с последней атаки</summary>
    public TimeSpan TimeSinceLastAttack { get; private set; }
    
    /// <summary>Границы башни для проверки коллизий</summary>
    public Rectangle Bounds { get; private set;}
}

