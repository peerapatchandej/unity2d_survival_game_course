using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  [SerializeField]
  private GameObject playerPrefab;

  [SerializeField]
  private GameObject enemyPrefab;

  [SerializeField]
  private int enemyCount = 20;

  [SerializeField]
  private UIGame uiGame;

  private PlayerController player;
  private bool isLevelUp = false;

  // Start is called before the first frame update
  void Start()
  {
    GameObject playerObj = Instantiate(playerPrefab, Vector2.zero, Quaternion.identity);
    player = playerObj.GetComponent<PlayerController>();

    for (int i = 1; i <= enemyCount; i++)
    {
      Instantiate(enemyPrefab);
    }

    uiGame.LevelUpSetup(() =>
    {
      player.SetMeleeSpeed(10);
      isLevelUp = false;
    }, () =>
    {
      player.SetGunFireRate(5);
      isLevelUp = false;
    });
  }

  public void LevelUp()
  {
    isLevelUp = true;
    uiGame.ShowLevelUp(true);
  }

  public bool IsLevelUp()
  {
    return isLevelUp;
  }

  public PlayerController GetPlayer()
  {
    return player;
  }

  public void SetExp(float exp)
  {
    uiGame.SetExp(exp);
  }

  public void SetLevel(int level)
  {
    uiGame.SetLevel(level);
  }

  public void GameOver()
  {
    uiGame.ShowGameOver(true);
  }
}
