using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpCoin : MonoBehaviour
{
  [SerializeField]
  private int exp = 1;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Player"))
    {
      PlayerController player = collision.GetComponent<PlayerController>();
      player.UpdateExp(exp);
      Destroy(gameObject);
    }
  }
}
