using System;
using System.Collections.Generic;
using System.Linq;
using Tower.Core.Models.Abstractions;

namespace Tower.Core.Managers;

public class TowerManager
{
    public List<ITower> Towers { get; set; }

    public void AddTower(ITower tower)
        => Towers.Add(tower);

    public void RemoveTower(ITower tower)
        => Towers.Remove(tower);

    public void UpdateTower(ITower tower)
    {
        if (!Towers.Contains(tower)) return;
        var index = Towers.IndexOf(tower);
        Towers[index] = tower;
    }

    public ITower GetTower(Guid id)
    => Towers.Single(x=>x.Id == id);
}