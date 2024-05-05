using UnityEngine;

public class NPCIKController : MonoBehaviour
{
    public Transform Player;
    public Animator animator;

    public float maxDistance = 10f;
    public Vector3 minRotation;
    public Vector3 maxRotation;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnAnimatorIK()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        if (distanceToPlayer <= maxDistance)
        {
            animator.SetBool("Saludo", true);
            
            Vector3 directionToPlayer = (Player.position - transform.position).normalized;

            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer, Vector3.up);

            targetRotation.eulerAngles = new Vector3(
                Mathf.Clamp(targetRotation.eulerAngles.x, minRotation.x, maxRotation.x),
                Mathf.Clamp(targetRotation.eulerAngles.y, minRotation.y, maxRotation.y),
                Mathf.Clamp(targetRotation.eulerAngles.z, minRotation.z, maxRotation.z)
            );

            animator.SetLookAtPosition(Player.position);
            animator.SetLookAtWeight(1f);
            animator.SetBoneLocalRotation(HumanBodyBones.Head, targetRotation);
        }
        else
        {
            animator.SetBool("Saludo", false);

            animator.SetLookAtWeight(0f);
        }
    }
}
