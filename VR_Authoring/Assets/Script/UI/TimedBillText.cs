
using UnityEngine;
using HoloToolkit.Unity;

public class TimedBillText : MonoBehaviour
{
    /// <summary>
    /// The axis about which the object will rotate.
    /// </summary>
    /// 
    enum BlendingState { READY, FADEIN, SHOW, FADEOUT, FINISHED };

    public PivotAxis PivotAxis = PivotAxis.Free;

    public float textAlphaOffset = 0.1f;
    public int contentCountOffset = 15;

    BlendingState currentState = BlendingState.READY;
    float textAlpha;
    int contentCount;
    private void Start()
    {
        textAlpha = 0.0f;

        contentCount = GetComponent<TextMesh>().text.Length* contentCountOffset;
        
    }

    public void UpdateText(string text)
    {
        GetComponent<TextMesh>().text = text;
        contentCount = GetComponent<TextMesh>().text.Length * contentCountOffset;
    }

    private void OnEnable()
    {
        Update();
    }

    private void FadeIn()
    {
        currentState = BlendingState.FADEIN;
        if (textAlpha < 1.0f)
            textAlpha += textAlphaOffset * Time.deltaTime;
        else
            currentState = BlendingState.SHOW;
        GetComponent<MeshRenderer>().material.color = new Color(GetComponent<MeshRenderer>().material.color.r, GetComponent<MeshRenderer>().material.color.g, GetComponent<MeshRenderer>().material.color.b, textAlpha);
    }

    private void FadeOut()
    {
        currentState = BlendingState.FADEOUT;
        if (textAlpha > 0.0f)
            textAlpha -= textAlphaOffset * Time.deltaTime;
        else
        {
            textAlpha = 0.0f;
            currentState = BlendingState.FINISHED;
        }            
        GetComponent<MeshRenderer>().material.color = new Color(GetComponent<MeshRenderer>().material.color.r, GetComponent<MeshRenderer>().material.color.g, GetComponent<MeshRenderer>().material.color.b, textAlpha);
    }

    private void HoldText()
    {
        if (contentCount > 0)
            contentCount--;
        else
        {
            contentCount = GetComponent<TextMesh>().text.Length * contentCountOffset;
            currentState = BlendingState.FADEOUT;
        }
    }

    private void BlendText()
    {
        if (currentState == BlendingState.READY || currentState == BlendingState.FADEIN)
            FadeIn();
        else if (currentState == BlendingState.SHOW)
            HoldText();
        else if (currentState == BlendingState.FADEOUT)
            FadeOut();
    }

    public void ShowText(string text)
    {
        // we'll show only new text
        if (text.Equals(GetComponent<TextMesh>().text))
            return;

        UpdateText(text);
        ShowText();
    }

    public void ShowText()
    {
        currentState = BlendingState.READY;
    }

    /// <summary>
    /// Keeps the object facing the camera.
    /// </summary>
    private void Update()
    {
        if (!Camera.main)
        {
            return;
        }

        BlendText();

        // Get a Vector that points from the target to the main camera.
        Vector3 directionToTarget = Camera.main.transform.position - transform.position;

        // Adjust for the pivot axis.
        switch (PivotAxis)
        {
            case PivotAxis.Y:
                directionToTarget.y = 0.0f;
                break;

            case PivotAxis.Free:
            default:
                // No changes needed.
                break;
        }

        // If we are right next to the camera the rotation is undefined. 
        if (directionToTarget.sqrMagnitude < 0.001f)
        {
            return;
        }

        // Calculate and apply the rotation required to reorient the object
        transform.rotation = Quaternion.LookRotation(-directionToTarget);
    }
}