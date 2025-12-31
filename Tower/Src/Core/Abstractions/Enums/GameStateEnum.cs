namespace Tower.Core.Abstractions.Enums;

/// <summary>
/// Перечисление состояний игры. Определяет текущее состояние игрового цикла.
/// </summary>
public enum GameStateEnum
{
    /// <summary>Главное меню - отображается при запуске игры</summary>
    Menu,
    
    /// <summary>Игра окончена - отображается когда цитадель уничтожена</summary>
    GameOver,
    
    /// <summary>Планирование - фаза размещения башен между волнами</summary>
    Planning,
    
    /// <summary>Игра - активная волна врагов</summary>
    Playing,
    
    /// <summary>Пауза - игра приостановлена (нажата ESC)</summary>
    Paused,
}