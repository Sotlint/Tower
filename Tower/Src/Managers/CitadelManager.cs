using Microsoft.Xna.Framework;
using Tower.Core;

namespace Tower.Managers;

public class CitadelManager
{
    public CitadelManager(Citadel citadel)
    {
        Citadel = citadel;
    }

    private Citadel Citadel { get; set; }

    public Vector2? GetCitadelPosition()
        => Citadel.Position;

    public Citadel GetCitadel()
        => Citadel;
}