namespace Tower.Core.Abstractions.Enums;

/// <summary>
/// Перечисление типов врагов. Определяет различные типы врагов в игре.
/// </summary>
public enum EnemyTypeEnum
{
    /// <summary>Быстрый враг - высокая скорость, низкое здоровье (не реализован)</summary>
    Fast,
    
    /// <summary>Базовый враг - стандартные характеристики</summary>
    Basic,
    
    /// <summary>Бронированный враг - высокое здоровье, низкая скорость (не реализован)</summary>
    Armored,
    
    /// <summary>Босс - очень высокое здоровье и урон (не реализован)</summary>
    Boss
}