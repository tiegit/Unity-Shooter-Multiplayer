using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerCharacter _player;
    [SerializeField] private const float OutFPSModifier = 0.05f;

    private void Start()
    {
        InvokeRepeating("SendMove", 0, OutFPSModifier); //�������� ��������� �� ������ 20 ���/��� �� ������ ������, ���� �������� ����������
    }
    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        _player.SetInput(h, v);
        //SendMove(); // ������� ����� ���������� ��������� �� ������ >60 ���/���
    }
    
    private void SendMove()
    { 
        _player.GetMoveInfo(out Vector3 position);
        Dictionary<string, object> data = new Dictionary<string, object>()
        {
            { "x", position.x },
            { "y", position.z }
        };

        MultiplayerManager.Instance.SendMessage("move", data); // ��� ����������� ���� ��� �������� ����� ������
    }
}
