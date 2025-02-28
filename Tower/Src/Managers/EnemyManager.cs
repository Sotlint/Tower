using System;
using System.Collections.Generic;
using System.Linq;
using Tower.Factories;
using Tower.Models.Abstractions;

namespace Tower.Managers;

public class EnemyManager
{
    private List<IEnemy> Enemies { get; set; } = new();

    public void AddEnemy(List<IEnemy> enemy)
        => Enemies.AddRange(enemy);

    public void AddEnemy(IEnemy enemy)
        => Enemies.Add(enemy);

    public void RemoveEnemy(IEnemy enemy)
        => Enemies.Remove(enemy);

    public void RemoveEnemy(List<IEnemy> enemies)
    {
        foreach (var enemy in enemies)
        {
            RemoveEnemy(enemy);
        }
    }

    public void UpdateEnemy(IEnemy enemy)
    {
        if (!Enemies.Contains(enemy)) return;
        var index = Enemies.IndexOf(enemy);
        Enemies[index] = enemy;
    }

    public IEnemy GetEnemy(Guid id)
        => Enemies.Single(x => x.Id == id);

    public List<IEnemy> GetEnemies() => Enemies;

    public List<IEnemy> GetAliveEnemies()
    {
        RemoveEnemy(Enemies.Where(x => x.IsDefeated()).ToList());
        return Enemies;
    }

    public void SpawnEnemy(int count)
        => AddEnemy(EnemyFactory.CreateEnemy(EnemyTypeEnum.Basic, count));
}