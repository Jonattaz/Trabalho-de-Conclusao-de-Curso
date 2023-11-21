using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowItem : MonoBehaviour{
    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject itemToThrow;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Sprite canThrowImage;
    [SerializeField] private Sprite cannotThrow;
    [SerializeField] private Image throwImage;

    private Rigidbody projectileRb;
    private GameObject projectile;
    private PlayerMovement PlayerMovement;

    [Header("Settings")]
    public int totalThrows;
    [SerializeField] private float throwCoolDown;
    [Range(10,100)]
    [SerializeField] private int linePoints = 25;
    [SerializeField] [Range(0.01f, 0.25f)] private float timeBetweenPoints = 0.1f;
    private LayerMask itemCollisionMask;

    [Header("Throwing")]
    [SerializeField] private KeyCode throwKey = KeyCode.G;
    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;
    [SerializeField] private float throwMovementTimer;
    private bool readyToThrow;
    Vector3 mousePos;
    public bool control;
    private float timer;

    // Start is called before the first frame update
    void Start(){
        PlayerMovement = GetComponent<PlayerMovement>();
        
        readyToThrow = true;    

         int itemLayer = itemToThrow.layer;
        for (int i = 0; i < 32; i++){
            if (!Physics.GetIgnoreLayerCollision(itemLayer, i)){
                itemCollisionMask |= 1 << i;
            }
        }  
    }

    // Update is called once per frame
    void Update(){

        mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            
        if(totalThrows > 0){
            throwImage.sprite = canThrowImage;
        }else{
            throwImage.sprite = cannotThrow;
        }

        if(!PlayerMovement.movementConstraint){
            if(Input.GetKey(throwKey) && readyToThrow && totalThrows > 0){
                DrawProjection();
            }   

            if(Input.GetKeyUp(throwKey) && readyToThrow && totalThrows > 0){
                Throw();
                lineRenderer.enabled = false;
            }
        }
    }

    IEnumerator UpwardForceCounter(){

        if(throwUpwardForce < 3f && !control){
            timer -= Time.deltaTime;
            if(timer <= 0f)
                throwUpwardForce++;
            
        }else if(throwUpwardForce == 3f && !control){
            control = true;
            timer = throwMovementTimer;
        }


        if(throwUpwardForce <= 3f && throwUpwardForce > -3f && control){            
            
            timer -= Time.deltaTime;
            if(timer <= 0f)
                throwUpwardForce--;
            
        }else if(throwUpwardForce == -3f && control){
            control = false;
            timer = throwMovementTimer;
        }              

        yield return new WaitForSeconds(1f);

    }

    private void DrawProjection(){
        
        lineRenderer.enabled = true;
        lineRenderer.positionCount = Mathf.CeilToInt(linePoints / timeBetweenPoints) + 1;
        Vector3 startPosition = attackPoint.position;
        
        Vector3 startVelocity = (throwForce * orientation.forward) + (throwUpwardForce * orientation.up) / itemToThrow.GetComponent<Rigidbody>().mass;
        
        int i= 0;

        lineRenderer.SetPosition(i, startPosition);

        for(float time = 0; time < linePoints; time += timeBetweenPoints){
            i++;
            Vector3 point = startPosition + time * startVelocity;
        
            point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y) / 2f * time * time;

            lineRenderer.SetPosition(i, point);


            // Prevent line from going through walls
            Vector3 lastPosition = lineRenderer.GetPosition(i - 1);

            if(Physics.Raycast(lastPosition, (point - lastPosition).normalized, out RaycastHit hit,(point - lastPosition).magnitude, itemCollisionMask)){
                lineRenderer.SetPosition(i, hit.point);
                lineRenderer.positionCount = i + 1;
                return;
            }
        }

    }

    private void Throw(){
        readyToThrow = false;

        // Instantiate object to throw
        projectile = Instantiate(itemToThrow, attackPoint.position, orientation.rotation);
        
         // Get rigidbody component
        projectileRb = projectile.GetComponent<Rigidbody>();  

        // Calculate direction
        Vector3 forceDirection = orientation.transform.forward;

        RaycastHit hit;

        if(Physics.Raycast(orientation.position, orientation.forward, out hit, 500f)){
            forceDirection = (hit.point - attackPoint.position).normalized; 
        }

        // Add force
        Vector3 forceToAdd = forceDirection * throwForce + transform.up * throwUpwardForce;

        projectileRb.AddForce(forceToAdd, ForceMode.Impulse);

        totalThrows--;

        // Throw cooldown
        Invoke(nameof(ResetThrow), throwCoolDown);

    }

    private void ResetThrow(){
        readyToThrow = true;
    }
}
















