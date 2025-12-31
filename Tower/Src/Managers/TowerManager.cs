using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;

namespace Tower.Managers;

/// <summary>
/// Менеджер башен. Управляет коллекцией всех башен на карте.
/// Предоставляет методы для добавления, удаления, обновления и получения башен.
/// </summary>
public class TowerManager
{
    /// <summary>Список всех башен на карте</summary>
    private List<ITower> Towers { get; set; } = new();

    /// <summary>
    /// Добавить башню на карту.
    /// </summary>
    /// <param name="tower">Башня для добавления</param>
    public void AddTower(ITower tower)
        => Towers.Add(tower);

    /// <summary>
    /// Удалить башню с карты.
    /// </summary>
    /// <param name="tower">Башня для удаления</param>
    public void RemoveTower(ITower tower)
        => Towers.Remove(tower);

    /// <summary>
    /// Обновление всех башен. Вызывается каждый кадр во время активной волны.
    /// Каждая башня ищет цели в радиусе атаки и стреляет по врагам.
    /// </summary>
    /// <param name="gameTime">Время игры для синхронизации</param>
    public void UpdateTowers(GameTime gameTime)
    {
        // Обновляем каждую башню (поиск целей, атака)
        foreach (var tower in Towers)
        {
            tower.Update(gameTime);
        }
    }
    
    /// <summary>
    /// Отрисовка всех башен на карте.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры</param>
    public void DrawTowers(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Отрисовываем каждую башню
        foreach (var tower in Towers)
        {
            tower.Draw(spriteBatch, gameTime);
        }
    }
    
    /// <summary>
    /// Получить башню по уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор башни</param>
    /// <returns>Башня с указанным ID</returns>
    /// <exception cref="InvalidOperationException">Если башня с таким ID не найдена</exception>
    public ITower GetTower(Guid id)
    => Towers.Single(x=>x.Id == id);
    
    /// <summary>
    /// Получить список всех башен на карте.
    /// Используется для проверки коллизий и других операций, требующих доступа ко всем башням.
    /// </summary>
    /// <returns>Список всех башен</returns>
    public List<ITower> GetTowers() => Towers;
}