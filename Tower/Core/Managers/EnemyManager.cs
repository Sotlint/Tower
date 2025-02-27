using System;
using System.Collections.Generic;
using System.Linq;
using Tower.Core.Models.Abstractions;

namespace Tower.Core.Managers;

public class EnemyManager
{
    public List<IEnemy> Enemies { get; set; }

    public void AddEnemy(IEnemy tower)
        => Enemies.Add(tower);

    public void RemoveEnemy(IEnemy tower)
        => Enemies.Remove(tower);

    public void UpdateEnemy(IEnemy tower)
    {
        if (!Enemies.Contains(tower)) return;
        var index = Enemies.IndexOf(tower);
        Enemies[index] = tower;
    }

    public IEnemy GetEnemy(Guid id)
        => Enemies.Single(x=>x.Id == id);
}