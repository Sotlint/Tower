using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;

namespace Tower.Managers;

/// <summary>
/// Менеджер башен, предоставляющий методы для добавления, удаления, обновления и получения башен.
/// </summary>
public class TowerManager
{
    private List<ITower> Towers { get; set; } = new();

    public void AddTower(ITower tower)
        => Towers.Add(tower);

    public void RemoveTower(ITower tower)
        => Towers.Remove(tower);

    public void UpdateTowers(GameTime gameTime)
    {
        foreach (var tower in Towers)
        {
            tower.Update(gameTime);
        }
    }
    
    public void DrawTowers(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (var tower in Towers)
        {
            tower.Draw(spriteBatch, gameTime);
        }
    }
    
    public ITower GetTower(Guid id)
    => Towers.Single(x=>x.Id == id);
    
    public List<ITower> GetTowers() => Towers;
}