using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGen : MonoBehaviour
{
    [SerializeField]
    private GameObject chunk;

    public Dictionary<int, Dictionary<int, Dictionary<int, GameObject>>> Chunks = new Dictionary<int, Dictionary<int, Dictionary<int, GameObject>>>() ;

    [SerializeField]
    Quaternion Orientation;


    // Start is called before the first frame update
    void Start()
    {
        CheckCoo.WG = this;

        for(int x = -4; x < 5; x++)
        {
            Chunks.Add(x, new Dictionary<int, Dictionary<int, GameObject>>());
            for(int y = -4; y < 5; y++)
            {
                Chunks[x].Add(y, new Dictionary<int, GameObject>());
                for(int z = -2; z < 5; z++)
                {
                    Chunks[x][y][z] = Instantiate(chunk, new Vector3(x, y, z), Orientation);

                    //Debug.Log(Chunks[x][y][z].GetComponent<Chunk>().Position);
                }
                
            }
        }
        
        

        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
    }
}
