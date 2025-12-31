namespace Tower.Core.Abstractions.Enums;

/// <summary>
/// Перечисление типов башен. Определяет различные типы башен, доступных в игре.
/// </summary>
public enum TowerTypeEnum
{
    /// <summary>Базовая башня - стандартная башня с обычной атакой</summary>
    Basic,
    
    /// <summary>Огненная башня - наносит огненный урон (не реализована)</summary>
    Fire,
    
    /// <summary>Ледяная башня - замедляет врагов (не реализована)</summary>
    Frost,
    
    /// <summary>Бомбардировщик - наносит урон по области (не реализована)</summary>
    Bomber,
}