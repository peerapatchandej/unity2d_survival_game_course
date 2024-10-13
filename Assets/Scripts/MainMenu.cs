using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
  [SerializeField]
  private Button start;

  [SerializeField]
  private Button quit;

  [SerializeField]
  private AudioClip buttonClip;

  private AudioSource audioSrc;

  // Start is called before the first frame update
  void Start()
  {
    audioSrc = GetComponent<AudioSource>();

    start.onClick.AddListener(() =>
    {
      StartCoroutine(Delay(() =>
      {
        SceneManager.LoadScene("Game");
      }));
    });

    quit.onClick.AddListener(() =>
    {
      StartCoroutine(Delay(() =>
      {
        Application.Quit();
      }));
    });
  }

  private IEnumerator Delay(Action onClick)
  {
    audioSrc.PlayOneShot(buttonClip);
    yield return new WaitForSeconds(0.2f);
    onClick();
  }
}
