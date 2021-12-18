using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem.Switch;

public class GameManager : SwitchScene
{
    private int score = 0;
    public GameObject userInterface;
    private float startingTime = 60f;
    private float currentTime = 0;
    private bool weapoonPickedUp;
    private bool gameEnded;

    private void Start()
    {
        weapoonPickedUp = false;
        gameEnded = false;
        currentTime = startingTime;
    }

    private void Update()
    {
        if (weapoonPickedUp)
        {
            currentTime -= 1 * Time.deltaTime;
        }

        if (currentTime <= 0)
        {
            gameEnded = true;
            userInterface.SetActive(true);
            StartCoroutine(ReturnToGameMenu());
        }
    }

    public bool GetCurrentGameEnded()
    {
        return gameEnded;
    }

    public int GetCurrentGameScore()
    {
        return score;
    }

    public float GetCurrentGameTime()
    {
        return currentTime;
    }

    public void AddToScore(int x)
    {
        score = score + x;
    }

    public void RemoveFromScore(int x)
    {
        if (score != 0)
        {
            score = score - x;
        }
    }

    public bool GetWeaponPickedUp()
    {
        return weapoonPickedUp;
    }

    public void PickedUpWeapon()
    {
        weapoonPickedUp = true;
    }

    private IEnumerator ReturnToGameMenu()
    {
        yield return new WaitForSeconds(10);
        ReturnToShootingRange();
    }
}