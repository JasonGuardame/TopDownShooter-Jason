using Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public EnemyDatabase enemyDatabase;
    public VisualEffectsDatabase visualEffectsDatabase;

    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;

    public bool inGame;

    public BoxCollider2D movementBounds;
    private PlayerControllerManager currentPlayer;
    private SpawnEnemyController spawnEnemyController;
    public SpawnVfxController spawnVfxController;

    public void Awake()
    {
        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        spawnEnemyController = new SpawnEnemyController();
        spawnEnemyController.Initialize(EnemyPrefab, movementBounds, enemyDatabase);

        spawnVfxController = new SpawnVfxController();
        spawnVfxController.database = visualEffectsDatabase;

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

        GameObject playerGo = GameObject.Instantiate(PlayerPrefab, new Vector2(1,1), Quaternion.identity);

        currentPlayer = playerGo.GetComponent<PlayerControllerManager>();
        currentPlayer.movementBounds = movementBounds;

        UiManager.instance.SetupInGame(currentPlayer);
        currentPlayer.OnReceiveDamageEvent.AddListener(CheckEndGame);
        spawnEnemyController.SetCurrentPlayer(currentPlayer);

        inGame = true;

        //spawnEnemyController.Update(Time.deltaTime);
    }

    public void CheckEndGame(float remainingHp)
    {
        spawnVfxController.SpawnEffects(currentPlayer.transform.position, currentPlayer.onDamagedVFX);
        if (remainingHp > 0.0f) return;

        EndGame();
    }

    void EndGame()
    {
        spawnVfxController.SpawnEffects(currentPlayer.transform.position, currentPlayer.onDeathVFX);

        currentPlayer.DestroyCharacter();
        currentPlayer = null;

        UiManager.instance.ResetInGameUi();
        UiManager.instance.ShowMainMenuPanel();
        UiManager.instance.HideInGamePanels();

        spawnEnemyController.ResetSpawn();

        inGame = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
