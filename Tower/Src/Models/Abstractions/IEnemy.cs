using System;

namespace Tower.Src.Models.Abstractions;

public interface IEnemy
{
    Guid Id { get;}
    
    public void Update(Citadel citadel);
}