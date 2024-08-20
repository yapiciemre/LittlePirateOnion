using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb; // Karakterin fiziksel bileþeni
    private Animator anim; // Karakterin animatörü

    [SerializeField] private float moveSpeed; // Karakterin hareket hýzý
    [SerializeField] private float jumpForce; // Zýplama kuvveti
    private float xInput; // Yatay hareket girdi deðeri

    [Header("Collision Check")]
    [SerializeField] private float groundCheckRadius; // Zemin kontrol yarýçapý
    [SerializeField] private Transform groundCheck; // Zemin kontrol noktasý
    [SerializeField] private LayerMask whatIsGround; // Zemin maskesi
    private bool isGrounded; // Karakterin zemine temas edip etmediðini kontrol eder

    private bool facingRight = true; // Karakterin saða mý sola mý baktýðýný belirler

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D bileþenini al
        anim = GetComponent<Animator>(); // Animator bileþenini al
    }

    void Update()
    {
        AnimationControllers(); // Animasyonlarý güncelle
        CollisionChecks(); // Çarpýþma kontrollerini yap
        FlipController(); // Karakterin yönünü kontrol et

        xInput = Input.GetAxisRaw("Horizontal"); // Yatay hareket girdisini al

        Movement(); // Hareket fonksiyonunu çaðýr

        if (Input.GetKeyDown(KeyCode.Space)) // Boþluk tuþuna basýldýðýnda zýpla
            Jump();
    }

    // Animasyon kontrollerini yapar
    void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0; // Karakter hareket ediyor mu?
        anim.SetBool("isMoving", isMoving); // Hareket animasyonunu güncelle

        anim.SetFloat("yVelocity", rb.velocity.y); // Düþme ve zýplama animasyonunu güncelle
        anim.SetBool("isGrounded", isGrounded); // Zemin temasý animasyonunu güncelle
    }

    // Zýplama fonksiyonu
    private void Jump()
    {
        if (isGrounded) // Karakter zemin üzerindeyse zýpla
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // Hareket fonksiyonu
    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y); // Yatay hareketi güncelle
    }

    // Karakterin yönünü kontrol eder
    private void FlipController()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Fare pozisyonunu dünya koordinatlarýna çevir

        if (mousePos.x < transform.position.x && facingRight) // Fare sol tarafa yönelmiþse
            Flip();
        else if (mousePos.x > transform.position.x && !facingRight) // Fare sað tarafa yönelmiþse
            Flip();
    }

    // Karakterin yönünü deðiþtirir
    private void Flip()
    {
        facingRight = !facingRight; // Yönü deðiþtir
        transform.Rotate(0, 180, 0); // Karakteri 180 derece döndür
    }

    // Çarpýþma kontrollerini yapar
    private void CollisionChecks()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); // Zemin temasýný kontrol et
    }

    // Gizmo'lar ile zemin kontrol alanýný gösterir
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); // Zemin kontrol alanýný çiz
    }
}
