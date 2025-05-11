using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerExitHandler : MonoBehaviour
{
    private BoxCollider boxCol;
    private TMP_Text killText;
    private bool finished = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(SceneManager.GetActiveScene().name == "MenuScene") return;
        killText = GlobalStateManager.Instance.getKillText();
        killText.gameObject.SetActive(false);
        boxCol = gameObject.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKey(KeyCode.E) && !finished){
            int livingEnemies = GlobalStateManager.Instance.getLivingEnemies();
            if (livingEnemies > 0){
                killText.gameObject.SetActive(true);
                killText.text = $"You must kill {livingEnemies} more to descend!";
                Invoke("hideText", 2f);
                return;
            }

            finished = true;
            PlayerScoreManager.Instance.completeLevel();
            LevelCompleteOverlayController.Instance.gameObject.SetActive(true);
            LevelCompleteOverlayController.Instance.updateText();
            GlobalStateManager.Instance.setLevelFinished();
            Destroy(this);
        }
    }

    void hideText(){
        killText.gameObject.SetActive(false);
    }
}
