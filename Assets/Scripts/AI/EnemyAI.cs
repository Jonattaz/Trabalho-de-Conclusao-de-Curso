using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif // Unity Editor

[RequireComponent(typeof(AwarenessSystem))]
public class EnemyAI : MonoBehaviour{

    [SerializeField] private float _VisionConeAngle = 60f;
    [SerializeField] private float _VisionConeRange = 30f;
    [SerializeField] private Color _VisionConeColor = new Color(1f, 0f, 0f, 0.25f);

    [SerializeField] private float _HearingRange = 20f;
    [SerializeField] private Color _HearingRangeColor = new Color(1f, 0f, 0f, 0.25f);

    [SerializeField] private float _ProximityDetectionRange = 3f;
    [SerializeField] private Color _ProximityRangeColor = new Color(1f, 1f, 1f, 0.25f);

    public Vector3 EyeLocation => transform.position;
    public Vector3 EyeDirection => transform.forward;

    public float VisionConeAngle => _VisionConeAngle;
    public float VisionConeRange => _VisionConeRange;
    public Color VisionConeColor => _VisionConeColor;

    public float HearingRange => _HearingRange;
    public Color HearingRangeColor => _HearingRangeColor;

    public float ProximityDetectionRange => _ProximityDetectionRange;
    public Color ProximityRangeColor => _ProximityRangeColor;


    public float cosVisionConeAngle{get; private set;} = 0f;

    AwarenessSystem Awareness;
    EnemyFSM EnemyMovement;

    // Awake is called when the script instance is being loaded.
    void Awake(){
        cosVisionConeAngle = Mathf.Cos(VisionConeAngle * Mathf.Deg2Rad);
        Awareness = GetComponent<AwarenessSystem>();
        EnemyMovement = GetComponent<EnemyFSM>();
    }

    public void ReportCanSee(DetectableTarget seen){
        Awareness.ReportCanSee(seen);
    }

    public void ReportCanHear(GameObject source, Vector3 location,EHeardSoundCategory category, float intensity){
        Awareness.ReportCanHear(source, location, category, intensity);
    }

    public void ReportInProximity(DetectableTarget target){
        Awareness.ReportInProximity(target);
    }


    // Add the reactions to the On Methods. Reference EnemyFSM script here

    // Go see object thrown
    public void OnSuspicious(GameObject target){
        Debug.Log("I hear you");
        EnemyMovement.OnSoundHeard(target);

    }

    // Start a idle moment and then after seconds continue patrolling
    public void OnDetected(GameObject target){
        Debug.Log("I see you " + target.gameObject.name);
        EnemyMovement.OnTargetDetected(target);
    }

    // Chase player
    public void OnFullyDetected(GameObject target){
        Debug.Log("Charge! " + target.gameObject.name);
        EnemyMovement.OnChase(target);
        
    }
    
    
    // Start a idle moment and then after seconds continue patrolling
    public void OnLostDetected(GameObject target){
        Debug.Log("Where are you " + target.gameObject.name);
        EnemyMovement.OnTargetDetected(target);
    }
    
    // Start patrolling
    public void OnLostSuspicion(){
        Debug.Log("Where did you go");
        EnemyMovement.OnTargetLost();
    }

    // Start patrolling
    public void OnFullyLost(){
        Debug.Log(" Must be nothing");
        EnemyMovement.OnTargetLost();
    }
}

// Only Unity Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(EnemyAI))]
    public class EnemyAIEditor: Editor{

        public void OnSceneGUI(){
            
            var ai = target as EnemyAI;

            // Draw the detection range
            Handles.color = ai.ProximityRangeColor;
            Handles.DrawSolidDisc(ai.transform.position, Vector3.up, ai.ProximityDetectionRange);

            // Draw the hearing range
            Handles.color = ai.HearingRangeColor;
            Handles.DrawSolidDisc(ai.transform.position, Vector3.up, ai.HearingRange);

            // Work out the start point of the vision cone
            Vector3 startPoint = Mathf.Cos(-ai.VisionConeAngle * Mathf.Deg2Rad) * ai.transform.forward + Mathf.Sin(-ai.VisionConeAngle * Mathf.Deg2Rad) * ai.transform.right;

            // Draw vision cone
            Handles.color= ai.VisionConeColor;
            Handles.DrawSolidArc(ai.transform.position, Vector3.up, startPoint, ai.VisionConeAngle * 2f, ai.VisionConeRange);
        }
    }
#endif













