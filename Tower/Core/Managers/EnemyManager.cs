using System;
using System.Collections.Generic;
using System.Linq;
using Tower.Core.Models.Abstractions;

namespace Tower.Core.Managers;

public class EnemyManager
{
    public List<IEnemy> Enemies { get; set; }

    public void AddEnemy(IEnemy enemy)
        => Enemies.Add(enemy);

    public void RemoveEnemy(IEnemy enemy)
        => Enemies.Remove(enemy);

    public void UpdateEnemy(IEnemy enemy)
    {
        if (!Enemies.Contains(enemy)) return;
        var index = Enemies.IndexOf(enemy);
        Enemies[index] = enemy;
    }

    public IEnemy GetEnemy(Guid id)
        => Enemies.Single(x=>x.Id == id);
}