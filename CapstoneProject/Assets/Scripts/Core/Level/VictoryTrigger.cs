using Core.Entity;
using Core.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject VictoryUIHolder;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("In trigger");
        if(collision.GetComponent<Player>())
        {
            VictoryUIHolder.SetActive(true);
            GameState.Instance.CurrentGameState = GameState.States.CutScene;
            Time.timeScale = 0.0f;
            SoundManager.Instance.ChangeMusicVolume(0);
            SoundManager.Instance.ChangeSoundVolume(0);
        }
    }
}
