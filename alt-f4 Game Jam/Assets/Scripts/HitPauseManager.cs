using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HitPauseManager : MonoBehaviour
{

    public static void Pause(int ms)
    {
        HitPauseManager PauseManager = FindObjectOfType<HitPauseManager>();

        PauseManager.StartCoroutine(PauseManager.IPause(ms));
    }

    public IEnumerator IPause(int ms)
    {
        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(ms / 1000);

        Time.timeScale = 1;
    }

    
}
