using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(Collider))]
public class PillarNote : MonoBehaviour
{
    [Range(1, 4)] public int pillarIndex = 1;
    public NoteFactory noteFactory;
    public Transform player;

    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    bool _loggedNoPlayer, _loggedNoFactory;

    void Awake()
    {
        if (player == null)
        {
            var tagged = GameObject.FindGameObjectWithTag("Player");
            if (tagged != null) player = tagged.transform;
            else if (Camera.main != null && Camera.main.transform.parent != null)
                player = Camera.main.transform.parent;
        }
    }

    void Update()
    {
        if (noteFactory == null)
        {
            if (!_loggedNoFactory)
            {
                Debug.LogError($"[{name}] NoteFactory not assigned.");
                _loggedNoFactory = true;
            }
            return;
        }

        if (player == null)
        {
            if (!_loggedNoPlayer)
            {
                Debug.LogError($"[{name}] Player not found.");
                _loggedNoPlayer = true;
            }
            return;
        }

        Vector3 a = player.position; a.y = 0f;
        Vector3 b = transform.position; b.y = 0f;
        float d = Vector3.Distance(a, b);

        if (d <= interactDistance && Input.GetKeyDown(interactKey))
        {
            var evt = noteFactory.GetNoteSound(pillarIndex);
            if (evt.IsNull)
            {
                Debug.LogError($"[{name}] EventReference is NULL for index {pillarIndex}.");
                return;
            }

            INoteCommand cmd = new NoteCommand(evt, transform.position);
            cmd.Execute();
        }
    }
}
