using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFSM : MonoBehaviour{

    public enum EState{
        Idle,
        Patrolling,
        SawSomething,
        HeardSomething,
        Chase
    }

    [SerializeField] EState CurrentState;

    [SerializeField] List<Transform> PatrolPoints;
    [SerializeField] float PatrolPointsReachedThreshold = 0.5f;

    [SerializeField] float MovementSpeed = 2f;

    [SerializeField] float IdleMinTime = 5f;
    [SerializeField] float IdleMaxTime = 10f;

    [SerializeField]  float ListenTme = 5f;
    [SerializeField] float AttentionTime = 5f ;
    float IdleTimeRemaning;
    float ListenTimeRemaning;
    float AttentionTimeRemaning;
    int CurrentPatrolPoint;
    GameObject LastHeardLocation;
    GameObject LastSeenTarget;
    float MovementSpeedValueRef;
    NavMeshAgent NavMeshAgent;

    // Start is called before the first frame update
    void Start(){

        MovementSpeedValueRef = MovementSpeed;
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.enabled = true;
        SwitchToState(CurrentState);
    }

    // Update is called once per frame
    void Update(){
        UpdateState();
    }

     
    public void OnTargetDetected(GameObject target){
        LastSeenTarget = target;
        SwitchToState(EState.SawSomething);    
    }

    public void OnTargetLost(){
    
        LastSeenTarget = null;

        if(CurrentState == EState.SawSomething){

            SwitchToState(EState.Patrolling);

        }

    }
    
    public void OnSoundHeard(GameObject location){
                
        LastHeardLocation = location;

        SwitchToState(EState.HeardSomething);

    }

    public void OnChase(GameObject target){
        
        LastSeenTarget = target;
        SwitchToState(EState.Chase);
    }
    

    void SwitchToState(EState newState){
        
        MovementSpeed = MovementSpeedValueRef;
        // Initialise based on the state
        if(newState == EState.Idle){
        
            IdleTimeRemaning = Random.Range(IdleMinTime, IdleMaxTime);
        
        }else if(newState == EState.Patrolling){
        
            CurrentPatrolPoint = Random.Range(0, PatrolPoints.Count);
        
        }else if(newState == EState.HeardSomething){

            // Look at the sound source for a set time
            transform.rotation = Quaternion.LookRotation(LastHeardLocation.transform.position - transform.position, Vector3.up);
            ListenTimeRemaning = ListenTme;

        }else if(newState == EState.SawSomething){

            // Look at target
            transform.rotation = Quaternion.LookRotation(LastSeenTarget.transform.position - transform.position, Vector3.up);
            AttentionTimeRemaning = AttentionTime;

        }else if(newState == EState.Chase){

            transform.rotation = Quaternion.LookRotation(LastSeenTarget.transform.position - transform.position, Vector3.up);
    
        }

        // Update state and debug display
        CurrentState = newState;
        Debug.Log(CurrentState.ToString());
    }

    private void UpdateState(){

        NavMeshAgent.speed = MovementSpeed;

        if(CurrentState ==  EState.Idle){
        
            UpdateState_Idle();
        
        }else if(CurrentState == EState.Patrolling){

            UpdateState_Patrolling();

        }else if(CurrentState == EState.HeardSomething){
            
            UpdateState_HeardSomething();

        }else if(CurrentState == EState.SawSomething){

            UpdateState_SawSomething();

        }else if(CurrentState == EState.Chase){
            UpdateState_Chase();
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
        NavMeshAgent.SetDestination(PatrolPoints[CurrentPatrolPoint].position);

        // Face the patrol point
        transform.LookAt(PatrolPoints[CurrentPatrolPoint], Vector3.up);

    }

    private void UpdateState_HeardSomething(){
  
        ListenTimeRemaning -= Time.deltaTime;

        // Nothing heard - Patrol
        if(ListenTimeRemaning <= 0){

            SwitchToState(EState.Patrolling);

        }else{
            
            if(LastHeardLocation != null)
                NavMeshAgent.SetDestination(LastHeardLocation.transform.position);
        }

    }    

    private void UpdateState_SawSomething(){

        MovementSpeed = 0f;
        AttentionTimeRemaning -= Time.deltaTime;
        Debug.Log(AttentionTime.ToString());
        // Nothing seen - Patrol
        if(AttentionTimeRemaning <= 0){
            MovementSpeed = MovementSpeedValueRef;
            SwitchToState(EState.Patrolling);

        }
    }

    private void UpdateState_Chase(){

        if(LastSeenTarget != null){
            MovementSpeed = MovementSpeedValueRef;

            NavMeshAgent.SetDestination(LastSeenTarget.transform.position);
        }

    }
}


























