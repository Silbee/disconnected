using UnityEngine;
using TMPro;

public class CoordinateDisplay : MonoBehaviour
{
    public static CoordinateDisplay Instance;

    public Transform HotSpotMinimapLocation;
    public Transform SpaceShipMinimapTransform;
    public Transform FakeHotSpotMinimapLocation;

    public TMP_Text CoordinateText;
    public TMP_Text DialogueText;
    public TMP_Text NameText;

    Vector3 DesiredLocation;

    MeshRenderer FakeHotspotMinimapLocationRenderer;
    Transform SpaceShipTransform;
    Animator DialogueAnimation;

    void Start()
    {
        if (Instance == null)
            Instance = this;

        FakeHotspotMinimapLocationRenderer = FakeHotSpotMinimapLocation.GetComponentInChildren<MeshRenderer>();

        SpaceShipTransform = GameObject.FindGameObjectWithTag("GameController").transform;
        DialogueAnimation = GetComponent<Animator>();

        ChangeLocation();

        SetName("Sattelite Dish");
        SetDialogue("GO TO: X: " + DesiredLocation.x + ", Y:" + DesiredLocation.z);
    }

    void Update()
    {
        CoordinateText.SetText("Location: X: " + SpaceShipTransform.position.x.ToString("F0") + ", Y: " + SpaceShipTransform.position.z.ToString("F0"));

        if (Vector3.Distance(HotSpotMinimapLocation.position, SpaceShipTransform.position) < 3)
            ChangeLocation();

        Vector3 direction = HotSpotMinimapLocation.position - SpaceShipTransform.position;

        if (Vector3.Distance(SpaceShipTransform.position, DesiredLocation) > 39)
        {
            FakeHotspotMinimapLocationRenderer.enabled = true;
            FakeHotSpotMinimapLocation.rotation = Quaternion.Euler(0, SpaceShipTransform.position.x > HotSpotMinimapLocation.position.x ? -Vector3.Angle(direction, SpaceShipMinimapTransform.forward) : Vector3.Angle(direction, SpaceShipMinimapTransform.forward), 0);
        }
        else
        {
            FakeHotspotMinimapLocationRenderer.enabled = false;
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
        DesiredLocation = new Vector3(Random.Range(-25, 26), 0, Random.Range(-25, 26));
        HotSpotMinimapLocation.position = DesiredLocation;

        SetDialogue("GO TO: X: " + DesiredLocation.x + ", Y:" + DesiredLocation.z);
        DialogueAnimation.Play("DialoguePopup", 0, 0);
    }
}
