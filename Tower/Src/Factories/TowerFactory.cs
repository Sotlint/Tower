using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
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
    /// <exception cref="NotImplementedException">Если тип башни еще не реализован</exception>
    /// <exception cref="ArgumentOutOfRangeException">Если передан неизвестный тип башни</exception>
    public static int GetTowerCost(TowerTypeEnum type)
        => type switch
        {
            TowerTypeEnum.Basic => 1, // Тестовая цена: 1 монета
            TowerTypeEnum.Fire => throw new NotImplementedException(),
            TowerTypeEnum.Frost => throw new NotImplementedException(),
            TowerTypeEnum.Bomber => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

    /// <summary>
    /// Создать башню указанного типа в указанной позиции.
    /// </summary>
    /// <param name="type">Тип башни для создания</param>
    /// <param name="position">Позиция на карте, где будет размещена башня</param>
    /// <returns>Созданная башня, реализующая интерфейс ITower</returns>
    /// <exception cref="NotImplementedException">Если тип башни еще не реализован</exception>
    /// <exception cref="ArgumentOutOfRangeException">Если передан неизвестный тип башни</exception>
    public static ITower CreateTower(TowerTypeEnum type, Vector2 position)
        => type switch
        {
            TowerTypeEnum.Basic => CreateBasicTower(position, SpriteManager.TowerSprite),
            TowerTypeEnum.Fire => throw new NotImplementedException(),
            TowerTypeEnum.Frost => throw new NotImplementedException(),
            TowerTypeEnum.Bomber => throw new NotImplementedException(),
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };

    /// <summary>
    /// Создать базовую башню. Базовая башня имеет стандартные характеристики:
    /// здоровье 100, урон 25, радиус атаки 100, задержка между атаками 250мс.
    /// </summary>
    /// <param name="position">Позиция башни на карте</param>
    /// <param name="sprite">Текстура спрайта башни</param>
    /// <returns>Созданная базовая башня</returns>
    private static ITower CreateBasicTower(Vector2 position, Texture2D sprite) =>
        new BasicTower(
            Guid.NewGuid(), // Уникальный идентификатор башни
            sprite, // Текстура спрайта
            position, // Позиция на карте
            TowerTypeEnum.Basic, // Тип башни
            100, // Здоровье
            25, // Сила атаки
            100, // Радиус атаки
            TimeSpan.FromMilliseconds(250) // Задержка между атаками (250мс)
        );
}