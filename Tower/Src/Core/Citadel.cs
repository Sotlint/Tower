using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Common;
using Tower.Core.Helpers;
using Tower.Managers;

namespace Tower.Core;

/// <summary>
/// Цитадель. Главная цель врагов в игре. Реализует интерфейс IBuilding.
/// Если здоровье цитадели достигает 0, игра заканчивается.
/// </summary>
public partial class Citadel : IBuilding
{
    /// <summary>
    /// Конструктор цитадели. Инициализирует все свойства цитадели.
    /// </summary>
    /// <param name="id">Уникальный идентификатор цитадели</param>
    /// <param name="position">Позиция цитадели на карте (обычно центр экрана)</param>
    /// <param name="health">Здоровье цитадели</param>
    /// <param name="attackPower">Сила атаки (не используется, цитадель не атакует)</param>
    /// <param name="attackRange">Радиус атаки (не используется, цитадель не атакует)</param>
    /// <param name="attackDelay">Задержка между атаками (не используется, цитадель не атакует)</param>
    /// <param name="healthBar">Полоса здоровья для отрисовки</param>
    public Citadel(Guid id, Vector2 position, int health, int attackPower, float attackRange, TimeSpan attackDelay,
        HealthBar healthBar)
    {
        Id = id;
        Position = position;
        Health = health;
        AttackPower = attackPower;
        AttackRange = attackRange;
        TimeSinceLastAttack = TimeSpan.Zero;
        AttackDelay = attackDelay;
        Sprite = SpriteManager.CitadelSprite; // Загружаем спрайт цитадели из SpriteManager
        SpriteScale = 0.1f; // Получаем масштаб спрайта для зданий
        MaxHealth = health;
        HealthBar = healthBar;
        UpdateBounds(); // Вычисляем границы коллизий
    }
    
    /// <summary>Полоса здоровья для отрисовки</summary>
    private HealthBar HealthBar { get; init; }
    
    /// <summary>Уникальный идентификатор цитадели</summary>
    public Guid Id { get; private set; }
    
    /// <summary>Масштаб спрайта цитадели (для отрисовки)</summary>
    public float SpriteScale { get; init; }
    
    /// <summary>Текстура спрайта цитадели</summary>
    public Texture2D Sprite { get; private set; }
    
    /// <summary>Позиция цитадели на карте</summary>
    public Vector2 Position { get; private set; }
    
    /// <summary>Максимальное здоровье цитадели</summary>
    public int MaxHealth { get; init; }
    
    /// <summary>Текущее здоровье цитадели</summary>
    public int Health { get; private set; }
    
    /// <summary>Сила атаки цитадели (не используется, цитадель не атакует)</summary>
    public int AttackPower { get; private set; }
    
    /// <summary>Радиус атаки цитадели (не используется, цитадель не атакует)</summary>
    public float AttackRange { get; private set; }
    
    /// <summary>Задержка между атаками (не используется, цитадель не атакует)</summary>
    public TimeSpan AttackDelay { get; private set; }
    
    /// <summary>Время, прошедшее с последней атаки (не используется, цитадель не атакует)</summary>
    public TimeSpan TimeSinceLastAttack { get; private set; }
    
    /// <summary>Границы цитадели для проверки коллизий</summary>
    public Rectangle Bounds { get; private set; }
}