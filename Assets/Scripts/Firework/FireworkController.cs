using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkController : IInteractiveObject
{
    public GameObject crystal;

    public override void Interact()
    {
        transform.Find("Rocket").gameObject.SetActive(true);
    }
    /*
void LateUpdate()
{
   if (!crystal.activeInHierarchy)
   {
       transform.Find("Rocket").gameObject.SetActive(true);
   }
}
*/
}
