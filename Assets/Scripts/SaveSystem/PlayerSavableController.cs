using UnityEngine;

public class PlayerSavableController : ISavableObject
{
    public static string LOAD_COORDINATES = "loadCoordinates";

    public override void Save()
    {
        SaveFloat("x", transform.position.x);
        SaveFloat("y", transform.position.y);
        SaveFloat("z", transform.position.z);

        Debug.Log("Saved player position");
        Debug.Log(transform.position);
    }

    public override void Load()
    {
        if (LoadGlobalBool(LOAD_COORDINATES))
        {
            transform.position = new Vector3(LoadFloat("x"),
                                             LoadFloat("y"),
                                             LoadFloat("z"));
            Debug.Log("Loaded player position");
            Debug.Log(transform.position);
        }
    }
}
