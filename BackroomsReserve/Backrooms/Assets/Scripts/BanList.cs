using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;

public class BanList : MonoBehaviour
{
    public string[] blockedPlayerIDs;
    public GameObject BannedPanel;
    private string playerID;

    private void Start()
    {
        playerID = AuthenticationService.Instance.PlayerId;
        CheckForBan();
    }

    private void CheckForBan()
    {
        foreach (string blockedID in blockedPlayerIDs)
        {
            if (blockedID == playerID)
            {
                BannedPanel.SetActive(true);
                Debug.Log("Игрок заблокирован!");
                // Здесь можно выполнить дополнительные действия, связанные с баном игрока
                return;
            }
        }

        Debug.Log("Игрок не заблокирован.");
    }
}

