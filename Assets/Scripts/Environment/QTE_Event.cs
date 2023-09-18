using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PudimdimGames{
    public class QTE_Event : MonoBehaviour{

        [SerializeField] private AudioClip lockpickFailSound;

        [SerializeField] private float fillAmount = 0;
        [SerializeField] private float fillValue;
        private float timeThreshold = 0;
        
        [SerializeField] private GameObject door;

        [SerializeField] private GameObject tubulacao;
        
        [SerializeField] private AudioClip lockpickingSound;

        [SerializeField] private bool isDoor;

        [SerializeField] private float fillHold;
        
        [SerializeField] private GameObject lockpickIcon;

        
        // Update is called once per frame
        void Update()
        {
            if(UnityEngine.Input.GetKeyDown("e")){
                lockpickIcon.SetActive(true);
                fillAmount += fillValue;
                // Sons de tentando abrir a porta
            }

            timeThreshold += Time.deltaTime;    

            if(timeThreshold > .1){
                timeThreshold = 0;
                fillAmount -= fillHold;
            }

            if(CountDownTimer.TimerInstance.noise){
                // Som de falhar ao tentar abrir a porta
            }

            if(fillAmount < 0){
                fillAmount = 0;
            }

            if(fillAmount >= 1){
                if(isDoor){
                    door.GetComponent<Door>().canOpenGet = true;
                }else{
                    tubulacao.GetComponent<Teleporting>().canTeleport = true;
                }
                lockpickIcon.SetActive(false);
            }

            GetComponent<Image>().fillAmount = fillAmount;
        }

    }
}
















