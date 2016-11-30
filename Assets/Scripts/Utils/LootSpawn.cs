using UnityEngine;
using System.Collections;




public class LootSpawn : MonoBehaviour
{

    private GameObject currentLoot;
    
    public GameObject CurrentLoot
    {
        get
        {
            return currentLoot;
        }
        set
        {
            if (currentLoot)
            {
                DeapthManager.Instance.RemoveActor(currentLoot.gameObject);
                currentLoot.gameObject.SetActive(false);
            }
            currentLoot = value;
        }
    }

    

}
