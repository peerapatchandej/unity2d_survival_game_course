using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Learning : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    UnityAction action1 = () =>
    {
      Debug.Log("Hello Unity");
    };

    UnityAction<int> action2 = (number) =>
    {
      Debug.Log("Show number : " + number);
    };

    action1();
    action2(10);
  }

  // Update is called once per frame
  void Update()
  {
    
  }
}
