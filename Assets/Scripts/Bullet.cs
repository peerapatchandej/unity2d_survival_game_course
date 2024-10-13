using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  private float speed = 8f;
  private float direction = 0f;
  private bool isSetup = false;
  private GameController controller;

  private void Start()
  {
    GameObject controllerObj = GameObject.FindGameObjectWithTag("GameController");
    controller = controllerObj.GetComponent<GameController>();
  }

  public void Setup(float directionValue)
  {
    direction = directionValue;
    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, direction * transform.eulerAngles.z);
    isSetup = true;
  }

  // Update is called once per frame
  void Update()
  {
    if (isSetup == true && controller.IsLevelUp() == false)
    {
      transform.position = new Vector2(transform.position.x + speed * direction * Time.deltaTime, transform.position.y);

      Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
      if (screenPos.x < 0 || screenPos.x > Camera.main.pixelWidth)
      {
        Destroy(gameObject);
      }
    }
  }
}
