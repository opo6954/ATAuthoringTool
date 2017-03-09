using UnityEngine;

/// <summary>
/// The Interactible class flags a Game Object as being "Interactible".
/// Determines what happens when an Interactible is being gazed at.
/// </summary>
public class GazeMagnifier : MonoBehaviour
{
    [Tooltip("Audio clip to play when interacting with this hologram.")]
    public AudioClip TargetFeedbackSound;
    private AudioSource audioSource;
    public float highlightScale = 1.5f;
    float originalScale = 1.0f;
    public bool isEnabled = true;
    public bool isGazed;

    public LogManager logManager;
    public ModeSelector modeSelector;


    void Start()
    {
        isGazed = false;
        // Add a BoxCollider if the interactible does not contain one.
        Collider collider = GetComponentInChildren<Collider>();
        if (collider == null)
        {
            gameObject.AddComponent<BoxCollider>();
        }

        EnableAudioHapticFeedback();
    }

    private void EnableAudioHapticFeedback()
    {
        // If this hologram has an audio clip, add an AudioSource with this clip.
        if (TargetFeedbackSound != null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            audioSource.clip = TargetFeedbackSound;
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1;
            audioSource.dopplerLevel = 0;
            audioSource.volume = 0.5f;
        }
    }

    /* TODO: DEVELOPER CODING EXERCISE 2.d */

    void GazeEntered()
    {
        //Debug.Log("Gaze Entered! : " + this.transform.name);
        LogManager.logType lt = LogManager.logType.Gaze;
        LogManager.LogCollector(lt, Time.realtimeSinceStartup - LogManager.startTime, 0, (int)modeSelector.currentMode);
        isGazed = true;
        // WARNING : hard coding
        transform.parent.GetComponent<GazeLogger>().isGazed = true;
        if (isEnabled)
        {
            this.GetComponent<Control_objectify>().isObjectify = true;

            // Play the audioSource feedback when we gaze and select a hologram.
            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
            
    }

    void GazeExited()
    {
        isGazed = false;
        if (isEnabled)
            this.GetComponent<Control_objectify>().isObjectify = false;
    }

    void OnSelect()
    {
        Debug.Log("Selected");
        /* TODO: DEVELOPER CODING EXERCISE 6.a */
        // 6.a: Handle the OnSelect by sending a PerformTagAlong message.
        this.SendMessage("PerformTagAlong");
    }
}