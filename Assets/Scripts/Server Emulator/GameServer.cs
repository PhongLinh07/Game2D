public enum ERequest
{
    Character = 0,
}

public class GameServer
{
    public static GameServer Instance;


    private void Awake()
    {
        Instance = this;
    }


}


