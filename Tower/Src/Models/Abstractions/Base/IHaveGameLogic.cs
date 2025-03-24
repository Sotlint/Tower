using Microsoft.Xna.Framework;
using Tower.Managers;

namespace Tower.Models.Abstractions.Base;

public interface IHaveGameLogic
{
    void Update(GameManager gameManager, GameTime gameTime);
}