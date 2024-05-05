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
            // Activa el parámetro "LookAtPlayer" para activar el IK del brazo
            animator.SetBool("LookAtPlayer", true);

            // Calcula la dirección hacia el jugador
            Vector3 directionToPlayer = player.position - transform.position;

            // Calcula la rotación para señalar al jugador
            Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);

            // Establece la posición y rotación de la mano derecha del NPC usando IK
            animator.SetIKPosition(AvatarIKGoal.RightHand, player.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, lookRotation);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f); // Peso completo para la posición
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1f); // Peso completo para la rotación
        }
        else
        {
            // Si el jugador no está presente, desactiva el parámetro "LookAtPlayer" para desactivar el IK del brazo
            animator.SetBool("LookAtPlayer", false);
        }
    }
}
