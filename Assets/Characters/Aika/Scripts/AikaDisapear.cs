using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AikaDisapear : MonoBehaviour
{
    [SerializeField] List<Renderer> meshies;
    [SerializeField] bool dissolve;
    [SerializeField] float progressVelocity = 1f;

    float progress = 1f;
    float nonDissolvedProgress = 0f;
    float dissolvedProgress = 1f;

    bool isRunning;

    IEnumerator coroutine;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
        {
            if ((dissolve == true) && (progress <= nonDissolvedProgress ))
            {
                coroutine = DissolveTo(dissolvedProgress);
                StartCoroutine(coroutine);
            }
            else if ((dissolve == false) && (progress >= dissolvedProgress))
            {
                coroutine = DissolveTo(nonDissolvedProgress);
                StartCoroutine(coroutine);
            }
        }
    }

    IEnumerator DissolveTo(float destination)
    {
        isRunning = true;

        Debug.Log("DissolveTo");

        while (progress != destination)
        {
            float direction = destination - progress;
            float absDirection = Mathf.Abs(direction);
            float deltaToApply = progressVelocity * Time.deltaTime;
            progress += Mathf.Min(absDirection, deltaToApply) * Mathf.Sign(direction);

            ApplyProgressToAllMeshes(progress);

            yield return new WaitForEndOfFrame();
            //Debug.Log($"...... {progress} - {destination}");
        }

        //Debug.Log("... finished");

        isRunning = false;
    }

    void ApplyProgressToAllMeshes(float progress)
    {
        foreach (Renderer r in meshies)
        {
            Material mat = r.sharedMaterial;
            mat.SetFloat("_Disolve_Amount", progress);
        }
    }


}
