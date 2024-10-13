using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
  [SerializeField]
  private GameObject bulletPrefab;

  [SerializeField]
  private Transform spawnBullet;

  private float fireRate = 0f;
  private float fireRateTimer = 0f;
  private float direction = 0f;
  private GameController controller;
  private AudioSource audioSrc;

  // Start is called before the first frame update
  void Start()
  {
    GameObject controllerObj = GameObject.FindGameObjectWithTag("GameController");
    controller = controllerObj.GetComponent<GameController>();
    audioSrc = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {
    if (fireRate == 0f || controller.IsLevelUp() == true)
    {
      return;
    }

    fireRateTimer += Time.deltaTime;
    if (fireRateTimer >= fireRate)
    {
      GameObject bulletObj = Instantiate(bulletPrefab, spawnBullet.position, bulletPrefab.transform.rotation);
      Bullet bullet = bulletObj.GetComponent<Bullet>();
      bullet.Setup(direction);
      fireRateTimer = 0f;
      audioSrc.Play();
    }
  }

  public void SetFireRate(float fireRateValue)
  {
    fireRate = fireRateValue;
  }

  public void ChangeDirection(float directionValue)
  {
    transform.localScale = new Vector2(directionValue, transform.localScale.y);
    direction = directionValue;
  }
}
