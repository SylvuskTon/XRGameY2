using DialogueEditor;
using UnityEngine;

public class ConversationStarter : MonoBehaviour
{
    PlayerMovement playMov;
    [SerializeField] private NPCConversation myConversation;

    public GameObject fInput;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                ConversationManager.Instance.StartConversation(myConversation);
                fInput.SetActive(false);
            }
            else
            {
                fInput.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        fInput.SetActive(false);
    }
}
