using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour{

    public enum EState{
        Idle,
        Patrolling,
        SawSomething,
        HeardSomething
    }

    [SerializeField] EState CurrentState;

    [SerializeField] List<Transform> PatrolPoints;
    [SerializeField] float PatrolPointsReachedThreshold = 0.5f;

    [SerializeField] float MovementSpeed = 2f;

    [SerializeField] float IdleMinTime = 5f;
    [SerializeField] float IdleMaxTime = 10f;

    [SerializeField]  float ListenTme = 5f;

    float IdleTimeRemaning;
    float ListenTimeRemaning;

    int CurrentPatrolPoint;

    Vector3 LastHeardLocation;

    Transform LastSeenTarget;

    // Start is called before the first frame update
    void Start(){
        SwitchToState(CurrentState);
    }

    // Update is called once per frame
    void Update(){
        UpdateState();
    }

     
    public void OnTargetDetected(GameObject target){

        Debug.Log("[" + gameObject.name + "] Detected " + target.name);
        LastSeenTarget = target.transform;
        SwitchToState(EState.SawSomething);    

    }

    public void OnTargetLost(GameObject target){
    
        Debug.Log("[" + gameObject.name + "] Lost " + target.name);

        LastSeenTarget = null;

        if(CurrentState == EState.SawSomething){

            SwitchToState(EState.Patrolling);

        }

    }
    
    public void OnSoundHeard(Vector3 location){
                
        Debug.Log("[" + gameObject.name + "] heard something at " + location);

        LastHeardLocation = location;

        SwitchToState(EState.HeardSomething);

    }
    

    void SwitchToState(EState newState){
        
        // Initialise based on the state
        if(newState == EState.Idle){
        
            IdleTimeRemaning = Random.Range(IdleMinTime, IdleMaxTime);
        
        }else if(newState == EState.Patrolling){
        
            CurrentPatrolPoint = Random.Range(0, PatrolPoints.Count);
        
        }else if(newState == EState.HeardSomething){

            // Look at the sound source for a set time
            transform.rotation = Quaternion.LookRotation(LastHeardLocation - transform.position, Vector3.up);
            ListenTimeRemaning = ListenTme;

        }else if(newState == EState.SawSomething){

            // Look at target
            transform.LookAt(LastSeenTarget, Vector3.up);

        }

        // Update state and debug display
        CurrentState = newState;
        Debug.Log(CurrentState.ToString());
    }

    private void UpdateState(){

        if(CurrentState ==  EState.Idle){
        
            UpdateState_Idle();
        
        }else if(CurrentState == EState.Patrolling){

            UpdateState_Patrolling();

        }else if(CurrentState == EState.HeardSomething){
            
            UpdateState_HeardSomething();

        }else if(CurrentState == EState.SawSomething){

            UpdateState_SawSomething();

        }
    }

    private void UpdateState_Idle(){

        // Update idle time remaning
        IdleTimeRemaning -= Time.deltaTime;

        // Idle time Completed
        if(IdleTimeRemaning <= 0){

            SwitchToState(EState.Patrolling);

        }
    }

    private void UpdateState_Patrolling(){

        Vector3 vectorToPatrolPoint = PatrolPoints[CurrentPatrolPoint].position - transform.position;

        // Reached patrol point
        if(vectorToPatrolPoint.magnitude <= PatrolPointsReachedThreshold){
            
            // Advance to the next point
            CurrentPatrolPoint = (CurrentPatrolPoint + 1) % PatrolPoints.Count;

        }

        // Move towards the point - Test this later using NavMesh instead of MoveTowards
        transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPatrolPoint].position, MovementSpeed * Time.deltaTime);

        // Face the patrol point
        transform.LookAt(PatrolPoints[CurrentPatrolPoint], Vector3.up);

    }

    private void UpdateState_HeardSomething(){

        ListenTimeRemaning -= Time.deltaTime;

        // Nothing heard - Patrol
        if(ListenTimeRemaning <= 0){

            SwitchToState(EState.Patrolling);

        }

    }    

    private void UpdateState_SawSomething(){

        transform.LookAt(LastSeenTarget, Vector3.up);

    }
}


























