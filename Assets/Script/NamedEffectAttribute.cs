using System;

/// <summary>
/// Used to identify effects
/// </summary>
class NamedEffectAttribute : System.Attribute
{
    public readonly string EffectID;

    public NamedEffectAttribute(string id)
    {
        EffectID = id;
    }
}
