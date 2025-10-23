using FMODUnity;
using UnityEngine;

public interface INoteCommand
{
    void Execute();
}

public class NoteCommand : INoteCommand
{
    private readonly EventReference _soundEvent;
    private readonly Vector3 _pos;

    public NoteCommand(EventReference soundEvent, Vector3 pos)
    {
        _soundEvent = soundEvent;
        _pos = pos;
    }

    public void Execute()
    {
        AudioManager.Instance.PlaySound(_soundEvent, _pos);
    }
}
