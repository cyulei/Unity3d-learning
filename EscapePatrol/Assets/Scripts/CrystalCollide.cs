using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalCollide : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
            //减少水晶数量
            Singleton<GameEventManager>.Instance.ReduceCrystalNum();
        }
    }
}
