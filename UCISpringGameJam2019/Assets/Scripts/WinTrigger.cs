using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CheckLayer("Player"))
            SceneManager.LoadScene("WinScreen");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
