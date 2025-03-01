using System;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Models.Abstractions;

public interface IBuilding
{
    Guid Id { get; }
    
    Texture2D Sprite { get; }
    
    public void Update();
}