using FMODUnity;
using UnityEngine;

public class NoteFactory : MonoBehaviour
{
    public EventReference Crystal1;
    public EventReference Crystal2;
    public EventReference Crystal3;
    public EventReference Crystal4;

    public EventReference GetNoteSound(int pillarIndex)
    {
        switch (pillarIndex)
        {
            case 1: return Crystal1;
            case 2: return Crystal2;
            case 3: return Crystal3;
            case 4: return Crystal4;
            default:
                Debug.LogError("Invalid pillar index!");
                return new EventReference();
        }
    }
}
