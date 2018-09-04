using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundFall : MonoBehaviour
{
    public Transform groundParent;
    public float fallWaitTime;

    [Header("Debug")]
    public bool fallOnStart;

    private List<GameObject> grounds;
    private Coroutine coroutine;

    private int currentGroundChild;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        grounds = new List<GameObject>();

        currentGroundChild = 0;
        grounds.Add(groundParent.GetChild(currentGroundChild).gameObject);
        currentGroundChild += 1;

        if (fallOnStart)
            StartGroundFall();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (groundParent.childCount > currentGroundChild)
        {
            grounds.Add(groundParent.GetChild(currentGroundChild).gameObject);
            currentGroundChild += 1;
        }
    }

    public void StartGroundFall() => coroutine = StartCoroutine(MakeGroundsFall());

    public void StopGroundFall() => StopCoroutine(coroutine);

    private IEnumerator MakeGroundsFall()
    {
        while (true)
        {
            int randomValue = Random.Range(0, 1000);
            int randomIndex = randomValue % grounds.Count;

            grounds[randomIndex].GetComponent<Rigidbody>().isKinematic = false;
            yield return new WaitForSeconds(fallWaitTime);
        }
    }
}
