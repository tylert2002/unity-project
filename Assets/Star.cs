using UnityEngine;

public class Star : MonoBehaviour
{
    private bool hasTriggered = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasTriggered)
        {
            return;

        }

        if (!other.CompareTag("Player")) 
        {
            return;
        }

        hasTriggered = true;
        GameManagerLV3.instance.starCollected = true; 

        bool levelComplete = GameManagerLV3.instance.CheckForLevelCompletion();

        if (levelComplete)
        {
            Destroy(gameObject);
        }
        else
        {
            // star stays collected but doesn't disappear until conditions met
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
        
    }
}
