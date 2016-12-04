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
            SoundManager.Instance.PlaySound("Loot_Drop");
            if (currentLoot)
            {
                DeapthManager.Instance.RemoveActor(currentLoot.gameObject);
                currentLoot.gameObject.SetActive(false);
            }
            currentLoot = value;
        }
    }

    

}
