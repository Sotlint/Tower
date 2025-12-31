using System;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;

namespace Tower.Core.Helpers;

/// <summary>
/// Вспомогательный класс для получения масштаба спрайтов.
/// Предоставляет extension-методы для определения масштаба спрайтов
/// различных типов объектов (враги, башни, здания).
/// </summary>
public static class SpriteScaleHelper
{
    /// <summary>
    /// Получить масштаб спрайта для врага в зависимости от его типа.
    /// </summary>
    /// <param name="enemy">Враг, для которого нужно получить масштаб</param>
    /// <returns>Масштаб спрайта (0.05 = 5% от исходного размера)</returns>
    /// <exception cref="ArgumentOutOfRangeException">Если передан неизвестный тип врага</exception>
    public static float GetSpriteScale(this IEnemy enemy)
    {
        var type = enemy.Type;
        return type switch
        {
            EnemyTypeEnum.Fast => 0.05f, // 5% от исходного размера
            EnemyTypeEnum.Basic => 0.05f, // 5% от исходного размера
            EnemyTypeEnum.Armored => 0.05f, // 5% от исходного размера
            EnemyTypeEnum.Boss => 0.05f, // 5% от исходного размера
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Получить масштаб спрайта для башни в зависимости от ее типа.
    /// </summary>
    /// <param name="tower">Башня, для которой нужно получить масштаб</param>
    /// <returns>Масштаб спрайта (0.1 = 10% от исходного размера)</returns>
    /// <exception cref="ArgumentOutOfRangeException">Если передан неизвестный тип башни</exception>
    public static float GetSpriteScale(this ITower tower)
    {
        var type = tower.Type;
        return type switch
        {
            TowerTypeEnum.Basic => 0.1f, // 10% от исходного размера
            TowerTypeEnum.Fire => 0.1f, // 10% от исходного размера
            TowerTypeEnum.Frost => 0.1f, // 10% от исходного размера
            TowerTypeEnum.Bomber => 0.1f, // 10% от исходного размера
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    /// <summary>
    /// Получить масштаб спрайта для здания. Просто возвращает уже установленный масштаб.
    /// </summary>
    /// <param name="building">Здание, для которого нужно получить масштаб</param>
    /// <returns>Масштаб спрайта здания</returns>
    public static float GetSpriteScale(this IBuilding building)
        => building.SpriteScale;
}