using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject healthPrefab;

    [SerializeField]
    private GameObject ammoPrefab;

    public IEnumerator RespawnItem(GameObject item)
    {
        var itemType = item.tag;
        var position = item.transform.position;
        var rotation = item.transform.rotation;
        Destroy(item);
        yield return new WaitForSeconds(30);
        if (itemType == "Health")
            Instantiate(healthPrefab, position, rotation);
        else
            Instantiate(ammoPrefab, position, rotation);
    }

}
