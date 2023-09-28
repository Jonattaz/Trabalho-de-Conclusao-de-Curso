using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EHeardSoundCategory{
    
    EFootstep,
    EItem
}

public class HearingManager : MonoBehaviour
{
      // Singleton
    public static HearingManager Instance {get; private set;} = null;

    public List<HearingSensor> AllSensors{get; private set;} = new List<HearingSensor>();


    // Awake is called when the script instance is being loaded.
    void Awake(){
        if(Instance != null){
            
            Debug.LogError("Multiple HearingManager found. Destroying " + gameObject.name);
            Destroy(gameObject);
            return;
        }

        Instance = this;

    }

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    public void Register(HearingSensor sensor){
        AllSensors.Add(sensor);
    }

    public void Deregister(HearingSensor sensor){
        AllSensors.Remove(sensor);
    }

    public void OnSoundEmitted(GameObject source, Vector3 location, EHeardSoundCategory category, float intensity){
        
        // Notify all sensors
        foreach (var sensor in AllSensors){
            sensor.OnHeardSound(source, location, category, intensity);
        }
    }
}










