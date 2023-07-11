using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    [SerializeField]
    private GameObject cube;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(cube, new Vector3(0, 0, 0), null);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
