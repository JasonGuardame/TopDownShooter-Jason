using Controller;
using Terresquall;
using UnityEngine;
using UnityEngine.Events;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public VirtualJoystick movementJs, fireJs;

    public HealthBarView healthBarView;

    [Header("Panels")]
    public GameObject InGameControlsPanel;
    public GameObject InGameInfoPanel;
    public GameObject MainMenuPanel;

    private MainMenuView mainMenuView;

    public void Awake()
    {
        instance = this;
        mainMenuView = MainMenuPanel.GetComponent<MainMenuView>();
    }

    public void SetupMainMenu()
    {
        mainMenuView.OnStartClickedEvent.AddListener(GameManager.instance.StartGame);
        mainMenuView.OnQuitClickedEvent.AddListener(GameManager.instance.QuitGame);
    }

    public void SetupInGame(PlayerControllerManager player)
    {
        healthBarView.UpdateMaxHealth(player.characterInfo.MaxHealth);
        healthBarView.UpdateCurHealth(player.characterInfo.Health);
    }

    public void ShowMainMenuPanel()
    {
        MainMenuPanel.SetActive(true);
    }

    public void HideMainMenuPanel()
    {
        MainMenuPanel.SetActive(false);
    }

    public void ShowInGamePanels()
    {
        InGameControlsPanel.SetActive(true);
        InGameInfoPanel.SetActive(true);
    }

    public void HideInGamePanels()
    {
        InGameControlsPanel.SetActive(false);
        InGameInfoPanel.SetActive(false);
    }
}
