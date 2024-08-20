using UnityEngine;

public class MoveController : MonoBehaviour
{
    private Rigidbody2D rb; // Karakterin fiziksel bile�eni
    private Animator anim; // Karakterin animat�r�

    [SerializeField] private float moveSpeed; // Karakterin hareket h�z�
    [SerializeField] private float jumpForce; // Z�plama kuvveti
    private float xInput; // Yatay hareket girdi de�eri

    [Header("Collision Check")]
    [SerializeField] private float groundCheckRadius; // Zemin kontrol yar��ap�
    [SerializeField] private Transform groundCheck; // Zemin kontrol noktas�
    [SerializeField] private LayerMask whatIsGround; // Zemin maskesi
    private bool isGrounded; // Karakterin zemine temas edip etmedi�ini kontrol eder

    private bool facingRight = true; // Karakterin sa�a m� sola m� bakt���n� belirler

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D bile�enini al
        anim = GetComponent<Animator>(); // Animator bile�enini al
    }

    void Update()
    {
        AnimationControllers(); // Animasyonlar� g�ncelle
        CollisionChecks(); // �arp��ma kontrollerini yap
        FlipController(); // Karakterin y�n�n� kontrol et

        xInput = Input.GetAxisRaw("Horizontal"); // Yatay hareket girdisini al

        Movement(); // Hareket fonksiyonunu �a��r

        if (Input.GetKeyDown(KeyCode.Space)) // Bo�luk tu�una bas�ld���nda z�pla
            Jump();
    }

    // Animasyon kontrollerini yapar
    void AnimationControllers()
    {
        bool isMoving = rb.velocity.x != 0; // Karakter hareket ediyor mu?
        anim.SetBool("isMoving", isMoving); // Hareket animasyonunu g�ncelle

        anim.SetFloat("yVelocity", rb.velocity.y); // D��me ve z�plama animasyonunu g�ncelle
        anim.SetBool("isGrounded", isGrounded); // Zemin temas� animasyonunu g�ncelle
    }

    // Z�plama fonksiyonu
    private void Jump()
    {
        if (isGrounded) // Karakter zemin �zerindeyse z�pla
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    // Hareket fonksiyonu
    private void Movement()
    {
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y); // Yatay hareketi g�ncelle
    }

    // Karakterin y�n�n� kontrol eder
    private void FlipController()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Fare pozisyonunu d�nya koordinatlar�na �evir

        if (mousePos.x < transform.position.x && facingRight) // Fare sol tarafa y�nelmi�se
            Flip();
        else if (mousePos.x > transform.position.x && !facingRight) // Fare sa� tarafa y�nelmi�se
            Flip();
    }

    // Karakterin y�n�n� de�i�tirir
    private void Flip()
    {
        facingRight = !facingRight; // Y�n� de�i�tir
        transform.Rotate(0, 180, 0); // Karakteri 180 derece d�nd�r
    }

    // �arp��ma kontrollerini yapar
    private void CollisionChecks()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround); // Zemin temas�n� kontrol et
    }

    // Gizmo'lar ile zemin kontrol alan�n� g�sterir
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); // Zemin kontrol alan�n� �iz
    }
}
