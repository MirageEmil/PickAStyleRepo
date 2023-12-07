using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public float breakTime;

    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, breakTime);

    }

}
