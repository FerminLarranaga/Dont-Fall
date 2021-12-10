using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float maxSpeed = 3f;
    public float speed = 75f;
    public float jumpPower = 7f;

    public GameObject PauseMenu;
    public AudioClip Jump, Die;

    private Rigidbody2D rb2d;
    private Animator anim;
    private SpriteRenderer spr;
    private Vector2 respawnPoint, startScale;
    private AudioSource BGMusic;
    private GameObject PlayerSpriteGoingUp;
    private AudioSource PlayerSound;
    private LiveSystemController livesSystem;

    private bool jump, doubleJump;
    private bool movement = true;
    private bool grounded;
    private bool attack = false;
    private bool isDead = false;
    private float offSetJumpTime = 0.2f, offSetGroundedTime = 0.2f;

    [SerializeField]
    private int lives = 5;

    private float offSetJump, offSetGrounded = 0;

    private float timePlayed = 0;
    private int totalJumps, deaths, checkpointsTaken, enemiesKilled;

    void Awake()
    {
        WorldData data = SavingSystem.LoadWorld(SavingSystem.WorldDataPath);
        lives = data.lives;

        if (data.respawnPoint[0] != 0 && data.respawnPoint[1] != 0)
            transform.position = new Vector2(data.respawnPoint[0], data.respawnPoint[1]);
    }

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
        respawnPoint = transform.position;
        PlayerSound = GetComponent<AudioSource>();
        startScale = transform.localScale;
        BGMusic = GameObject.Find("BGMusic").GetComponent<AudioSource>();
        livesSystem = GameObject.Find("Lives").GetComponent<LiveSystemController>();
        PlayerSpriteGoingUp = transform.Find("GoingUpPlayer").gameObject;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));
        anim.SetBool("Grounded", grounded);
        anim.SetBool("Attack", attack);

        timePlayed += Time.deltaTime;

        ManageJump();
        ManagePause();
    }

    private void ManagePause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
            BGMusic.volume = 0.15f;
        }
    }

    private void ManageJump()
    {
        offSetJump -= Time.deltaTime;
        offSetGrounded -= Time.deltaTime;

        if (grounded)
        {
            offSetGrounded = offSetGroundedTime;
            doubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            offSetJump = offSetJumpTime;
        }

        if (offSetJump > 0)
        {
            if (offSetGrounded > 0)
            {
                offSetGrounded = 0;
                offSetJump = 0;
                jump = true;
                doubleJump = true;
            } else if (doubleJump)
            {
                offSetGrounded = 0;
                offSetJump = 0;
                jump = true;
                doubleJump = false;
            }
        }
    }
    
    void FixedUpdate()
    {
        ManageHorizontalMovement();

        ManageVerticalMovement();

        ManageAttack();

        if (!isDead)
            Respawn();

        if (isDead && !PlayerSound.isPlaying)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("Menu");
        }
    }

    private void ManageHorizontalMovement()
    {
        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.75f;

        if (grounded)
        {
            rb2d.velocity = fixedVelocity;
        }

        float h = Input.GetAxisRaw("Horizontal");

        if (!movement)
        {
            h = 0;
        }

        rb2d.AddForce(new Vector2(speed * h, 0));

        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);

        if (h > 0.1)
        {
            transform.localScale = new Vector3(startScale.x, startScale.y, 1);
        }
        else if (h < -0.1)
        {
            transform.localScale = new Vector3(-startScale.x, startScale.y, 1);
        }
    }

    private void ManageVerticalMovement()
    {
        if (jump)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, 0);
            rb2d.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            jump = false;
            totalJumps++;
            PlayerSound.clip = Jump;
            PlayerSound.Play();
        }
    }

    private void ManageAttack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            attack = true;
            Invoke("StopAttacking", 0.1f);
        }
    }

    void StopAttacking()
    {
        attack = false;
    }

    // Funcion utilizada en Red_Enemy_Controller
    public void EnemyKnockBack(float [] data)
    {
        float EnemyPositionX = data[0];
        float power = data[1];

        // Quitar velocidad para que no se sumen fuerzas
        rb2d.velocity = new Vector2(rb2d.velocity.x, 0);

        // Salto para arriba
        rb2d.AddForce(Vector2.up * power, ForceMode2D.Impulse);

        // Movimiento para el lado atacado
        float side = Mathf.Sign(EnemyPositionX - transform.position.x);
        rb2d.AddForce(Vector2.left * power * side, ForceMode2D.Impulse);

        // Desactivar movimiento
        movement = false;

        // Modificar color para parecer que lo atacaron
        spr.color = Color.red;

        // Volver a activar el movimiento
        Invoke("EnableMovement", 0.7f);
    }

    // Activar movimiento
    void EnableMovement()
    {
        movement = true;
        spr.color = Color.white;
    }

    // Destruir enemigo
    public IEnumerator DestroyEnemy(Collider2D col)
    {
        enemiesKilled++;
        GameObject Enemy = col.gameObject;
        Destroy(col);
        yield return new WaitForSeconds(0.4f);
        Destroy(Enemy);
    }

    void Respawn()
    {
        if (lives > 0 && transform.position.y < -10)
        {
            lives--;
            livesSystem.SendMessage("UpdateLives");
            if (lives > 0)
            {
                deaths++;
                transform.position = respawnPoint;
                maxSpeed = 3;
                jumpPower = 7;
            }
        }

        if (lives == 0 && !isDead)
        {
            isDead = true;
            BGMusic.Pause();
            speed = 0;
            maxSpeed = 0;
            jumpPower = 0;
            PlayerSound.clip = Die;
            PlayerSound.Play();

            string DataFolderPath = Application.persistentDataPath + SavingSystem.WorldFolderPath;

            string[] dataFilesPaths = Directory.GetFiles(DataFolderPath);
            foreach (string dataFilePath in dataFilesPaths)
            {
                File.Delete(dataFilePath);
            }

            Directory.Delete(DataFolderPath);
        }
    }

    public void PlayerIsUsingRope(bool isUsingRope)
    {
        if (isUsingRope)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            PlayerSpriteGoingUp.SetActive(true);
            
            doubleJump = false;
            movement = false;

            rb2d.velocity = new Vector2(0, rb2d.velocity.y);

            rb2d.mass = 0.1f;
        } else
        {
            GetComponent<SpriteRenderer>().enabled = true;
            PlayerSpriteGoingUp.SetActive(false);

            movement = true;

            rb2d.mass = 1f;
        }
    }

    public void SaveLevel()
    {
        LevelData data = new LevelData(this);
        SavingSystem.SaveLevel(data, SavingSystem.WorldFolderPath + "/Level " + GetLevel() + ".gsp");
    }

    public void SaveWorld()
    {
        float[] respawnPt = new float[2] { respawnPoint.x, respawnPoint.y };
        WorldData data = new WorldData(1, respawnPt, GetLevel(), lives, checkpointsTaken, deaths,
                                       enemiesKilled, totalJumps, timePlayed);
        SavingSystem.SaveWorld(data, SavingSystem.WorldDataPath);
    }

    public void SetRespawnPoint(Vector2 newRespawnPoint)
    {
        checkpointsTaken++;
        respawnPoint = newRespawnPoint;
    }

    public void SetJump(bool jump)
    {
        this.jump = jump;
    }

    public void SetDoubleJump(bool doubleJump)
    {
        this.doubleJump = doubleJump;
    }

    public void SetGrounded(bool grounded)
    {
        this.grounded = grounded;
    }

    public int GetLives()
    {
        return lives;
    }

    public int GetLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public Vector2 GetRespawnPoint()
    {
        return respawnPoint;
    }

    public int GetTotalJumps()
    {
        return totalJumps;
    }

    public int GetDeaths()
    {
        return deaths;
    }

    public int GetCheckpointsTaken()
    {
        return checkpointsTaken;
    }

    public float GetTimePlayed()
    {
        return timePlayed;
    }

    public bool GetAttack()
    {
        return attack;
    }
}
