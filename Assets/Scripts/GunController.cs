using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Animator gunAnim;
    [SerializeField] private Transform gun;
    [SerializeField] private float gunDistance = 1f;

    private bool gunFacingRight = true;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int maxBullets = 15;
    private int currentBullets;

    private void Start()
    {
        ReloadGun();
    }

    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        gun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gunDistance, 0, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0) && HaveBullets())
            Shoot(direction);

        if (Input.GetKeyDown(KeyCode.R))
            ReloadGun();

        GunFlipController(mousePos);
    }


    private void GunFlipController(Vector3 mousePos)
    {
        if (mousePos.x < gun.position.x && gunFacingRight)
            GunFlip();
        else if (mousePos.x > gun.position.x && !gunFacingRight)
            GunFlip();
    }

    private void GunFlip()
    {
        gunFacingRight = !gunFacingRight;
        gun.localScale = new Vector3(gun.localScale.x, gun.localScale.y * -1, gun.localScale.z);
    }

    public void Shoot(Vector3 direction)
    {
        gunAnim.SetTrigger("Shoot");
        UI.instance.UpdateAmmoInfo(currentBullets, maxBullets);

        GameObject newBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
        newBullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * bulletSpeed;

        Destroy(newBullet, 7);
    }

    private void ReloadGun()
    {
        currentBullets = maxBullets;
        UI.instance.UpdateAmmoInfo(currentBullets, maxBullets);
    }

    public bool HaveBullets()
    {
        if (currentBullets <= 0)
        {
            return false;
        }

        currentBullets--;
        return true;
    }
}
