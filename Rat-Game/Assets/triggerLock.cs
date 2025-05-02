using UnityEngine;

public class triggerLock : MonoBehaviour
{   
    public GameObject cam;
    public EnemySpawner EnemySpawner;
    public GameObject[] enemies;
    private GameObject[] activeEnemies = new GameObject[0];
    public bool isLocked = false;
    public GameObject barriers;

    // On Trigger Enter, lock the camera and set isLocked to true
    // Spawn the list of enemies and set isEnemiesDead to false
    // Check in update if those enemies are dead (not found in the scene anymore)
    // If they are dead, unlock the camera and set isLocked to false

    private void OnTriggerEnter(Collider other){
        Debug.Log("Trigger Entered: " + other.name);
        if(other.tag == "Player" && !isLocked){  
            // spawn enemies in enemies array
            EnemySpawner.SpawnEnemies(enemies);
            
            // lock camera and spawn barriers
            cam.GetComponent<CameraController>().enabled = false;
            // barriers.SetActive(true);
            isLocked = true;
        }
    }

    private void Update(){
        if(isLocked && !EnemySpawner.EnemiesAlive()){
            print("UNLOCK");
            cam.GetComponent<CameraController>().enabled = true;
            isLocked = false;
            //barriers.SetActive(false);
        }
    }

}
