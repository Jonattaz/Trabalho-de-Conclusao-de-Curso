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
    private Rigidbody projectileRb;
    private GameObject projectile;
    public Text throwsText;
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
    private bool readyToThrow; 

    // Start is called before the first frame update
    void Start(){
        PlayerMovement = GetComponent<PlayerMovement>();
        throwsText.text = totalThrows.ToString();
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

    private void DrawProjection(){
        lineRenderer.enabled = true;
        lineRenderer.positionCount= Mathf.CeilToInt(linePoints / timeBetweenPoints) + 1;
        Vector3 startPosition = attackPoint.position;
        Vector3 startVelocity = throwForce * orientation.forward / itemToThrow.GetComponent<Rigidbody>().mass;
        int i= 0;

        lineRenderer.SetPosition(i, startPosition);
        for(float time = 0; time < linePoints; time += timeBetweenPoints){
            i++;
            Vector3 point = startPosition + time * startVelocity;
            point.y = startPosition.y +startVelocity.y * time + (Physics.gravity.y) / 2f * time * time;

            lineRenderer.SetPosition(i, point);

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
        throwsText.text = totalThrows.ToString();

        // Throw cooldown
        Invoke(nameof(ResetThrow), throwCoolDown);

    }

    private void ResetThrow(){
        readyToThrow = true;
    }
}
















