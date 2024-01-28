namespace GGJ2024
{
    public enum PlayerEnum
    {
        P1,
        P2
    }
    
    public enum GameState
    {
        Playing,
        Waiting,
        Pause,
        GameOver
    }

    public enum PlayerState
    {
        Normal,
        WaitingForRespawn,
        Invincible,
        Attacking
    }
}