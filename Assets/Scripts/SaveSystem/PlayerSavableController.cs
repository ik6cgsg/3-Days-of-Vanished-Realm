using UnityEngine;

public class PlayerSavableController : ISavableObject
{
    public static string LOAD_COORDINATES = "loadCoordinates";
    public static string PLAYER_ROTATION_Y = "PlayerRotationY";

    public override void Save()
    {
        SaveFloat("x", transform.position.x);
        SaveFloat("y", transform.position.y);
        SaveFloat("z", transform.position.z);
    }

    public override void Load()
    {
        if (LoadGlobalBool(LOAD_COORDINATES))
        {
            transform.position = new Vector3(LoadFloat("x"),
                                             LoadFloat("y"),
                                             LoadFloat("z"));
            transform.Rotate(0, LoadGlobalFloat(PLAYER_ROTATION_Y), 0);
            SaveGlobalFloat(PLAYER_ROTATION_Y, 0);
        }
    }
}
