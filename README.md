# GameEngineProject

Name: Edmond Huang

Student Number: 100923160

This is a Solo Project.

This interactive media is an audio-focused first-person game. There are 4 pillars, each assigned with a specific note. There would be an orb in the air that can turn green, red or blue. When the orb is blue, it plays a sequence of 4 notes, after it's done playing the notes, the player is able to try to match the sequence of notes played by making each pillar play their note by pressing E. If the sequence of notes played by the player matches what the orb has played, the orb will turn green, indicating it is correct or red, indicating it is wrong. The notes played by the orb are randomized each time. Guessing the right note sequences 5 times in a row will result in a win; if not, it will keep resetting back to 0 wins.

<img width="2362" height="1318" alt="image" src="https://github.com/user-attachments/assets/b54385ba-be88-4d0d-a959-6ffed14b46bd" />


<img width="427" height="382" alt="image" src="https://github.com/user-attachments/assets/f54db430-df48-45c0-9c61-7f21a80002b3" />

Currently, the pillars are simple squares

I focused on the design pattern, so a good amount of the gameplay has not yet been implemented.

**[Singleton]**

CLASS AudioManager
    STATIC Instance

    METHOD Awake()
        IF Instance EXISTS AND Instance != THIS
            DESTROY THIS
            RETURN
        ENDIF
        Instance ← THIS
        MAKE_PERSISTENT(THIS)
    END

    METHOD PlaySound(eventRef, worldPos)
        IF eventRef IS_NULL
            RETURN
        ENDIF
        FMOD.PlayOneShot(eventRef, worldPos)
    END
END

The Singleton pattern was implemented through an AudioManager class that ensures only one audio system exists in the game. In the Awake() method, the script checks for an existing instance and destroys duplicates, while DontDestroyOnLoad() keeps it active between scenes. It was implemented this way to prevent multiple audio controllers from conflicting or playing duplicate sounds. This pattern benefits my interactive experience by providing a single, global point of control for all sound effects, keeping the audio system consistent and easy to access from any script without having to manually link references.

**[Factory]**

CLASS NoteFactory
    FIELD Crystal1Event
    FIELD Crystal2Event
    FIELD Crystal3Event
    FIELD Crystal4Event

    METHOD GetNoteSound(pillarIndex : INT) : EventRef
        SWITCH pillarIndex
            CASE 1: RETURN Crystal1Event
            CASE 2: RETURN Crystal2Event
            CASE 3: RETURN Crystal3Event
            CASE 4: RETURN Crystal4Event
            DEFAULT: LOG_ERROR("Invalid pillar index"); RETURN NULL_EVENT
        END
    END
END

The Factory pattern was implemented in the NoteFactory class, which creates and manages different FMOD sound events. When a pillar is activated, it requests a sound through GetNoteSound(pillarIndex), and the factory returns the correct FMOD event based on the pillar’s index. This was implemented to separate sound selection from the pillars themselves, so each pillar doesn’t need to store or manage its own sound logic. This pattern benefits my interactive media experience by making sound management modular and scalable; new sound types can be added or replaced through the factory without editing every pillar script, improving maintainability.

**[Command]**

INTERFACE INoteCommand
    METHOD Execute()
END

CLASS NoteCommand IMPLEMENTS INoteCommand
    FIELD eventRef
    FIELD position

    CONSTRUCTOR(eventRef, position)
        THIS.eventRef ← eventRef
        THIS.position ← position
    END

    METHOD Execute()
        AudioManager.Instance.PlaySound(eventRef, position)
    END
END

The Command pattern was implemented through the INoteCommand interface and the NoteCommand class. Each command object encapsulates the action of playing a sound, storing its FMOD event and position, and then calling Execute() to play it through the AudioManager. It was implemented this way to separate the trigger (player pressing E) from the action (playing the note). This benefits the interactive experience by making interactions more flexible; future commands like lighting effects or note sequences could easily be added and executed the same way, improving the extensibility and structure of the gameplay logic.

**[Plugin]**

I used FMOD for plugins/DLL. FMOD is a great plugin for audio-related things in game design. FMOD separates sound design from game logic, making everything clean and clearer to use. It also pairs well with the design patterns required for the assignment. Plus, it has versatile and easy-to-use 3D and 2D audio editing tools that can be instantly applied to the game with only a few lines of code.

**[Video]**

The video file was too big, so I uploaded it to YouTube.

https://youtu.be/EDuvdVABV_U
