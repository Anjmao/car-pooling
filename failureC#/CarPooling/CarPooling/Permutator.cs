using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarPooling
{
    public class Permutator
    {
        public static IEnumerable<string> Combinations(List<string> characters, int length)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                // only want 1 character, just return this one
                if (length == 1)
                    yield return characters[i];

                // want more than one character, return this one plus all combinations one shorter
                // only use characters after the current one for the rest of the combinations
                else
                    foreach (string next in Combinations(characters.GetRange(i + 1, characters.Count - (i + 1)), length - 1))
                        yield return characters[i] + next;
            }
        }
    }
}
