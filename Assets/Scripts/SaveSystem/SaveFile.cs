using System;
using System.Collections.Generic;

public class SaveFile
{
    public DateTime saveDate;
    public PlayerData playerData;

    public SaveFile() {
        playerData = new PlayerData();
    }
}
