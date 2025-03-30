using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[CreateAssetMenu(fileName = "Receipts", menuName = "Scriptable Objects/Receipts")]
public class Receipts : ScriptableObject
{
    [SerializeField] private List<Receipt> receipts;
    public List<Receipt> ReceiptsData => receipts;
}
