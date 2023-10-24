using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCL.Pages
{
    class Solver
    {
        public List<Move> Moves { get; set; } = new List<Move>();

        public List<Move> SolveHanoi(int DiskCount)
        {
            Moves.Clear();
            moveDisk(DiskCount, 1, 3, 2);
            return Moves;
        }

        public void moveDisk(int n, int from, int to, int on)
        {
            if (n == 1)
            {
                Moves.Add(new Move(from, to));
            }
            else
            {
                moveDisk(n - 1, from, on, to);
                moveDisk(1, from, to, on);
                moveDisk(n - 1, on, to, from);
            }
        }
    }

    class Move
    {
        public int poleFrom { get; set; }
        public int poleTo { get; set; }

        public Move(int from, int to)
        {
            poleFrom = from;
            poleTo = to;
        }
    }
}