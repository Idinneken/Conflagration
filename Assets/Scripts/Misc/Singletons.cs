using Extensions;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

public class Singletons : Singleton<Singletons>
{
    [field: SerializeField] public GameObject playerGameObject { get; private set; }
    [field: SerializeField] public Player player { get; private set; }

    protected override void OnAwake()
    {
        CheckFields();
        base.OnAwake();
    }

    public void CheckFields()
    {
        foreach (FieldInfo fieldInfo in GetType().GetFields())
        {
            if (fieldInfo.GetValue(this).IsNull())
            {
                Debug.LogWarning("'" + fieldInfo.Name + "' on the LevelManager isn't set to anything. Please set it to what it's meant to so our code works!");
            }
        }
    }
}
