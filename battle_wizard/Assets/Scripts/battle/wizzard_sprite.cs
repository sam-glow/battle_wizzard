using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wizzard_sprite : MonoBehaviour
{
    [SerializeField] private GameObject aggressive;
    [SerializeField] private GameObject almost_dead;
    [SerializeField] private GameObject almost_win;
    [SerializeField] private GameObject being_hit;
    [SerializeField] private GameObject celebration;
    [SerializeField] private GameObject dead;
    [SerializeField] private GameObject relax;
    [SerializeField] private GameObject struggling;

    public GameObject Wand()
    {
        foreach (Transform t in transform)
            if(t.gameObject.active)
                return t.GetChild(0).gameObject;

        return this.gameObject;
    }
    public void SetState(wizzard_visual.state _s)
    {
        aggressive.SetActive(false);
        almost_dead.SetActive(false);
        almost_win.SetActive(false);
        being_hit.SetActive(false);
        celebration.SetActive(false);
        dead.SetActive(false);
        relax.SetActive(false);
        struggling.SetActive(false);

        switch (_s)
        {
            case wizzard_visual.state.aggresive:
                aggressive.SetActive(true);
                break;
            case wizzard_visual.state.almost_dead:
                almost_dead.SetActive(true);
                break;
            case wizzard_visual.state.slmost_win:
                almost_win.SetActive(true);
                break;
            case wizzard_visual.state.hit:
                being_hit.SetActive(true);
                break;
            case wizzard_visual.state.celebrate:
                celebration.SetActive(true);
                break;
            case wizzard_visual.state.dead:
                dead.SetActive(true);
                break;
            case wizzard_visual.state.relax:
                relax.SetActive(true);
                break;
            case wizzard_visual.state.striggle:
                struggling.SetActive(true);
                break;
        }
    }
}
