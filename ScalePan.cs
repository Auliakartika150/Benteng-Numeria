using UnityEngine;
using System.Collections.Generic;

public class ScalePan : MonoBehaviour
{
    public ScaleController otakTimbangan;
    public bool isPiringanKiri;
    public List<BlockValue> balokDiatasSini = new List<BlockValue>();

    void OnCollisionEnter(Collision collision)
    {
        BlockValue blok = collision.gameObject.GetComponentInParent<BlockValue>();
        
        if (blok != null)
        {
            if (!balokDiatasSini.Contains(blok))
            {
                balokDiatasSini.Add(blok);
                otakTimbangan.UpdateBerat();
            }


            if (blok.gameObject.name.Contains("Batu15kg")) 
            {
                collision.transform.SetParent(this.transform);
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        BlockValue blok = collision.gameObject.GetComponentInParent<BlockValue>();
        
        if (blok != null)
        {
            if (balokDiatasSini.Contains(blok))
            {
                balokDiatasSini.Remove(blok);
                otakTimbangan.UpdateBerat();
            }

            if (blok.gameObject.name.Contains("Batu15kg"))
            {
                if (collision.transform.parent == this.transform)
                {
                    collision.transform.SetParent(null);
                }
            }
        }
    }
}