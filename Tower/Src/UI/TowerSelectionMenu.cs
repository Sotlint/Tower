using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Tower.Core.Abstractions.Enums;
using Tower.Managers;

namespace Tower.UI;

public class TowerSelectionMenu
{
    private readonly List<UIButton<TowerTypeEnum>> _towerButtons = new();
    private Vector2 Position { get; set; } = new(400, 300); // Общая позиция меню

    public Texture2D BackgroundTexture { get; set; }
    public Texture2D FrameTexture { get; set; }

    public TowerSelectionMenu()
    {
        var buttonSize = 64;
        var padding = 10;
        var types = new[] { TowerTypeEnum.Basic /*, TowerTypeEnum.Fire, etc. */ };

        for (var i = 0; i < types.Length; i++)
        {
            var type = types[i];
            var x = (int)Position.X;
            var y = (int)(Position.Y + i * (buttonSize + padding));
            var bounds = new Rectangle(x, y, buttonSize, buttonSize);

            var button = new UIButton<TowerTypeEnum>(bounds, type)
            {
                IconTexture = SpriteManager.TowerSprite,
            };

            _towerButtons.Add(button);
        }
    }

    public void Update(Vector2 mousePosition)
    {
        foreach (var button in _towerButtons)
        {
            button.Update(mousePosition);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var button in _towerButtons)
        {
            button.Draw(spriteBatch);
        }
    }

    public TowerTypeEnum? GetTowerTypeAt(Vector2 mousePosition)
    {
        foreach (var button in _towerButtons)
        {
            if (button.Bounds.Contains(mousePosition))
                return button.Payload;
        }

        return null;
    }
}