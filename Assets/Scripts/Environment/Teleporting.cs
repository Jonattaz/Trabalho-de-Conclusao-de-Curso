using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporting : MonoBehaviour{

        public bool canTeleport;
        [SerializeField] private AudioClip stepsSound;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Transform teleportTarget;
        [SerializeField] private PlayerMovement player;
        
        void OnTriggerStay(Collider other){
            if(canTeleport){
                    StartCoroutine(Teleport());
                    Fader.FadeInstance.Fade();
                    // Travar a movimentação do personagem
                    player.movementConstraint = true;
                }                
            }
            
        IEnumerator Teleport(){
            yield return new WaitForSeconds(1);
            
            if(audioSource != null)
                audioSource.PlayOneShot(stepsSound);

            Fader.FadeInstance.Fade();
            player.gameObject.transform.position = new Vector3(
                teleportTarget.transform.position.x,
                teleportTarget.transform.position.y,
                teleportTarget.transform.position.z
            );
            
            // Destravar personagem
            player.movementConstraint = false;
            
        }
}