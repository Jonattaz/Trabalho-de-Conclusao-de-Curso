using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyAI))]
public class VisionSensor : MonoBehaviour{

    // The layer that will be detected
    [SerializeField] private LayerMask DetectionMask = ~0;

    EnemyAI LinkedAI;

    // Start is called before the first frame update
    void Start(){
        LinkedAI = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update(){
        
        // Check all candidates
        for (int index = 0; index < DetectableTargetManager.Instance.AllTargets.Count; index++){
            
            var candidateTarget = DetectableTargetManager.Instance.AllTargets[index];

            // Skip if the candidate is ourselves
            if(candidateTarget.gameObject == gameObject){
                continue;
            }

            var vectorToTarget = candidateTarget.transform.position - LinkedAI.EyeLocation;

            // If out of range - cannot see
            if(vectorToTarget.sqrMagnitude > (LinkedAI.VisionConeRange * LinkedAI.VisionConeRange)){
                continue;
            }

            vectorToTarget.Normalize();

            // If out of vision cone - cannot see
            if(Vector3.Dot(vectorToTarget.normalized, LinkedAI.EyeDirection) < LinkedAI.cosVisionConeAngle){
                continue;
            }

            // Raycast to target passes
            RaycastHit hitResult;
            if(Physics.Raycast(LinkedAI.EyeLocation, vectorToTarget.normalized, out hitResult,LinkedAI.VisionConeRange, DetectionMask, QueryTriggerInteraction.Collide)){
                
                if(hitResult.collider.GetComponentInParent<DetectableTarget>() == candidateTarget)
                    LinkedAI.ReportCanSee(candidateTarget);    
            }

        }        
    }
}
















