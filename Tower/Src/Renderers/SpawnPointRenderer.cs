using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;

namespace Tower.Renderers;

/// <summary>
/// Рендерер точек спавна врагов. Отображает визуальные индикаторы точек спавна
/// на стадии планирования с пульсирующим эффектом и количеством врагов.
/// </summary>
public static class SpawnPointRenderer
{
    /// <summary>Кэшированная текстура точки спавна (создается один раз)</summary>
    private static Texture2D _spawnPointTexture;
    
    /// <summary>
    /// Отрисовка всех точек спавна с пульсирующим эффектом и количеством врагов.
    /// Отображается только на стадии планирования.
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="gameTime">Время игры для синхронизации анимации пульсации</param>
    public static void DrawSpawnPoints(SpriteBatch spriteBatch, GameTime gameTime)
    {
        var spawnManager = GameManager.SpawnPointManager;
        if (spawnManager == null) return;
        
        // Получаем план спавна для следующей волны (только активные точки)
        var spawnPlan = spawnManager.GetNextWaveSpawnPlan();
        if (spawnPlan.Count == 0) return;
        
        // Вычисляем пульсирующий эффект (от 0.8 до 1.2 размера, от 150 до 255 прозрачности)
        var pulseTime = (float)gameTime.TotalGameTime.TotalSeconds * 2.0f; // Скорость пульсации (2 цикла в секунду)
        var pulseScale = 1.0f + (float)Math.Sin(pulseTime) * 0.2f; // От 0.8 до 1.2
        var pulseAlpha = 150 + (int)((Math.Sin(pulseTime) + 1.0) * 0.5 * 105); // От 150 до 255
        
        // Создаем или получаем кэшированную текстуру для точки спавна
        if (_spawnPointTexture == null || _spawnPointTexture.IsDisposed)
        {
            _spawnPointTexture = CreateSpawnPointTexture(spriteBatch.GraphicsDevice);
        }
        var font = SpriteManager.DefaultFont;
        
        spriteBatch.Begin(SpriteSortMode.BackToFront);
        
        // Отрисовываем только активные точки спавна из плана следующей волны
        foreach (var kvp in spawnPlan)
        {
            var spawnPoint = kvp.Key;
            var enemyCount = kvp.Value; // Количество врагов для этой конкретной точки
            
            // Размер точки спавна с учетом пульсации (увеличен)
            var baseSize = 40.0f; // Увеличено с 20 до 40
            var currentSize = baseSize * pulseScale;
            
            // Яркий красный цвет с пульсирующей прозрачностью
            var color = new Color((byte)255, (byte)0, (byte)0, (byte)pulseAlpha);
            
            // Вычисляем позицию для отрисовки (центрирование)
            var drawPosition = new Vector2(
                spawnPoint.X - currentSize / 2,
                spawnPoint.Y - currentSize / 2
            );
            
            // Отрисовываем пульсирующую красную точку
            spriteBatch.Draw(
                _spawnPointTexture,
                drawPosition,
                null,
                color,
                0f,
                Vector2.Zero,
                currentSize / _spawnPointTexture.Width, // Масштаб для пульсации
                SpriteEffects.None,
                TileRenderer.LayerDepth.UI - 0.01f // Чуть ниже UI, но выше игровых объектов
            );
            
            // Отрисовываем цифру с количеством врагов для этой точки (белый цвет, меньший размер)
            if (font != null)
            {
                var countText = enemyCount.ToString();
                // Уменьшаем размер шрифта (масштаб 0.6)
                var scale = 0.6f;
                var textSize = font.MeasureString(countText) * scale;
                var textPosition = new Vector2(
                    spawnPoint.X - textSize.X / 2,
                    spawnPoint.Y - textSize.Y / 2
                );
                
                // Отрисовываем текст с черной обводкой для лучшей читаемости
                // Сначала черная обводка (4 направления) с учетом масштаба
                var outlineColor = Color.Black;
                var outlineOffset = 1.5f;
                spriteBatch.DrawString(font, countText, textPosition + new Vector2(-outlineOffset, 0), outlineColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, countText, textPosition + new Vector2(outlineOffset, 0), outlineColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, countText, textPosition + new Vector2(0, -outlineOffset), outlineColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, countText, textPosition + new Vector2(0, outlineOffset), outlineColor, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                
                // Затем белый текст поверх
                spriteBatch.DrawString(font, countText, textPosition, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }
        
        spriteBatch.End();
    }
    
    /// <summary>
    /// Создает текстуру для точки спавна (красный круг).
    /// </summary>
    /// <param name="graphicsDevice">Графическое устройство для создания текстуры</param>
    /// <returns>Текстура красного круга</returns>
    private static Texture2D CreateSpawnPointTexture(GraphicsDevice graphicsDevice)
    {
        // Кэшируем текстуру, чтобы не создавать её каждый кадр
        // В реальной игре лучше создать её один раз при инициализации
        const int size = 40; // Увеличено с 20 до 40 для большей точки
        var texture = new Texture2D(graphicsDevice, size, size);
        var colorData = new Color[size * size];
        
        var center = size / 2.0f;
        var radius = size / 2.0f - 1;
        
        // Создаем круглую текстуру
        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                var distance = Math.Sqrt((x - center) * (x - center) + (y - center) * (y - center));
                if (distance <= radius)
                {
                    // Красный цвет с градиентом к центру
                    var alpha = (float)(1.0 - (distance / radius));
                    colorData[y * size + x] = new Color((byte)255, (byte)0, (byte)0, (byte)(alpha * 255));
                }
                else
                {
                    colorData[y * size + x] = Color.Transparent;
                }
            }
        }
        
        texture.SetData(colorData);
        return texture;
    }
}

