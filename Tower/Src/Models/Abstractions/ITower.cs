using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace Tower.Models.Abstractions;

public interface ITower : IBuilding
{


    public void Update(List<IEnemy> enemies);
}