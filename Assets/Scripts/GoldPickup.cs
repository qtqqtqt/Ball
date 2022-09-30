using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour
{
    [SerializeField] int goldAmount;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<GoldHandler>().IncreaseGold(goldAmount);
            Destroy(gameObject);
        }
    }
}
