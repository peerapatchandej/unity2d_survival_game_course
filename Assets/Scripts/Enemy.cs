using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
  private enum SpawnDirection
  {
    Left,
    Right,
    Top,
    Bottom
  }

  [SerializeField]
  private SpriteRenderer sprite;

  [SerializeField]
  private Animator anim;

  [SerializeField]
  private GameObject expPrefab;

  [SerializeField]
  private AudioClip hitClip;

  [SerializeField]
  private AudioClip deadClip;

  private float moveSpeed = 1f;

  private int hp = 0;
  private bool isDead = false;
  private CapsuleCollider2D capsuleCollider;
  private float diffPos = 60f;
  private PlayerController playerController;
  private GameController controller;
  private AudioSource audioSrc;

  // Start is called before the first frame update
  void Start()
  {
    GameObject controllerObj = GameObject.FindGameObjectWithTag("GameController");
    controller = controllerObj.GetComponent<GameController>();

    playerController = controller.GetPlayer();
    capsuleCollider = GetComponent<CapsuleCollider2D>();
    audioSrc = GetComponent<AudioSource>();

    SetHp();
    SetPostion();
  }

  // Update is called once per frame
  void Update()
  {
    if (isDead == true || playerController.IsDead() == true || controller.IsLevelUp() == true)
    {
      return;
    }

    transform.position = Vector2.MoveTowards(transform.position, playerController.transform.position, moveSpeed * Time.deltaTime);
    sprite.flipX = transform.position.x > playerController.transform.position.x;
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Bullet"))
    {
      TakeDamage();
      Destroy(collision.gameObject);
    }
    else if (collision.CompareTag("Melee"))
    {
      TakeDamage();
    }
  }

  private void TakeDamage()
  {
    hp--;

    if (hp <= 0)
    {
      isDead = true;
      capsuleCollider.enabled = false;
      anim.SetBool("Dead", true);
      audioSrc.PlayOneShot(deadClip);
      Instantiate(expPrefab, transform.position, Quaternion.identity);
      StartCoroutine(Respawn());
    }
    else
    {
      anim.SetTrigger("Hit");
      audioSrc.PlayOneShot(hitClip);
    }
  }

  private IEnumerator Respawn()
  {
    yield return new WaitForSeconds(1f);

    gameObject.SetActive(false);
    isDead = false;
    capsuleCollider.enabled = true;
    anim.SetBool("Dead", false);
    SetHp();
    SetPostion();
    gameObject.SetActive(true);
  }

  private void SetPostion()
  {
    SpawnDirection dir = (SpawnDirection)UnityEngine.Random.Range(0, Enum.GetNames(typeof(SpawnDirection)).Length);

    if (dir == SpawnDirection.Left)
    {
      transform.position = Camera.main.ScreenToWorldPoint(new Vector2(-diffPos, UnityEngine.Random.Range(0, Camera.main.pixelHeight + 1)));
    }
    else if (dir == SpawnDirection.Right)
    {
      transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth + diffPos, UnityEngine.Random.Range(0, Camera.main.pixelHeight + 1)));
    }
    else if (dir == SpawnDirection.Top)
    {
      transform.position = Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.Random.Range(0, Camera.main.pixelWidth + 1), Camera.main.pixelHeight + diffPos));
    }
    else if (dir == SpawnDirection.Bottom)
    {
      transform.position = Camera.main.ScreenToWorldPoint(new Vector2(UnityEngine.Random.Range(0, Camera.main.pixelWidth + 1), -diffPos));
    }
  }

  private void SetHp()
  {
    hp = UnityEngine.Random.Range(1, 4);
  }
}
