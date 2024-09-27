using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new dialogue", menuName = "Dialogue", order = 100)]
public class dialogueObject : ScriptableObject
{


    [System.Serializable]
    public class dialogueScreen
    {
        public string Name;

        public float Delay;

        [Header("Dialogue")]
        [TextArea]
        [SerializeField]
        string Text;


        public string GetText()
        {
            return Text;
        }
    }

    public List<dialogueScreen> dialogue;
}