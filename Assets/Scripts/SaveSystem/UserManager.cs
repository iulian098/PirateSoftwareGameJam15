public static class UserManager
{
    static PlayerData playerData;

    public static PlayerData PlayerData
    {
        get
        {
            if (playerData == null)
                playerData = new PlayerData();
            return playerData;
        }
    }

    public static void SetPlayerData(PlayerData data) {
        playerData = data;
    }
}
