using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Deathzone : MonoBehaviour
{
    public GameObject player;
    public string again;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player"))
        {
            Destroy(player);
            SceneManager.LoadScene(again);
        }
    }
}
