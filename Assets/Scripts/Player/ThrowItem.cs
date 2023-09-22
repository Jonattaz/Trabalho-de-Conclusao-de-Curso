using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowItem : MonoBehaviour{
    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private GameObject itemToThrow;
    [SerializeField] private Text throwsText;

    [Header("Settings")]
    [SerializeField] private int totalThrows;
    [SerializeField] private float throwCoolDown;

    [Header("Throwing")]
    [SerializeField] private KeyCode throwKey = KeyCode.G;
    [SerializeField] private float throwForce;
    [SerializeField] private float throwUpwardForce;
    private bool readyToThrow; 

    // Start is called before the first frame update
    void Start(){
        readyToThrow = true;        
    }

    // Update is called once per frame
    void Update(){
        if(Input.GetKeyDown(throwKey) && readyToThrow && totalThrows > 0){
            Throw();
        }        
    }
    private void Throw(){
        readyToThrow = false;

        // Instantiate object to throw
        GameObject projectile = Instantiate(itemToThrow, attackPoint.position, orientation.rotation);

        // Get rigidbody component
        Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();

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
















