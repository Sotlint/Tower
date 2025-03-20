using System;

namespace Tower.Models.Abstractions.Base;

public interface IHaveIdentity
{
    Guid Id { get; }
}