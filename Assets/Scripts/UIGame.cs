using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class UIGame : MonoBehaviour
{
  [SerializeField]
  private Image expBar;

  [SerializeField]
  private TextMeshProUGUI levelText;

  [SerializeField]
  private GameObject levelUpObj;

  [SerializeField]
  private Button meleeSpeed;

  [SerializeField]
  private TextMeshProUGUI meleeSpeedLvText;

  [SerializeField]
  private Button gunFireRate;

  [SerializeField]
  private TextMeshProUGUI gunFireRateLvText;

  [SerializeField]
  private GameObject gameOverObj;

  [SerializeField]
  private Button restart;

  [SerializeField]
  private Button mainMenu;

  [SerializeField]
  private AudioClip levelUpClip;

  [SerializeField]
  private AudioClip buttonClip;

  [SerializeField]
  private AudioClip gameOverClip;

  [SerializeField]
  private AudioSource bgm;

  private int meleeSpeedLv = 1;
  private int gunFireRateLv = 1;

  private AudioSource audioSrc;

  private void Start()
  {
    audioSrc = GetComponent<AudioSource>();
  }

  public void LevelUpSetup(Action onMeleeSpeedUp, Action onFireRateUp)
  {
    meleeSpeedLvText.text = "Lv." + meleeSpeedLv.ToString();

    meleeSpeed.onClick.AddListener(() =>
    {
      audioSrc.PlayOneShot(buttonClip);
      meleeSpeedLv++;
      meleeSpeedLvText.text = "Lv." + meleeSpeedLv.ToString();
      onMeleeSpeedUp();
      ShowLevelUp(false);
    });

    gunFireRate.onClick.AddListener(() =>
    {
      audioSrc.PlayOneShot(buttonClip);
      gunFireRateLv++;
      gunFireRateLvText.text = "Lv." + gunFireRateLv.ToString();
      onFireRateUp();
      ShowLevelUp(false);
    });

    restart.onClick.AddListener(() =>
    {
      StartCoroutine(Delay(() =>
      {
        SceneManager.LoadScene("Game");
      }));
    });

    mainMenu.onClick.AddListener(() =>
    {
      StartCoroutine(Delay(() =>
      {
        SceneManager.LoadScene("MainMenu");
      }));
    });
  }

  public void ShowLevelUp(bool value)
  {
    if (value == true) audioSrc.PlayOneShot(levelUpClip);
    levelUpObj.SetActive(value);
  }

  public void SetExp(float exp)
  {
    expBar.fillAmount = exp;
  }

  public void SetLevel(int level)
  {
    levelText.text = "Lv." + level.ToString();
  }

  public void ShowGameOver(bool value)
  {
    if (value == true)
    {
      bgm.Stop();
      audioSrc.PlayOneShot(gameOverClip);
    }

    gameOverObj.SetActive(value);
  }

  private IEnumerator Delay(Action onClick)
  {
    audioSrc.PlayOneShot(buttonClip);
    yield return new WaitForSeconds(0.2f);
    onClick();
  }
}
