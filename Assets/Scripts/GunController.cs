using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject player;
    // Update is called once per frame
    void Update()
    {
        RotateGun();
    }

    private void Start()
    {
        Transform playerChildTransform = transform.parent;
        Debug.Log(playerChildTransform.position);
    }

    private void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            
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
