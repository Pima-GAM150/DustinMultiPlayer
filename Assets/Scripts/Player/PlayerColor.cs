using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class PlayerColor : MonoBehaviourPun
{
    public Renderer[] Rends;

    public List<Material> playerColors;

    public Material CurrentColor { get; set; }
    
    [PunRPC]
    public void SetColor(int order)
    {
        CurrentColor = playerColors[order];

        foreach (Renderer rend in Rends)
            rend.material= CurrentColor;
    }
}
