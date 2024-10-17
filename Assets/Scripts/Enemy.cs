<<<<<<< HEAD
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    // Música
    public AudioClip explosaoClip;

    void Start()
    {
        if (GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();
        }
    }
    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.down);
        
        if(transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Projectile"))
        {
            StartCoroutine(PlayExplosaoAndDestroy());
            //Destroy(gameObject);
        }
    }

    private IEnumerator PlayExplosaoAndDestroy()
    {
        Debug.Log("Tocando explosão!"); // Verifique se este log aparece
        var audioSource = GetComponent<AudioSource>();
        audioSource.clip = explosaoClip; // Define o clip de explosão
        audioSource.Play();

        // Espera o áudio tocar completamente
        yield return new WaitForSeconds(0.2f);

        Destroy(gameObject); // Destroi o objeto após a explosão
    }

}
=======
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Time.deltaTime * speed * Vector3.up);
        
        if(transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);
        }
    }
    
}
>>>>>>> ee1b2f18a5d430bda6be252274d79a1f6907e1f5
