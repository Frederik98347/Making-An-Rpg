using UnityEngine;
namespace RpgTools.Combat.Animations
{
    public class Alphaanim : MonoBehaviour
    {
        Animator anim;
        AudioSource audioSource;

        PlayerClass.Player player;

        // Use this for initialization
        void Start()
        {
            audioSource = GetComponent<AudioSource>();
            FindObjectOfType<PlayerClass.Player>();
        }

        public void OnTriggerEnter(Collider col)
        {
            if (col.CompareTag("enemy"))
            {
                player.DoAutoDamage();
                audioSource.Play();
            }
        }
    }
}