using UnityEngine;

public class BlockValue : MonoBehaviour
{
    public enum TipeBalok { Angka, VariabelX }
    
    [Header("Identitas Batu")]
    public TipeBalok tipe; 
    public int nilai = 1;
}