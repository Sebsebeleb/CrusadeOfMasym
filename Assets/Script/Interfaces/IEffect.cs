using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public interface IEffect
{

}

public interface ISpellEffect : IEffect
{
    void OnUseCard(Owner caster, MapPosition target);
}