using System;

namespace Tower.Core.Abstractions.Base;

public interface IHaveIdentity
{
    Guid Id { get; }
}