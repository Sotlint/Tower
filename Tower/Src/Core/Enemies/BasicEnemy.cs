using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
using Tower.Core.Common;
using Tower.Core.Helpers;

namespace Tower.Core.Enemies;

/// <summary>
/// Базовый враг. Реализует интерфейс IEnemy и предоставляет базовую функциональность
/// для всех типов врагов: движение к цитадели, атака, отрисовка, обработка коллизий.
/// </summary>
public partial class BasicEnemy : IEnemy
{
    /// <summary>
    /// Конструктор базового врага. Инициализирует все свойства врага.
    /// </summary>
    /// <param name="sprite">Текстура спрайта врага</param>
    /// <param name="id">Уникальный идентификатор врага</param>
    /// <param name="position">Начальная позиция врага на карте</param>
    /// <param name="health">Здоровье врага</param>
    /// <param name="speed">Скорость движения врага</param>
    /// <param name="attackPower">Сила атаки (урон по цитадели)</param>
    /// <param name="attackRange">Радиус атаки (расстояние для атаки цитадели)</param>
    /// <param name="type">Тип врага</param>
    /// <param name="attackDelay">Задержка между атаками</param>
    /// <param name="healthBar">Полоса здоровья для отрисовки</param>
    public BasicEnemy(Texture2D sprite, Guid id, Vector2 position, int health, int speed, int attackPower,
        float attackRange, EnemyTypeEnum type, TimeSpan attackDelay, HealthBar healthBar)
    {
        Sprite = sprite;
        Id = id;
        Position = position;
        Health = health;
        MaxHealth = health;
        Speed = speed;
        AttackPower = attackPower;
        AttackRange = attackRange;
        Type = type;
        TimeSinceLastAttack = TimeSpan.Zero;
        AttackDelay = attackDelay;
        SpriteScale = this.GetSpriteScale(); // Получаем масштаб спрайта для данного типа врага
        HealthBar = healthBar;
        Reward = 10; // Награда за убийство врага (монеты)
        UpdateBounds(); // Вычисляем границы коллизий
    }

    /// <summary>Масштаб спрайта врага (для отрисовки)</summary>
    public float SpriteScale { get; init; }
    
    /// <summary>Текстура спрайта врага</summary>
    public Texture2D Sprite { get; private set; }
    
    /// <summary>Тип врага</summary>
    public EnemyTypeEnum Type { get; private set; }
    
    /// <summary>Награда за убийство врага (количество монет)</summary>
    public int Reward { get; init; }
    
    /// <summary>Уникальный идентификатор врага</summary>
    public Guid Id { get; private set; }
    
    /// <summary>Позиция врага на карте</summary>
    public Vector2 Position { get; private set; }
    
    /// <summary>Максимальное здоровье врага</summary>
    public int MaxHealth { get; init; }
    
    /// <summary>Текущее здоровье врага</summary>
    public int Health { get; private set; }
    
    /// <summary>Скорость движения врага</summary>
    public int Speed { get; private set; }
    
    /// <summary>Сила атаки врага (урон по цитадели)</summary>
    public int AttackPower { get; private set; }
    
    /// <summary>Радиус атаки врага (расстояние для атаки цитадели)</summary>
    public float AttackRange { get; private set; }
    
    /// <summary>Задержка между атаками</summary>
    public TimeSpan AttackDelay { get; private set; }
    
    /// <summary>Полоса здоровья для отрисовки</summary>
    private HealthBar HealthBar { get; init; }
    
    /// <summary>Время, прошедшее с последней атаки</summary>
    public TimeSpan TimeSinceLastAttack { get; private set; }
    
    /// <summary>Границы врага для проверки коллизий</summary>
    public Rectangle Bounds { get; private set; }
}