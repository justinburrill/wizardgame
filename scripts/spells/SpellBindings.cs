using System;
using System.Collections.Generic;

namespace wizardgame.scripts.spells
{
    public class SpellBindings
    {
        Dictionary<AttackButton, SpellType> Earth = new();
        Dictionary<AttackButton, SpellType> Water = new();
        Dictionary<AttackButton, SpellType> Fire = new();
        Dictionary<AttackButton, SpellType> Air = new();

        private Dictionary<AttackButton, SpellType> GetDictFromElement(Element e)
        {
            Dictionary<AttackButton, SpellType> dict = e switch
            {
                Element.Air => Air,
                Element.Fire => Fire,
                Element.Earth => Earth,
                Element.Water => Water,
                _ => throw new Exception("nah what the hell bruh"),
            };
            return dict;
        }

        public SpellType? GetSpell(AttackButton ab, Element e)
        {
            var dict = GetDictFromElement(e);
            if (!dict.ContainsKey(ab))
            {
                return null;
            }
            return dict?[ab];
        }

        public void AssignSpell(AttackButton ab, SpellType st, Element e)
        {
            //Dictionary<AttackButton, Spell> dict = value.element switch
            //{
            //    Element.Air => Air,
            //    Element.Fire => Fire,
            //    Element.Earth => Earth,
            //    Element.Water => Water,
            //    _ => throw new Exception("nah what the hell bruh"),
            //};
            var dict = GetDictFromElement(e);
            dict.Add(ab, st);
        }
    }
}
