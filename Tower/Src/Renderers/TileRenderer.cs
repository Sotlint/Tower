using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Managers;

namespace Tower.Renderers;

/// <summary>
/// Рендерер тайлов земли. Отвечает за отрисовку тайлов земли на всю карту
/// на уровне 0 (самый дальний слой).
/// </summary>
public static class TileRenderer
{
    /// <summary>
    /// Константы уровней слоев для отрисовки объектов.
    /// Используются для правильной сортировки объектов по глубине.
    /// </summary>
    public static class LayerDepth
    {
        /// <summary>Уровень 0: Земля (самый дальний слой)</summary>
        public const float Ground = 0.0f;
        
        /// <summary>Уровень 1: Башни и цитадель</summary>
        public const float Buildings = 0.5f;
        
        /// <summary>Уровень 2: Снаряды</summary>
        public const float Projectiles = 0.8f;
        
        /// <summary>Уровень 3: UI (самый ближний слой)</summary>
        public const float UI = 1.0f;
    }
    
    /// <summary>
    /// Отрисовка тайлов земли на всю карту. Разбивает весь экран на сетку 128x128 пикселей
    /// и отрисовывает спрайт земли в каждой ячейке на уровне 0 (самый дальний слой).
    /// </summary>
    /// <param name="spriteBatch">SpriteBatch для отрисовки</param>
    /// <param name="graphicsDevice">Графическое устройство для получения размеров экрана</param>
    public static void DrawGroundTiles(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
    {
        // Получаем размеры экрана
        var screenWidth = graphicsDevice.Viewport.Width;
        var screenHeight = graphicsDevice.Viewport.Height;
        
        // Получаем текстуру земли
        var groundTexture = SpriteManager.GroundSprite;
        if (groundTexture == null) return; // Если спрайт не загружен, не отрисовываем
        
        // Размер каждой ячейки сетки в пикселях (128x128)
        const int tileSize = 128;
        
        // Вычисляем количество ячеек по горизонтали и вертикали
        // Используем Math.Ceiling для округления вверх, чтобы покрыть весь экран
        var tilesX = (int)Math.Ceiling((double)screenWidth / tileSize);
        var tilesY = (int)Math.Ceiling((double)screenHeight / tileSize);
        
        spriteBatch.Begin(SpriteSortMode.BackToFront); // Используем сортировку по глубине
        
        // Отрисовываем спрайт земли в каждой ячейке сетки
        for (var y = 0; y < tilesY; y++)
        {
            for (var x = 0; x < tilesX; x++)
            {
                // Вычисляем позицию ячейки (верхний левый угол)
                var tileX = x * tileSize;
                var tileY = y * tileSize;
                
                // Создаем прямоугольник назначения размером 128x128
                var destinationRectangle = new Rectangle(
                    tileX, // X позиция ячейки
                    tileY, // Y позиция ячейки
                    tileSize, // Ширина ячейки
                    tileSize // Высота ячейки
                );
                
                // Отрисовываем спрайт земли в ячейке на уровне 0
                spriteBatch.Draw(
                    groundTexture, // Текстура земли
                    destinationRectangle, // Прямоугольник назначения (128x128)
                    null, // Область текстуры (null = вся текстура)
                    Color.White, // Цвет (без изменений)
                    0f, // Угол поворота
                    Vector2.Zero, // Точка привязки (не используется при использовании Rectangle)
                    SpriteEffects.None, // Эффекты отображения
                    LayerDepth.Ground // Глубина слоя (уровень 0)
                );
            }
        }
        
        spriteBatch.End();
    }
}

