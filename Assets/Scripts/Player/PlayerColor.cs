using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using Sirenix.OdinInspector;


public class PlayerColor : MonoBehaviourPun
{
    #region Variables
    [BoxGroup("Player Color Options",true,true),LabelText("Player Pieces Renderers")]
    public Renderer[] Rends;

    [BoxGroup("Player Color Options", true, true), LabelText("Player Colors Available")]
    public List<Material> playerColors;

    [BoxGroup("Player Color Options", true, true), LabelText("Current Player Color"), ReadOnly]
    public Material CurrentColor;
    #endregion

    [PunRPC]
    public void SetColor(int order)
    {
        CurrentColor = playerColors[order];

        foreach (Renderer rend in Rends)
            rend.material= CurrentColor;
    }
}
