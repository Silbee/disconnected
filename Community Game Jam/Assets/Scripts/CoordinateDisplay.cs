using UnityEngine;
using TMPro;

public class CoordinateDisplay : MonoBehaviour
{
    public static CoordinateDisplay Instance;

    public TMP_Text CoordinateText;
    public TMP_Text DialogueText;
    public TMP_Text NameText;

    Transform SpaceShipTransform;
    Animator DialogueAnimation;

    Vector3 DesiredLocation;

    void Start()
    {
        if (Instance == null)
            Instance = this;

        SpaceShipTransform = GameObject.FindGameObjectWithTag("GameController").transform;
        DialogueAnimation = GetComponent<Animator>();

        ChangeLocation();

        SetName("Sattelite Dish");
        SetDialogue("GO TO: X: " + DesiredLocation.x + ", Y:" + DesiredLocation.z);
    }

    void Update()
    {
        CoordinateText.SetText("Location: X: " + SpaceShipTransform.position.x.ToString("F0") + ", Y: " + SpaceShipTransform.position.z.ToString("F0"));

        if (Vector3.Distance(DesiredLocation, SpaceShipTransform.position) < 5)
        {
            ChangeLocation();
        }
    }


    public void SetDialogue(string text)
    {
        DialogueText.SetText(text);
    }


    public void SetName(string text)
    {
        NameText.SetText(text);
    }


    void ChangeLocation()
    {
        DesiredLocation = new Vector3(Random.Range(-100, 101), 0, Random.Range(-100, 101));
        SetDialogue("GO TO: X: " + DesiredLocation.x + ", Y:" + DesiredLocation.z);
        DialogueAnimation.Play("DialoguePopup");
    }
}
