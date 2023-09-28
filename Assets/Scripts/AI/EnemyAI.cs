using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif // Unity Editor

public class EnemyAI : MonoBehaviour{

    [SerializeField] private float _VisionConeAngle = 60f;
    [SerializeField] private float _VisionConeRange = 30f;
    [SerializeField] private Color _VisionConeColor = new Color(1f, 0f, 0f, 0.25f);

    [SerializeField] private float _HearingRange = 20f;
    [SerializeField] private Color _HearingRangeColor = new Color(1f, 0f, 0f, 0.25f);

    public Vector3 EyeLocation => transform.position;
    public Vector3 EyeDirection => transform.forward;

    public float VisionConeAngle => _VisionConeAngle;
    public float VisionConeRange => _VisionConeRange;
    public Color VisionConeColor => _VisionConeColor;

    public float HearingRange => _HearingRange;
    public Color HearingRangeColor => _HearingRangeColor;

    public float cosVisionConeAngle{get; private set;} = 0f;

    // Awake is called when the script instance is being loaded.
    void Awake(){
        cosVisionConeAngle = Mathf.Cos(VisionConeAngle * Mathf.Deg2Rad);
    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void ReportCanSee(DetectableTarget seen){
        Debug.Log(" Can see " + seen.gameObject.name);
    }

    public void ReportCanHear(Vector3 location,EHeardSoundCategory category, float intensity){
        Debug.Log(" Heard Sound " + category + " at " + location.ToString() + " with intensity of " + intensity);
    }

}

// Only Unity Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(EnemyAI))]
    public class EnemyAIEditor: Editor{

        public void OnSceneGUI(){
            
            var ai = target as EnemyAI;

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













