using System;

namespace Tower.Core.Abstractions.Base;

/// <summary>
/// Интерфейс для объектов, которые имеют уникальный идентификатор.
/// Реализуется всеми игровыми объектами для их идентификации и управления.
/// </summary>
public interface IHaveIdentity
{
    /// <summary>Уникальный идентификатор объекта (GUID)</summary>
    Guid Id { get; }
}