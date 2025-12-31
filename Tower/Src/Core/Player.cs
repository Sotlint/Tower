namespace Tower.Core;

/// <summary>
/// Класс игрока. Управляет экономикой игрока (деньгами).
/// Игрок получает деньги за убийство врагов и тратит их на покупку башен.
/// </summary>
public partial class Player
{
    /// <summary>Текущее количество денег у игрока</summary>
    public int Money { get; private set; }

    /// <summary>
    /// Конструктор игрока. Инициализирует начальное количество денег.
    /// </summary>
    /// <param name="initialMoney">Начальное количество денег (100 монет при старте новой игры)</param>
    public Player(int initialMoney)
    {
        Money = initialMoney;
    }

    /// <summary>
    /// Получить деньги (например, за убийство врага).
    /// </summary>
    /// <param name="amount">Количество денег для добавления</param>
    public void EarnMoney(int amount)
    {
        Money += amount;
    }

    /// <summary>
    /// Потратить деньги (например, на покупку башни).
    /// Деньги списываются только если их достаточно.
    /// </summary>
    /// <param name="amount">Количество денег для списания</param>
    /// <returns>true, если деньги успешно списаны, false - если недостаточно денег</returns>
    public bool SpendMoney(int amount)
    {
        // Проверяем, достаточно ли денег
        if (Money >= amount)
        {
            Money -= amount;
            return true; // Деньги успешно списаны
        }

        return false; // Недостаточно денег
    }
}