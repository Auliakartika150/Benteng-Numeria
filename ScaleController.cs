using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScaleController : MonoBehaviour
{
    [Header("Koneksi Sensor")]
    public ScalePan piringanKiri; 
    public ScalePan piringanKanan; 
    public Transform lenganBeam; 

    [Header("Koneksi Visual")]
    public Transform gantunganKiri;  
    public Transform gantunganKanan; 

    [Header("Koneksi Ending")]
    public GameObject visualTimbanganUtama; 
    public DoorController pintuGerbang;     
    public GameObject efekPartikel;         

    [Header("Matematika")]
    public int nilaiX = 5; 
    
    [Header("Info Berat")]
    public int totalBeratKiri;
    public int totalBeratKanan;

    private Quaternion rotasiAwalGantunganKiri;
    private Quaternion rotasiAwalGantunganKanan;
    private bool prosesMenangBerjalan = false; 

    void Start() 
    {
        if (gantunganKiri != null) rotasiAwalGantunganKiri = gantunganKiri.rotation;
        if (gantunganKanan != null) rotasiAwalGantunganKanan = gantunganKanan.rotation;
        UpdateBerat();
    }

    public void UpdateBerat()
    {
        if (prosesMenangBerjalan) return; 

        totalBeratKiri = HitungTotal(piringanKiri);
        totalBeratKanan = HitungTotal(piringanKanan);

        if (totalBeratKiri == totalBeratKanan && totalBeratKiri > 0)
        {
            StartCoroutine(UrutanKemenangan());
        }
    }

    IEnumerator UrutanKemenangan()
    {
        prosesMenangBerjalan = true;
        
        yield return new WaitForSeconds(3.0f);

        if(efekPartikel != null) 
        {
            efekPartikel.SetActive(true); 
        }

        yield return new WaitForSeconds(0.1f);

        HapusBatuDiPiringan(piringanKiri);
        HapusBatuDiPiringan(piringanKanan);

        visualTimbanganUtama.SetActive(false); 

        if(pintuGerbang != null) pintuGerbang.BukaPintu();
    }

    void HapusBatuDiPiringan(ScalePan piringan)
    {
        if (piringan != null)
        {
            foreach (var blok in piringan.balokDiatasSini.ToArray())
            {
                if (blok != null)
                {
                    blok.gameObject.SetActive(false);
                }
            }
        }
    }

    int HitungTotal(ScalePan piringan)
    {
        int total = 0;
        if (piringan != null) 
        {
            foreach (var blok in piringan.balokDiatasSini)
            {
                if (blok.tipe == BlockValue.TipeBalok.Angka) total += blok.nilai;
                else if (blok.tipe == BlockValue.TipeBalok.VariabelX) total += nilaiX;
            }
        }
        return total;
    }

    void Update()
    {
        float targetSudut = 0;

        if (prosesMenangBerjalan)
        {
            targetSudut = 0f;
        }
        else
        {
            if (totalBeratKiri > totalBeratKanan) targetSudut = 15f; 
            else if (totalBeratKanan > totalBeratKiri) targetSudut = -15f; 
            else targetSudut = 0f; 
        }

        Quaternion targetRotasi = Quaternion.Euler(targetSudut, 0, 0);
        lenganBeam.localRotation = Quaternion.Slerp(lenganBeam.localRotation, targetRotasi, Time.deltaTime * 3f);

        if (gantunganKiri != null) gantunganKiri.rotation = rotasiAwalGantunganKiri; 
        if (gantunganKanan != null) gantunganKanan.rotation = rotasiAwalGantunganKanan;
    }
}