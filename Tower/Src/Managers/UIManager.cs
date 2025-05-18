using Tower.UI;

namespace Tower.Managers;

public class UIManager
{
    public TowerSelectionMenu TowerSelectionMenu { get; private set; }
    public UIManager()
    {
        TowerSelectionMenu = new TowerSelectionMenu();
    }
}