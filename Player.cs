using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : PhysicsObject
{
    [Header("Attributes")]
    //Attributes
    private int maxHealth = 100;
    [SerializeField] private float maxSpeed = 1f;
    [SerializeField] private float jumpPower = 10;
    public int attackPower = 25;
    [SerializeField] private float attackDuration;


    [Header("Inventory")]
    //Linked to collectibles
    public int health = 100;
    public int ammo;
    public int coinsCollected;

    [Header("References")]

    [SerializeField] private GameObject attackBox;
    //Items
    public Sprite keySprite;
    public Sprite keyGemSprite;
    public Sprite inventoryItemBlank;
    //UI Ref
    private Vector2 healthBarOrigSize;
    //UI Ref
    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();



    //Singleton Instantiation
    private static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<Player>();
            return instance;
        }
    }


    private void Awake()
    {
        if (GameObject.Find("New Player")) Destroy(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        gameObject.name = "New Player";

        healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;
        UpdateUI();

        SetSpawnPosition();
    }

    // Update is called once per frame
    void Update()
    {
        //Left & Right movement
        targetVelocity = new Vector2(Input.GetAxis("Horizontal") * maxSpeed, 0); //Time.deltaTime calc in PhysicsObject Script

        //Jump
        if (Input.GetButtonDown("Jump") && grounded)
        {
            velocity.y = jumpPower; //Vec2 var called in PO.cs
        }

        //Player turning
        if (targetVelocity.x < -.01)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (targetVelocity.x > .01)
        {
            transform.localScale = new Vector2(1, 1);
        }

        //use fire1 to set attackBox to active, otherwise set to false
        if (Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(ActivateAttack());

        }

        //Die if player health is 0
        if (health <= 0)
        {
            Die();
        }
    }

    //Activate Attack Function
    public IEnumerator ActivateAttack()
    {
        attackBox.SetActive(true);
        yield return new WaitForSeconds(attackDuration);
        attackBox.SetActive(false);

    }


    //Update UI elements
    public void UpdateUI()
    {
        if (healthBarOrigSize == Vector2.zero) healthBarOrigSize = GameManager.Instance.healthBar.rectTransform.sizeDelta;
        //Coins UI text = Coins collected
        GameManager.Instance.coinsText.text = coinsCollected.ToString();

        //Set the HB width to a % of its original value
        //healthBar.x * (health / maxHealth)
        GameManager.Instance.healthBar.rectTransform.sizeDelta = new Vector2(healthBarOrigSize.x * ((float)health / (float)maxHealth), GameManager.Instance.healthBar.rectTransform.sizeDelta.y);


    }

    public void AddInventoryItem(string inventoryName, Sprite image)
    {
        inventory.Add(inventoryName, image);
        // The blank sprite should swap with the key sprite
        GameManager.Instance.inventoryItemImage.sprite = inventory[inventoryName];
    }


    public void SetSpawnPosition()
    {
        transform.position = GameObject.Find("SpawnLocation").transform.position;
    }

    public void Die()
    {
        SceneManager.LoadScene("SampleScene");
    }


    public void RemoveInventoryItem(string inventoryName)
    {
        inventory.Remove(inventoryName);
        // The blank sprite should swap with the key sprite
        GameManager.Instance.inventoryItemImage.sprite = inventoryItemBlank;



    }
}
