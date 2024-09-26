using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class HitPauseManager : MonoBehaviour
{

    public static async void Pause(int ms)
    {
        Time.timeScale = 0;

        await Task.Delay(ms);

        Time.timeScale = 1;
    }

    
}
