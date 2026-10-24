using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Komponen Engsel")]
    public Transform engselKiri;
    public Transform engselKanan;

    [Header("Pengaturan")]
    public float sudutBuka = 90f;
    public float kecepatan = 2.0f;
    
    private bool sedangMembuka = false;
    private Quaternion targetKiri;
    private Quaternion targetKanan;

    void Start()
    {
        if (engselKiri != null && engselKanan != null)
        {
            targetKiri = engselKiri.rotation * Quaternion.Euler(0, -sudutBuka, 0); 
            targetKanan = engselKanan.rotation * Quaternion.Euler(0, sudutBuka, 0);
        }
    }

    public void BukaPintu()
    {
        sedangMembuka = true;
    }

    void Update()
    {
        if (sedangMembuka)
        {
            if (engselKiri != null)
                engselKiri.rotation = Quaternion.Slerp(engselKiri.rotation, targetKiri, Time.deltaTime * kecepatan);

            if (engselKanan != null)
                engselKanan.rotation = Quaternion.Slerp(engselKanan.rotation, targetKanan, Time.deltaTime * kecepatan);
        }
    }
}