using UnityEngine;

public class triggerLock : MonoBehaviour
{   
    public GameObject cam;
    public EnemySpawner EnemySpawner;
    public GameObject[] enemies;
    private GameObject[] activeEnemies = new GameObject[0];
    public bool isLocked = false;
    public bool isEnemiesDead = false;
    public int enemyCount;
    public GameObject barriers;
    public GameObject[] rats;
    public GameObject[] bosses;
    public GameObject[] gooberts;

    // On Trigger Enter, lock the camera and set isLocked to true
    // Spawn the list of enemies and set isEnemiesDead to false
    // Check in update if those enemies are dead (not found in the scene anymore)
    // If they are dead, unlock the camera and set isLocked to false

    private void OnTriggerEnter(Collider other){
        Debug.Log("Trigger Entered: " + other.name);
        if(other.tag == "Player" && !isLocked){
            isLocked = true;
            // lock camera and spawn barriers
            cam.GetComponent<CameraController>().enabled = false;
            // spawn enemies in enemies array
            EnemySpawner.SpawnEnemies(enemies);

            rats = GameObject.FindGameObjectsWithTag("Enemy");
            bosses = GameObject.FindGameObjectsWithTag("Boss");
            gooberts = GameObject.FindGameObjectsWithTag("Goobert");
            enemyCount = rats.Length + bosses.Length + gooberts.Length;
            
            // barriers.SetActive(true);
        }
    }

    private void Update(){
        rats = GameObject.FindGameObjectsWithTag("Enemy");
        bosses = GameObject.FindGameObjectsWithTag("Boss");
        gooberts = GameObject.FindGameObjectsWithTag("Goobert");
        enemyCount = rats.Length + bosses.Length + gooberts.Length;

        Debug.Log("Enemies alive: " + enemyCount);
        Debug.Log("Rats alive: " + rats.Length);
        Debug.Log("Bosses alive: " + bosses.Length);
        Debug.Log("Gooberts alive: " + gooberts.Length);
        

        if(isLocked && enemyCount == 0){
            print("UNLOCK");
            cam.GetComponent<CameraController>().enabled = true;
            isLocked = false;
            //barriers.SetActive(false);
            Destroy(gameObject);
        }
    }

}
