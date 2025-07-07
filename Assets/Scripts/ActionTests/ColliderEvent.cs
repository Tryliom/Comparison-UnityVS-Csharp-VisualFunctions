using UnityEngine;

public class OnEnter : MonoBehaviour
{
    [SerializeField] private GameObject _objectToToggle;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _objectToToggle.SetActive(false);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _objectToToggle.SetActive(true);
        }
    }
}