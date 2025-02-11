using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public EnemyDatabase enemyDatabase;

    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;

    public bool inGame;

    public BoxCollider2D movementBounds;

    private SpawnEnemyController spawnEnemyController;

    public void Awake()
    {
        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemyController = new SpawnEnemyController();
        spawnEnemyController.Initialize(EnemyPrefab, movementBounds, enemyDatabase);

        LoadUiSetup();
    }

    private void Update()
    {
        spawnEnemyController.Update(Time.deltaTime);
    }

    void LoadUiSetup()
    {
        UiManager.instance.SetupMainMenu();

        UiManager.instance.ShowMainMenuPanel();
        UiManager.instance.HideInGamePanels();
    }

    public void StartGame()
    {
        UiManager.instance.HideMainMenuPanel();
        UiManager.instance.ShowInGamePanels();

        GameObject playerGo = GameObject.Instantiate(PlayerPrefab, Vector2.zero, Quaternion.identity);

        PlayerControllerManager player = playerGo.GetComponent<PlayerControllerManager>();
        player.movementBounds = movementBounds;

        UiManager.instance.SetupInGame(player);
        inGame = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
