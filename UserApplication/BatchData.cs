using System;
using System.Collections.Generic;
using System.Linq;

namespace UserApplication.App
{
    static class BatchData
    {
        public static List<string> Generate()
        {
            var items = new List<Guid>();

            for (var i = 0; i < Random.Next(2, 9); i++)
            {
                items.Add(Guid.NewGuid());
            }

            return items.Select(i => i.ToString("N")).ToList();
        }

        private static readonly Random Random = new Random(0);
    }
}
