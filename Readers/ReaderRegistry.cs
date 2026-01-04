using UnityEngine;
using System.Reflection;

namespace DisplayMachineryDetail.Readers;

[AttributeUsage(AttributeTargets.Class)]
public class BehaviourReaderAttribute : Attribute
{
    public Type BehaviourType { get; }
    public bool IsDamageReader { get; }

    public BehaviourReaderAttribute(Type behaviourType, bool isDamageReader = false)
    {
        BehaviourType = behaviourType;
        IsDamageReader = isDamageReader;
    }
}

public static class ReaderRegistry
{
    private static readonly Dictionary<Type, Type> MachineryReaderMap = new();
    private static readonly Dictionary<Type, Type> DamageReaderMap = new();
    private static bool _initialized;

    public static void Initialize()
    {
        if (_initialized) return;
        _initialized = true;

        var readerTypes = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IAttributeReader).IsAssignableFrom(t));

        foreach (var readerType in readerTypes)
        {
            var attribute = readerType.GetCustomAttribute<BehaviourReaderAttribute>();
            if (attribute != null)
            {
                if (attribute.IsDamageReader)
                {
                    DamageReaderMap[attribute.BehaviourType] = readerType;
                }
                else
                {
                    MachineryReaderMap[attribute.BehaviourType] = readerType;
                }
            }
        }
    }

    public static IAttributeReader CreateMachineryReader(GameObject gameObject)
    {
        return CreateReader(gameObject, MachineryReaderMap);
    }

    public static IAttributeReader CreateDamageReader(GameObject gameObject)
    {
        return CreateReader(gameObject, DamageReaderMap);
    }

    private static IAttributeReader CreateReader(GameObject gameObject, Dictionary<Type, Type> readerMap)
    {
        foreach (var kvp in readerMap)
        {
            var behaviourType = kvp.Key;
            var readerType = kvp.Value;

            Component component = null;

            if (behaviourType == typeof(WinchBehaviour))
            {
                component = gameObject.GetComponentInChildren(behaviourType);
            }
            else
            {
                component = gameObject.GetComponent(behaviourType);
            }

            if (component != null)
            {
                return (IAttributeReader)Activator.CreateInstance(readerType, component);
            }
        }

        return null;
    }
}