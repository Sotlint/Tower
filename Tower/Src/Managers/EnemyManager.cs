using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions;
using Tower.Core.Abstractions.Enums;
using Tower.Factories;

namespace Tower.Managers;

/// <summary>
/// Менеджер врагов. Управляет коллекцией всех врагов на карте.
/// Позволяет добавлять, удалять, обновлять и получать врагов, а также создавать новых врагов
/// с помощью фабрики <seealso cref="EnemyFactory"/>.
/// </summary>
public class EnemyManager
{
    /// <summary>Список всех врагов на карте</summary>
    private List<IEnemy> Enemies { get; set; } = new();

    /// <summary>
    /// Добавить врагов на карту (внутренний метод).
    /// </summary>
    /// <param name="enemy">Список врагов для добавления</param>
    private void AddEnemy(List<IEnemy> enemy)
        => Enemies.AddRange(enemy);

    /// <summary>
    /// Удалить врага с карты (внутренний метод).
    /// </summary>
    /// <param name="enemy">Враг для удаления</param>
    private void RemoveEnemy(IEnemy enemy)
        => Enemies.Remove(enemy);
    
    /// <summary>
    /// Обновление всех врагов. Вызывается каждый кадр во время активной волны.
    /// Обновляет движение врагов, проверяет их здоровье и начисляет деньги при смерти.
    /// </summary>
    /// <param name="gameTime">Время игры для синхронизации</param>
    public void UpdateEnemies(GameTime gameTime)
    {
        // Проходим по списку врагов в обратном порядке для безопасного удаления
        for (var i = Enemies.Count - 1; i >= 0; i--)
        {
            // Проверяем, уничтожен ли враг
            if (Enemies[i].IsDie())
            {
                // Начисляем игроку деньги за убийство врага
                GameManager.Player.EarnMoney(Enemies[i].Reward);
                // Начисляем очки за убийство врага (1 очко за каждого врага)
                GameManager.UpdateScore(1);
                // Удаляем мертвого врага
                RemoveEnemy(Enemies[i]);
            }
            else
            {
                // Обновляем живого врага (движение, атака цитадели)
                Enemies[i].Update(gameTime);
            }
        }
    }

    /// <summary>
    /// Отрисовка всех врагов на карте. Отрисовывает спрайты врагов и полосы здоровья.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры</param>
    public void DrawEnemies(SpriteBatch spriteBatch, GameTime gameTime)
    {
        // Отрисовываем каждого врага
        foreach (var enemy in Enemies)
        {
            enemy.Draw(spriteBatch, gameTime);
        }
    }

    /// <summary>
    /// Получить список всех врагов на карте.
    /// Используется для проверки окончания волны и для поиска целей башнями.
    /// </summary>
    /// <returns>Список всех врагов</returns>
    public List<IEnemy> GetEnemies() => Enemies;

    /// <summary>
    /// Создать и добавить врагов на карту. Используется при начале новой волны.
    /// </summary>
    /// <param name="count">Количество врагов для создания</param>
    public void SpawnEnemy(int count)
        => AddEnemy(EnemyFactory.CreateEnemy(EnemyTypeEnum.Basic, count));
}