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
        Kill
    }

    // Novas variaveis
    [SerializeField] private float AttentionTime = 5f ;
    [SerializeField] private float StunTime;
    [SerializeField] private PlayerMovement PlayerMovement;
    [SerializeField] private GameObject DeathUI;
    [SerializeField] private GameObject PlayerObj;
    float AttentionTimeRemaning;
    float MovementSpeedValueRef;
    float distance;
    NavMeshAgent NavMeshAgent;

    
    // Variaveis originais
    [SerializeField] EState CurrentState;
    [SerializeField] List<Transform> PatrolPoints;
    [SerializeField] private float PatrolPointsReachedThreshold = 0.5f;
    [SerializeField] private float MovementSpeed = 2f;
    [SerializeField] private float IdleMinTime = 5f;
    [SerializeField] private float IdleMaxTime = 10f;
    [SerializeField] private float ListenTme = 5f;
    float IdleTimeRemaning;
    float ListenTimeRemaning;
    int CurrentPatrolPoint;
    Vector3 LastHeardLocation;
    Transform LastSeenTarget;

    public bool stopAI;
   

    // Start is called before the first frame update
    void Start(){
        MovementSpeedValueRef = MovementSpeed;
        NavMeshAgent = GetComponent<NavMeshAgent>();
        NavMeshAgent.enabled = true;
        SwitchToState(CurrentState);
    }

    // Update is called once per frame
    void Update(){
            
        if(!stopAI)
            UpdateState();
    }

     
    public void OnTargetDetected(GameObject target){
        // OnDetected - I See you
        LastSeenTarget = target.transform;
        SwitchToState(EState.SawSomething);    
    }

    public void OnTargetLost(){
        // OnLostDetected - Where are you
        LastSeenTarget = null;

        if(CurrentState == EState.SawSomething){
            SwitchToState(EState.Patrolling);
        }

    }

    public void OnSoundHeard(Vector3 location){
        // OnSuspicious - I hear you
        LastHeardLocation = location;

        SwitchToState(EState.HeardSomething);

    }

    public void OnKill(GameObject target){
        // OnFullyDetected - Charge
        LastSeenTarget = target.transform;
        SwitchToState(EState.Kill);
    }
    
    public void OnTotalLostSuspicion(){
        // OnlostSuspicion - Where did you go
        LastSeenTarget = null;

        if(CurrentState == EState.SawSomething){
            SwitchToState(EState.Patrolling);
        }
    }

    public void OnTotalLost(){
        // OnFullyLost - Must be Nothing
        LastSeenTarget = null;

        if(CurrentState == EState.SawSomething){
            SwitchToState(EState.Patrolling);
        }
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
            transform.rotation = Quaternion.LookRotation(LastHeardLocation - transform.position, Vector3.up);
            ListenTimeRemaning = ListenTme;

        }else if(newState == EState.SawSomething){

            // Look at target
            transform.rotation = Quaternion.LookRotation(LastSeenTarget.transform.position - transform.position, Vector3.up);
            //transform.LookAt(LastSeenTarget, Vector3.up);
            AttentionTimeRemaning = AttentionTime;

        }else if(newState == EState.Kill){

            transform.rotation = Quaternion.LookRotation(LastSeenTarget.transform.position - transform.position, Vector3.up);
            AttentionTimeRemaning = AttentionTime;
    
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

        }else if(CurrentState == EState.Kill){
            UpdateState_Kill();
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
        //transform.position = Vector3.MoveTowards(transform.position, PatrolPoints[CurrentPatrolPoint].position, MovementSpeed * Time.deltaTime);

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

        MovementSpeed = 0f;
        AttentionTimeRemaning -= Time.deltaTime;
        
        if(LastSeenTarget != null)
            distance = Vector3.Distance(LastSeenTarget.transform.position, gameObject.transform.position);
        
        // Nothing seen - Patrol
        if(AttentionTimeRemaning <= 0){
            MovementSpeed = MovementSpeedValueRef;
            SwitchToState(EState.Patrolling);

        }
    }

    private void UpdateState_Kill(){

        MovementSpeed = 0f;
        AttentionTimeRemaning -= Time.deltaTime;
        if(LastSeenTarget != null)
            distance = Vector3.Distance(LastSeenTarget.transform.position, gameObject.transform.position);
        
        // Nothing seen - Patrol
        if(AttentionTimeRemaning <= 0){
            MovementSpeed = MovementSpeedValueRef;
            SwitchToState(EState.Patrolling);

        }else{
            StartCoroutine(StunAttack());
        }
    }

    IEnumerator StunAttack(){
        // Stun and game over
        PlayerMovement.movementConstraint = true;

        yield return new WaitForSeconds(StunTime);
        // Game Over
        DeathUI.SetActive(true);
        PlayerObj.SetActive(false);
        this.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }

}


























