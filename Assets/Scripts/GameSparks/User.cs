using UnityEngine;

[CreateAssetMenu(fileName = "New User", menuName = "Plinko/User")]
public class User : ScriptableObject
{
    public string Name { get; set; }
    public int Played { get; set; }
    public int Won { get; set; }
    public int Lost { get; set; }
    public int Made { get; set; }
    public int Received { get; set; }
    public int Average { get; set; }
    public string Vertical { get; set; }
    public string Room { get; set; }
}