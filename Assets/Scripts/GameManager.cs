using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Ball ball;
    [SerializeField]
    private List<Block> blocks;
    [SerializeField]
    private Wall upWall, lWall, rWall;
    [SerializeField]
    private Bound bound;
    [SerializeField]
    private ScoreManager scoreManager;
    [SerializeField]
    private Rail rail;
    [SerializeField]
    private LevelManager levelManager;

    void Start()
    {
        ball.gameManager = this;
        foreach (var block in blocks) {
            block.gameManager = this;
        }
        upWall.gameManager = this;
        lWall.gameManager = this;
        rWall.gameManager = this;
        bound.gameManager = this;
        scoreManager.gameManager = this;
        rail.gameManager = this;
        if (levelManager != null) {
            // ensure level manager has reference if not assigned
            // levelManager is optional; it will call LoadLevel on Start from UI
        }
    }

    public Ball GetBall() { return ball; }
    public ScoreManager GetScoreManager() { return scoreManager; }
    public void SetLevelManager(LevelManager lm) { levelManager = lm; }
    public void OnBallCollideWall(Collision2D coll) {
        var wall = coll.otherCollider.GetComponent<Wall>();
        ball.OnCollideWall(wall.Index);
    }
    public void OnBallCollideBlock(Collision2D coll) {
        var block = coll.otherCollider.GetComponent<Block>();
        ball.OnCollideBlock(block.Index);
        scoreManager.OnCollideBlock();
        Destroy(block.gameObject);
        CheckWinCondition();
    }
    public void OnBallCollideRail(Collision2D coll) {
        ball.OnCollideRail(rail.Velocity);
    }
    public void OnBallOutofBound(Collision2D coll) {
        Destroy(ball.gameObject);
        // notify level manager if assigned
        if (levelManager != null) levelManager.OnLose();
    }

    private void CheckWinCondition()
    {
        if(blocks.Count == scoreManager.Score) {
            Destroy(ball.gameObject);
            levelManager.OnWin();
        }
    }

    // Retained for compatibility: when using prefab-per-level the LevelManager will instantiate a fresh
    // GameManager prefab. This LoadLevel no longer attempts to reset individual objects.
    public void LoadLevel(int index)
    {
        if (scoreManager != null) scoreManager.ResetScore();
    }

}
