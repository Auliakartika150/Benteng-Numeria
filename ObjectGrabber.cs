using UnityEngine;

public class ObjectGrabber : MonoBehaviour
{
    [Header("Pengaturan")]
    public Transform tempatPegang; 
    public float jangkauan = 3.0f;
    public string namaTag = "Grabbable"; 
    public LayerMask layerPenghalang = ~0; 

    public float kecepatanGerak = 10f; 

    private GameObject bendaTerpegang; 
    private Rigidbody rbBenda; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (bendaTerpegang == null) CobaAmbil();
            else Lepaskan();
        }
    }
    void LateUpdate()
    {
        if (bendaTerpegang != null)
        {
            UpdatePosisiBenda();
        }
    }

    void UpdatePosisiBenda()
    {
        float jarakIdeal = Vector3.Distance(transform.position, tempatPegang.position);
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Vector3 targetPosisi;

        if (Physics.Raycast(ray, out hit, jarakIdeal, layerPenghalang))
        {
            targetPosisi = hit.point - (transform.forward * 0.2f); 
        }
        else
        {
            targetPosisi = tempatPegang.position;
        }

        bendaTerpegang.transform.position = Vector3.Lerp(bendaTerpegang.transform.position, targetPosisi, Time.deltaTime * kecepatanGerak);

        bendaTerpegang.transform.rotation = tempatPegang.rotation;
    }

    void CobaAmbil()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, jangkauan))
        {
            if (hit.collider.CompareTag(namaTag))
            {
                rbBenda = hit.collider.GetComponentInParent<Rigidbody>();
                if (rbBenda != null)
                {
                    bendaTerpegang = rbBenda.gameObject;
                    rbBenda.isKinematic = true; 
                    
                    Physics.IgnoreCollision(GetComponentInParent<Collider>(), bendaTerpegang.GetComponentInChildren<Collider>(), true);
                }
            }
        }
    }

    void Lepaskan()
    {
        if (bendaTerpegang != null)
        {
            if (rbBenda != null) 
            {
                rbBenda.isKinematic = false;
                rbBenda.linearVelocity = transform.forward * 2f; 
            }

            Physics.IgnoreCollision(GetComponentInParent<Collider>(), bendaTerpegang.GetComponentInChildren<Collider>(), false);

            bendaTerpegang = null;
            rbBenda = null;
        }
    }
}