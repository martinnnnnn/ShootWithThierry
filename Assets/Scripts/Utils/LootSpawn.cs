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
                currentLoot.gameObject.SetActive(false);
            }
            currentLoot = value;
        }
    }

    

}
