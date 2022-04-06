using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrassTile : Tile
{
    [SerializeField] private Color _baseColor, _offsetColor;
    [SerializeField] private GameObject _power, _hp, _damage;

    private Vector3 screenPos;
    private Camera _cam;

    public override void Init(int x, int y, Camera camera) {
        var isOffset = (x + y) % 2 == 1;
        _renderer.color = isOffset ? _offsetColor : _baseColor;
        _cam = camera;

        xPos = x;
        yPos = y;

        screenPos = camera.WorldToScreenPoint(this.transform.position);
    }

    public override void updateText()
    {
        if (OccupiedUnit != null)
        {
            _power.SetActive(true);
            var component = _power.GetComponentInChildren<Text>();
            component.transform.position = screenPos + powerVect; // new Vector3(21, -36.5f, 0);
            component.text = "P: " + OccupiedUnit.Power;

            _hp.SetActive(true);
            component = _hp.GetComponentInChildren<Text>();
            component.transform.position = screenPos + hpVect; // new Vector3(21, -47.5f, 0);
            component.text = "HP: " + OccupiedUnit.HP;

            _damage.SetActive(true);
            component = _damage.GetComponentInChildren<Text>();
            component.transform.position = screenPos + damageVect; // new Vector3(21, -58.5f, 0);
            component.text = "D: " + OccupiedUnit.Damage;
        }
        else
        {
            //Debug.Log("HERE");
            _power.SetActive(false);
            _hp.SetActive(false);
            _damage.SetActive(false);
        }
    }
}
