using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;

public class gun : MonoBehaviour
{
    [Header("Gun Settings")]
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public float range = 100;
    public float impactForce = 30f;
    public float damage = 10;
    public int reservedAmmoCapacity = 270;

    public TextMeshProUGUI ammoDisplay;
    public Camera fpsCam;

    public Transform gunBarrel;
    public GameObject bulletTrailPrefab;

    private bool canShoot = true;
    private int currentAmmoInClip;
    private int ammoInReserve;

    private Vector3 originalLocalPosition;
    public Vector3 recoilPosition = new Vector3(0f, 0f, -0.02f);
    public float aimSmoothing = 10;

    void Start()
    {
        originalLocalPosition = transform.localPosition;
        currentAmmoInClip = clipSize;
        ammoInReserve = reservedAmmoCapacity;
    }

    void Update()
    {
        ammoDisplay.text = currentAmmoInClip.ToString();
        DetermineAim();

        if (Input.GetMouseButton(0) && canShoot && currentAmmoInClip > 0)
        {
            canShoot = false;
            currentAmmoInClip--;
            StartCoroutine(ShootGun());
        }
        else if (Input.GetKeyDown(KeyCode.R) && currentAmmoInClip < clipSize && ammoInReserve > 0)
        {
            int amountNeeded = clipSize - currentAmmoInClip;
            if (amountNeeded >= ammoInReserve)
            {
                currentAmmoInClip += ammoInReserve;
                ammoInReserve -= amountNeeded;
            }
            else
            {
                currentAmmoInClip = clipSize;
                ammoInReserve -= amountNeeded;
            }
        }
    }

    void DetermineAim()
    {
        Vector3 target = originalLocalPosition;
        if (Input.GetMouseButton(1)) target = originalLocalPosition + recoilPosition;

        Vector3 desiredPosition = Vector3.Lerp(transform.localPosition, target, Time.deltaTime * aimSmoothing);

        transform.localPosition = desiredPosition;
    }

    IEnumerator ShootGun()
    {
        DetermineRecoil();
        RayCastForShooting();

        yield return new WaitForSeconds(fireRate);
        canShoot = true;
    }

    void DetermineRecoil()
    {
        transform.Translate(recoilPosition, Space.Self);
    }

    void RayCastForShooting()
    {
        RaycastHit hit;
        if (Physics.Raycast(gunBarrel.transform.position, transform.right, out hit))
        {
            // Check if the hit object has the "Elephant" tag
            if (hit.collider.CompareTag("Elephant"))
            {
                // Apply damage to the elephant
                ElephantHealth elephantHealth = hit.collider.GetComponent<ElephantHealth>();
                if (elephantHealth != null)
                {
                    elephantHealth.TakeDamage(damage);
                }
            }
        }

        // Instantiate bullet trail
        GameObject bulletTrail = Instantiate(bulletTrailPrefab, gunBarrel.position, Quaternion.identity);
        bulletTrail.GetComponent<Rigidbody>().AddForce(transform.right * 10000);
    }
}
