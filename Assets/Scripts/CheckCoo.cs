using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheckCoo
{

    [SerializeField]
    public static WorldGen WG;

    public static string CheckBlock(Vector3 Pos)
    {

        Vector3 chunk = new Vector3((int)Pos.x / 16,
                                    (int)Pos.y / 16,
                                    (int)Pos.z / 16);

        if(Pos.x < 0 && Pos.x % 16 != 0 ) chunk.x-=1;
        if(Pos.y < 0 && Pos.y % 16 != 0 ) chunk.y-=1;
        if(Pos.z < 0 && Pos.z % 16 != 0 ) chunk.z-=1; 



        try
        {
            Vector3 RelativePos = Pos - (chunk * 16);
            
            //tt ce bordel en gros choppe juste le bloc correspondant dans le chunk correspondant
            string r = WG.Chunks[(int)chunk.x][(int)chunk.y][(int)chunk.z].GetComponent<Chunk>()
            .Blocks[(int)RelativePos.x][(int)RelativePos.y][(int)RelativePos.z].BlockID;
            
            
            //Debug.Log(chunk + RelativePos + "Found in "+Pos+" : " + r);
            
            return r;
        }

        catch
        {
            return "Air";
        }


    }


    


}
