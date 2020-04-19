using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HandController handController;

    public PlayerGaze playerGaze;

    public event Action<bool> OnHasMagicStateChanged;
    
    private bool _hasMagic = false;

    public bool HasMagic
    {
        get => _hasMagic;
        set
        {
            _hasMagic = value;
            OnHasMagicStateChanged?.Invoke(_hasMagic);
        }
}

    // Start is called before the first frame update
    void Start()
    {
        if (handController == null)
            handController = GetComponent<HandController>() ?? GetComponentInChildren<HandController>();
        if (handController != null)
            handController.player = this;
        
        if (playerGaze == null)
            playerGaze = GetComponent<PlayerGaze>() ?? GetComponentInChildren<PlayerGaze>();

        if (playerGaze != null)
            playerGaze.OnPowerReceived += HandlePowerReceived;
    }

    private void HandlePowerReceived()
    {
        HasMagic = true;
        GetComponent<PlayerSoundController>().BGMchange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        
    }
}
