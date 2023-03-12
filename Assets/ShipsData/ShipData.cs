using UnityEngine;

[CreateAssetMenu(fileName = "New ShipData", menuName = "Ship Data", order = 51)]
public class ShipData : ScriptableObject
{
    [SerializeField] private ShipType type;
    [SerializeField] [Range(1, 4)] private int lengthX50;
    [SerializeField] private float moveSpeed;

    [SerializeField] private float rotationSpeed;

    [SerializeField] private Sprite[] lights = new Sprite[4];
    [SerializeField] private Sprite flag;
    [SerializeField] private Sprite soundSprite;
    [SerializeField] private Sprite shipSprite;
    [SerializeField] private AudioClip soundClip;

    public ShipType ShipType => type;
    public AudioClip SoundClip => soundClip;
    public int Length => lengthX50 * 50;
    public float Size => (float) (lengthX50 * 0.5);
    public float MoveSpeed => moveSpeed * 1.85f * 0.30f * 0.5f * 0.5f;
    public float DataMoveSpeed => moveSpeed;

    public Sprite ShipSprite => shipSprite;


    public float RotationSpeed => rotationSpeed;

    // TODO: class Light{Front, Back, Left, Right, getByDirection(float)}
    public Sprite LightFront => lights[0];
    public Sprite LightBack => lights[1];
    public Sprite LightLeft => lights[2];
    public Sprite LightRight => lights[3];
    public Sprite[] Lights => lights;
    public Sprite Flag => flag;
    public Sprite SoundSprite => soundSprite;

    public Sprite GetLight(ShipSide side)
    {
        return lights[(int) side];
    }
}