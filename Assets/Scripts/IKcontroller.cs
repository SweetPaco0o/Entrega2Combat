using UnityEngine;

public class NPCIKController : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    private Animator animator; // Referencia al Animator del NPC

    void Start()
    {
        animator = GetComponent<Animator>(); // Obtener el componente Animator del NPC
    }

    void Update()
    {
        if (player != null)
        {
            // Activa el par�metro "LookAtPlayer" para activar el IK del brazo
            animator.SetBool("LookAtPlayer", true);

            // Calcula la direcci�n hacia el jugador
            Vector3 directionToPlayer = player.position - transform.position;

            // Calcula la rotaci�n para se�alar al jugador
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

            // Establece la posici�n y rotaci�n de la mano derecha del NPC usando IK
            animator.SetIKPosition(AvatarIKGoal.RightHand, player.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, lookRotation);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f); // Peso completo para la posici�n
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f); // Peso completo para la rotaci�n
        }
        else
        {
            // Si el jugador no est� presente, desactiva el par�metro "LookAtPlayer" para desactivar el IK del brazo
            animator.SetBool("LookAtPlayer", false);
        }
    }
}
