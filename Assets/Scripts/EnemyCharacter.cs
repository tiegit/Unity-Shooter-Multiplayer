using SchemaTest.InstanceSharing;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyCharacter : MonoBehaviour
{
    private Vector3 _position;
    private float _tactTime;

    private List<Vector3> _positionsList = new List<Vector3>();
    private List<float> _tactTimeList = new List<float>();

    private Vector3 _oldPosition = Vector3.zero;
    private Vector3 _newPosition;

    [SerializeField] private float _speedModifaer = 100f;



    private void Start()
    {
        _oldPosition = transform.position;

        for (int i = 0; i < 5; i++)
        {
            _positionsList.Add(Vector3.zero);
            _tactTimeList.Add(0);
        }
    }
    public void SetMoveData(Vector3 position, float tactTime)
    {
        _position = position;
        _tactTime = tactTime;
        //Debug.Log(tactTime);

        MoveEnemy();
    }

    private void MoveEnemy() //Обработка потока входящих координат и времени такта
    {
        _newPosition = _position;

        _positionsList.Add(_newPosition);
        _positionsList.RemoveAt(0);

        Vector3 resultVector = Vector3.zero;

        for (int i = 0; i < _positionsList.Count; i++)
        {
            resultVector += _positionsList[i];
        }
        Vector3 direction = resultVector.normalized;

        _tactTimeList.Add(_tactTime);
        _tactTimeList.RemoveAt(0);
        float averageTactTime = _tactTimeList.Average();

        transform.position = Vector3.Lerp(_oldPosition, _newPosition + direction * averageTactTime, Time.deltaTime * _speedModifaer); // использовать среднее всремя и среднее направление

        _oldPosition = transform.position;
    }
}
