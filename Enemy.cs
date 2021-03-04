using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : PhysicsObject
{
    [SerializeField] private float maxSpeed;
    private int direction = 1;
    [SerializeField] private int attackPower = 10;
    public int health = 100;
    private int maxHealth = 100;
    //Raycast vars
    private RaycastHit2D rightWallRaycastHit;
    private RaycastHit2D leftWallRaycastHit;
    private RaycastHit2D rightLedgeRaycastHit;
    private RaycastHit2D leftLedgeRaycastHit;
    [SerializeField] private LayerMask raycastLayerMask;
    [SerializeField] private Vector2 raycastOffset;
    [SerializeField] private float raycastLength = 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        targetVelocity = new Vector2(maxSpeed * direction, 0);

        //Check for right ledge
        rightLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x + raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down, raycastLength);
        Debug.DrawRay(new Vector2(transform.position.x + raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down * raycastLength, Color.blue);
        if (rightLedgeRaycastHit.collider == null)
        {
            direction = -1;
        }


        //Check for left ledge
        leftLedgeRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x - raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down, raycastLength);
        Debug.DrawRay(new Vector2(transform.position.x - raycastOffset.x, transform.position.y + raycastOffset.y), Vector2.down * raycastLength, Color.green);
        if (leftLedgeRaycastHit.collider == null)
        {
            direction = 1;
        }


        //Check for right wall
        rightWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.right, raycastLength, raycastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.right * raycastLength, Color.red);
        if (rightWallRaycastHit.collider != null) direction = -1;

        //Check for left wall
        leftWallRaycastHit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.left, raycastLength, raycastLayerMask);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), Vector2.left * raycastLength, Color.magenta);
        if (leftWallRaycastHit.collider != null) direction = 1;

        //Death
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //If colliding with player, lose P health & update UI
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == Player.Instance.gameObject)
        {
            Player.Instance.health -= attackPower;
            Player.Instance.UpdateUI();
        }
    }



}
