using UnityEngine;
using TMPro;

public class CoordinateDisplay : MonoBehaviour
{
    public static CoordinateDisplay Instance;

    public Transform HotSpotLocation;

    public TMP_Text CoordinateText;
    public TMP_Text DialogueText;
    public TMP_Text NameText;

    Transform SpaceShipTransform;
    Animator DialogueAnimation;

    void Start()
    {
        if (Instance == null)
            Instance = this;

        SpaceShipTransform = GameObject.FindGameObjectWithTag("GameController").transform;
        DialogueAnimation = GetComponent<Animator>();

        ChangeLocation();

        SetName("Sattelite Dish");
        SetDialogue("GO TO: X: " + HotSpotLocation.position.x + ", Y:" + HotSpotLocation.position.z);
    }

    void Update()
    {
        CoordinateText.SetText("Location: X: " + SpaceShipTransform.position.x.ToString("F0") + ", Y: " + SpaceShipTransform.position.z.ToString("F0"));

        if (Vector3.Distance(HotSpotLocation.position, SpaceShipTransform.position) < 3)
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
        HotSpotLocation.position = new Vector3(Random.Range(-100, 101), 0, Random.Range(-100, 101));
        SetDialogue("GO TO: X: " + HotSpotLocation.position.x + ", Y:" + HotSpotLocation.position.z);
        DialogueAnimation.Play("DialoguePopup", 0, 0);
    }
}
