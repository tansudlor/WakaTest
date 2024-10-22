
public class GameUIManager
{

    private static GameUIManager _instance;

    public UIView UIView;

    //public GameView GameView;

    public static GameUIManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameUIManager();
        }
        return _instance;
    }

}


