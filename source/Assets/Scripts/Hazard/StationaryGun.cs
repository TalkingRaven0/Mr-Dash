using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryGun : MonoBehaviour
{
    public GameObject bullet;
    public float firedelay;
    public float fireinterval;
    private float firecd;

    private void Awake()
    {
        firecd = firedelay;
    }

    private void Update()
    {
        if (firecd <= 0)
            Fire();
        else
            firecd -= Time.deltaTime;
    }

    void Fire()
    {
        firecd = fireinterval;
        FindObjectOfType<AudioManager>().Play("Gunshot");
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
