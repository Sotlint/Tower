using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
using Tower.Core.Towers;
using Tower.Managers;

namespace Tower.Factories;

/// <summary>
/// Фабрика для создания башен. Предоставляет методы для создания башен различных типов
/// и получения их стоимости.
/// </summary>
public static class TowerFactory
{
    /// <summary>
    /// Получить стоимость башни указанного типа.
    /// </summary>
    /// <param name="type">Тип башни</param>
    /// <returns>Стоимость башни в монетах</returns>
    /// <exception cref="ArgumentOutOfRangeException">Если передан неизвестный тип башни</exception>
    public static int GetTowerCost(TowerTypeEnum type)
        => type switch
        {
            TowerTypeEnum.Stone => 1, // Тестовая цена: 1 монета
            TowerTypeEnum.Fire => 2, // Огненная башня: 2 монеты
            TowerTypeEnum.Ice => 2, // Ледяная башня: 2 монеты
            TowerTypeEnum.Light => 3, // Башня света: 3 монеты
            TowerTypeEnum.Poison => 2, // Ядовитая башня: 2 монеты
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

    /// <summary>
    /// Создать башню указанного типа в указанной позиции.
    /// </summary>
    /// <param name="type">Тип башни для создания</param>
    /// <param name="position">Позиция на карте, где будет размещена башня</param>
    /// <returns>Созданная башня, реализующая интерфейс ITower</returns>
    /// <exception cref="ArgumentOutOfRangeException">Если передан неизвестный тип башни</exception>
    public static ITower CreateTower(TowerTypeEnum type, Vector2 position)
        => type switch
        {
            TowerTypeEnum.Stone => CreateStoneTower(position, SpriteManager.StoneTowerSprite),
            TowerTypeEnum.Fire => CreateFireTower(position, SpriteManager.FireTowerSprite),
            TowerTypeEnum.Ice => CreateIceTower(position, SpriteManager.IceTowerSprite),
            TowerTypeEnum.Light => CreateLightTower(position, SpriteManager.LightTowerSprite),
            TowerTypeEnum.Poison => CreatePoisonTower(position, SpriteManager.PoisonTowerSprite),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

    /// <summary>
    /// Создать каменную башню. Каменная башня имеет стандартные характеристики:
    /// здоровье 100, урон 25, радиус атаки 100, задержка между атаками 250мс.
    /// </summary>
    /// <param name="position">Позиция башни на карте</param>
    /// <param name="sprite">Текстура спрайта башни</param>
    /// <returns>Созданная каменная башня</returns>
    private static ITower CreateStoneTower(Vector2 position, Texture2D sprite) =>
        new StoneTower(
            Guid.NewGuid(), // Уникальный идентификатор башни
            sprite, // Текстура спрайта
            position, // Позиция на карте
            TowerTypeEnum.Stone, // Тип башни
            100, // Здоровье
            25, // Сила атаки
            100, // Радиус атаки
            TimeSpan.FromMilliseconds(250) // Задержка между атаками (250мс)
        );
    
    /// <summary>
    /// Создать огненную башню. Огненная башня имеет характеристики:
    /// здоровье 80, урон 30, радиус атаки 120, задержка между атаками 200мс.
    /// </summary>
    /// <param name="position">Позиция башни на карте</param>
    /// <param name="sprite">Текстура спрайта башни</param>
    /// <returns>Созданная огненная башня</returns>
    private static ITower CreateFireTower(Vector2 position, Texture2D sprite) =>
        new FireTower(
            Guid.NewGuid(), // Уникальный идентификатор башни
            sprite, // Текстура спрайта
            position, // Позиция на карте
            TowerTypeEnum.Fire, // Тип башни
            80, // Здоровье (меньше, чем у каменной)
            30, // Сила атаки (больше, чем у каменной)
            120, // Радиус атаки (больше, чем у каменной)
            TimeSpan.FromMilliseconds(200) // Задержка между атаками (быстрее, чем у каменной)
        );
    
    /// <summary>
    /// Создать ледяную башню. Ледяная башня имеет характеристики:
    /// здоровье 90, урон 20, радиус атаки 150, задержка между атаками 300мс.
    /// </summary>
    /// <param name="position">Позиция башни на карте</param>
    /// <param name="sprite">Текстура спрайта башни</param>
    /// <returns>Созданная ледяная башня</returns>
    private static ITower CreateIceTower(Vector2 position, Texture2D sprite) =>
        new IceTower(
            Guid.NewGuid(), // Уникальный идентификатор башни
            sprite, // Текстура спрайта
            position, // Позиция на карте
            TowerTypeEnum.Ice, // Тип башни
            90, // Здоровье
            20, // Сила атаки (меньше, но замедляет врагов)
            150, // Радиус атаки (большой радиус)
            TimeSpan.FromMilliseconds(300) // Задержка между атаками (медленнее)
        );
    
    /// <summary>
    /// Создать башню света. Башня света имеет характеристики:
    /// здоровье 120, урон 35, радиус атаки 110, задержка между атаками 180мс.
    /// </summary>
    /// <param name="position">Позиция башни на карте</param>
    /// <param name="sprite">Текстура спрайта башни</param>
    /// <returns>Созданная башня света</returns>
    private static ITower CreateLightTower(Vector2 position, Texture2D sprite) =>
        new LightTower(
            Guid.NewGuid(), // Уникальный идентификатор башни
            sprite, // Текстура спрайта
            position, // Позиция на карте
            TowerTypeEnum.Light, // Тип башни
            120, // Здоровье (больше, чем у других)
            35, // Сила атаки (высокий урон)
            110, // Радиус атаки
            TimeSpan.FromMilliseconds(180) // Задержка между атаками (быстро)
        );
    
    /// <summary>
    /// Создать ядовитую башню. Ядовитая башня имеет характеристики:
    /// здоровье 85, урон 22, радиус атаки 130, задержка между атаками 220мс.
    /// </summary>
    /// <param name="position">Позиция башни на карте</param>
    /// <param name="sprite">Текстура спрайта башни</param>
    /// <returns>Созданная ядовитая башня</returns>
    private static ITower CreatePoisonTower(Vector2 position, Texture2D sprite) =>
        new PoisonTower(
            Guid.NewGuid(), // Уникальный идентификатор башни
            sprite, // Текстура спрайта
            position, // Позиция на карте
            TowerTypeEnum.Poison, // Тип башни
            85, // Здоровье
            22, // Сила атаки (средний урон, но яд наносит урон со временем)
            130, // Радиус атаки (большой радиус)
            TimeSpan.FromMilliseconds(220) // Задержка между атаками
        );
}