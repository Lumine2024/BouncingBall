using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Level prefabs")]
    [SerializeField]
    private List<GameObject> levelPrefabs; // prefabs containing GameManager per level

    private GameObject currentLevelInstance;
    private GameManager currentGameManager;

    [Header("UI (optional)")]
    [SerializeField]
    private GameObject menuPanel; // UI panel for menu
    [SerializeField]
    private Dropdown levelDropdown; // choose level index
    [SerializeField]
    private Text statusText; // show win/lose/status
    [SerializeField]
    private Button startButton;

    private int currentLevel = 0;

    void Start()
    {
        // Assume UI references are assigned in the inspector
        startButton.onClick.AddListener(StartSelectedLevel);

        ShowMenu(true);
    }

    public void ShowMenu(bool show)
    {
        // menuPanel must be assigned in inspector
        menuPanel.SetActive(show);
    }

    public void StartSelectedLevel()
    {
        // levelDropdown must be assigned and populated in inspector
        currentLevel = levelDropdown.value;
        StartLevel(currentLevel);
    }

    public void StartLevel(int levelIndex)
    {
        currentLevel = levelIndex;
        ShowMenu(false);
        if (statusText != null) statusText.text = "Playing";
        // Instantiate selected prefab (assume levelPrefabs assigned and populated in inspector).
        // Bounds check for index only.
        if (levelPrefabs.Count > 0) {
            if (levelIndex < 0 || levelIndex >= levelPrefabs.Count) {
                Debug.LogWarning($"LevelManager: invalid level index {levelIndex}");
                return;
            }

            // destroy previous instance (Destroy handles null)
            Destroy(currentLevelInstance);

            var prefab = levelPrefabs[levelIndex];
            currentLevelInstance = Instantiate(prefab);
            currentGameManager = currentLevelInstance.GetComponent<GameManager>();
            // wire back reference so GameManager can call OnWin/OnLose and initialize
            currentGameManager.SetLevelManager(this);
            //currentGameManager.LoadLevel(levelIndex);
        }
    }

    // Restart support removed. Use menu to select and start level again.

    public void OnWin()
    {
        ShowMenu(true);
        if (statusText != null) statusText.text = "Win!";
    }

    public void OnLose()
    {
        ShowMenu(true);
        if (statusText != null) statusText.text = "Lose";
    }
}
