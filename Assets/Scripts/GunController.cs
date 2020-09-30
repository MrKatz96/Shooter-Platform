using UnityEngine;

public class GunController : MonoBehaviour
{
    
    public GameObject projectile;
    public Transform shotPoint;
    private float timeBetweenShots;
    public float startTimeBetweenShots;
    // Update is called once per frame
    void Update()
    {
        RotateGun();
        Shoot();
    }

    private void Start()
    {

    }

    private void Shoot()
    {
     
        if(timeBetweenShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(projectile, shotPoint.position, transform.rotation);
                timeBetweenShots = startTimeBetweenShots;
                
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
            }
        }
       
    }

    private void RotateGun()
    {
     
        //Get the difference from the mouse pointer and the gun position
        Vector3 mousePosition = Input.mousePosition;
        Vector3 gunPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.x = mousePosition.x - gunPosition.x;
        mousePosition.y = mousePosition.y - gunPosition.y;
        float gunAngle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;

        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < transform.parent.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(180f, 0f, -gunAngle));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, gunAngle));
        }
    }
    
}
