using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public float breakTime;
    [Range(0f, 1f)]
    public float spawnChance;

    public GameObject[] pickups;

    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, breakTime);

    }

    private void OnDestroy()
    {
        if(pickups.Length > 0 && Random.value < spawnChance)
        {
            int randomIndex = Random.Range(0, pickups.Length);

            Instantiate(pickups[randomIndex], transform.position, Quaternion.identity);

        }

    }

}
