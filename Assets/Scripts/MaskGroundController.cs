using UnityEngine;
using UnityEngine.U2D;

public class MaskGroundController : MonoBehaviour
{
    public GameObject MaskGround;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MaskGround.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MaskGround.SetActive(true);
        }
    }

}
