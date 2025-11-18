using UnityEngine;

public class maou : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootInterval = 2f;

    private float timer;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0f;
        }
    }

    void Shoot()
    {
        //Debug.Log("sssssssssssssss");
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}