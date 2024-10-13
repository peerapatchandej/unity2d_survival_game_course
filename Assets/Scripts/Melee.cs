using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
  private float speed = 0;
  private GameController controller;

  // Start is called before the first frame update
  void Start()
  {
    GameObject controllerObj = GameObject.FindGameObjectWithTag("GameController");
    controller = controllerObj.GetComponent<GameController>();
  }

  // Update is called once per frame
  void Update()
  {
    if (speed == 0 || controller.IsLevelUp() == true)
    {
      return;
    }

    transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + speed * Time.deltaTime);
  }

  public void SetSpeed(float speedValue)
  {
    speed = speedValue;
  }
}
