using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
  [SerializeField]
  private Animator anim;

  [SerializeField]
  private SpriteRenderer sprite;

  [SerializeField]
  private Gun gun;

  [SerializeField]
  private Melee melee;

  [SerializeField]
  private Image hpBar;

  private float moveSpeed = 2f;
  private float horizontal = 0f;
  private float vertical = 0f;
  private float gunFireRate = 1f;
  private float meleeSpeed = 100f;
  private int hp = 0;
  private int maxHp = 1000;
  private bool isDead = false;
  private int exp = 0;
  private int maxExp = 3;
  private int level = 1;

  private CapsuleCollider2D capsuleCollider;
  private GameController controller;

  // Start is called before the first frame update
  void Start()
  {
    GameObject controllerObj = GameObject.FindGameObjectWithTag("GameController");
    controller = controllerObj.GetComponent<GameController>();

    gun.ChangeDirection(1);
    gun.SetFireRate(gunFireRate);

    melee.SetSpeed(meleeSpeed);

    hp = maxHp;
    hpBar.fillAmount = (float)hp / maxHp;

    capsuleCollider = GetComponent<CapsuleCollider2D>();

    controller.SetExp(exp);
    controller.SetLevel(level);
  }

  // Update is called once per frame
  void Update()
  {
    if (isDead == true || controller.IsLevelUp() == true)
    {
      return;
    }

    horizontal = Input.GetAxisRaw("Horizontal");
    vertical = Input.GetAxisRaw("Vertical");
    transform.position = new Vector2(transform.position.x + moveSpeed * horizontal * Time.deltaTime, transform.position.y + moveSpeed * vertical * Time.deltaTime);

    anim.SetBool("Run", horizontal != 0f ||  vertical != 0f);

    if (horizontal != 0f)
    {
      sprite.flipX = horizontal < 0f;
      gun.ChangeDirection(horizontal);
    }
  }

  private void OnCollisionEnter2D(Collision2D collision)
  {
    TakeDamage(collision);
  }

  private void OnCollisionStay2D(Collision2D collision)
  {
    TakeDamage(collision);
  }

  private void TakeDamage(Collision2D collision)
  {
    if (collision.collider.CompareTag("Enemy"))
    {
      hp--;
      hpBar.fillAmount = (float)hp / maxHp;

      if (hp <= 0) 
      {
        isDead = true;
        capsuleCollider.enabled = false;
        gun.gameObject.SetActive(false);
        melee.gameObject.SetActive(false);
        anim.SetTrigger("Dead");
        controller.GameOver();
      }
    }
  }

  public bool IsDead()
  {
    return isDead;
  }

  public void UpdateExp(int expValue)
  {
    exp += expValue;

    if (exp >= maxExp)
    {
      exp = 0;
      maxExp += 3;
      level++;
      controller.SetExp(exp);
      controller.SetLevel(level);
      controller.LevelUp();
    }
    else
    {
      controller.SetExp((float)exp / maxExp);
    }
  }

  public void SetMeleeSpeed(float rateUp)
  {
    meleeSpeed += meleeSpeed * (rateUp / 100);
    melee.SetSpeed(meleeSpeed);
  }

  public void SetGunFireRate(float rateUp)
  {
    gunFireRate -= gunFireRate * (rateUp / 100);
    gun.SetFireRate(gunFireRate);
  }
}
