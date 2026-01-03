
using Tower.Core.Abstractions.Enums;

namespace Tower.Managers;

/// <summary>
/// Менеджер состояний игры. Хранит в себе текущее состояние и предыдущее состояние игровой сессии.
/// Предоставляет функциональность для смены состояния игры и отслеживания переходов между состояниями.
/// Используется для управления переходами между Menu, Playing, Planning, Paused и GameOver.
/// </summary>
public static class GameStateManager
{
    /// <summary>
    /// Текущее состояние игры. Определяет, какая логика обновления и отрисовки должна выполняться.
    /// </summary>
    public static GameStateEnum CurrentState { get; private set; } = GameStateEnum.Menu;

    /// <summary>
    /// Предыдущее состояние игры. Используется для возврата из паузы в предыдущее состояние.
    /// Может быть null, если предыдущего состояния нет.
    /// </summary>
    public static GameStateEnum? PreviousState { get; private set; } = null;

    /// <summary>
    /// Изменение состояния игры. Сохраняет текущее состояние как предыдущее
    /// и устанавливает новое состояние.
    /// </summary>
    /// <param name="newState">Новое состояние игры</param>
    public static void ChangeState(GameStateEnum newState)
    {
        PreviousState = CurrentState; // Сохраняем текущее состояние как предыдущее
        CurrentState = newState; // Устанавливаем новое состояние
    }
}