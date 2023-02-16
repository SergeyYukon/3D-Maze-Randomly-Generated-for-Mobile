using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    [SerializeField] private GameObject finishEffect;
    [SerializeField] private float speedPullToPortal;
    private Rigidbody rbPlayer;

    private void Awake()
    {
        EventController.OnLevelEndEvent += ReleasePlayer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            rbPlayer = other.GetComponent<Rigidbody>();
            rbPlayer.velocity = Vector3.zero;
            rbPlayer.useGravity = false;
            finishEffect.SetActive(true);
            EventController.OnFinish();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Mathf.Abs(other.transform.position.x - transform.position.x) > 0.1f)
            {
                other.transform.position = Vector3.MoveTowards(other.transform.position, transform.position, speedPullToPortal * Time.deltaTime);
            }
        }
    }

    private void ReleasePlayer()
    {
        rbPlayer.useGravity = true;
    }
}
