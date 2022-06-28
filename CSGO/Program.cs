using System;
using System.Collections.Generic;
using static CSGO.Terrorist;
using static CSGO.CounterTerrorist;

namespace CSGO
{
    class Program
    {
        public static Random rand = new Random();
        public static bool GameOver = false;
        internal static Terrorist[] terrorists;
        internal static CounterTerrorist[] counterterrorists;

        static void Main(string[] args)
        {
            FillTeams(5, 5);

            while (!BombPlanted && !GameOver) NextTurn();

            if (BombPlanted && !GameOver) FinalFifteenTurns();
        }

        private static void FillTeams(int tCount, int ctCount)
        {
            var ts = new List<Terrorist>();
            var cts = new List<CounterTerrorist>();
            for (int i = 0; i < tCount; i++) ts.Add(new Terrorist());
            for (int i = 0; i < ctCount; i++) cts.Add(new CounterTerrorist());
            terrorists = ts.ToArray();
            counterterrorists = cts.ToArray();
        }

        private static void NextTurn()
        {
            TerroristsTurn();
            CounterTerroristsTurn();
            Console.WriteLine();
        }

        private static void FinalFifteenTurns()
        {
            for (int turnsLeft = 15; turnsLeft > 0; turnsLeft--)
            {
                WriteLineRed($"The bomb will explode in {turnsLeft} turns.");
                NextTurn();
                if (GameOver) break;
                if (turnsLeft == 1) WriteLineRed("\nThe bomb exploded! The terrorists won!");
            }
        }

        public static void WriteLineRed(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void WriteLineGreen(string text)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void TerroristsTurn()
        {
            if (GameOver || TerroristsDead()) return;
            if (BombSite_Discovered && !BombPlanted) PlantBomb();
            else if (!BombPlanted && rand.Next(2) == 1) FindBombSite();
            else
            {
                foreach (var t in LivingTerrorists())
                {
                    if (!GameOver) t.KillCounterTerrorist(RandomCounterTerrorist());
                }
            }
        }

        private static void CounterTerroristsTurn()
        {
            if (GameOver) return;
            if (!TerroristsDead())
            {
                foreach (var ct in LivingCounterTerrorists())
                {
                    if (!TerroristsDead()) ct.KillTerrorist(RandomTerrorist());
                }
            }
            else if (BombPlanted) DefuseBomb();
        }

        public static bool IsSuccessful(int maxValue)
        {
            return rand.Next(0, maxValue) == 2;
        }

        private static Terrorist RandomTerrorist()
        {
            var livingTs = LivingTerrorists();
            var targetedT = livingTs[rand.Next(livingTs.Length)];
            return targetedT;
        }

        private static CounterTerrorist RandomCounterTerrorist()
        {
            var livingCTs = LivingCounterTerrorists();
            var targetedCT = livingCTs[rand.Next(livingCTs.Length)];
            return targetedCT;
        }

        public static Terrorist[] LivingTerrorists()
        {
            var livingTerrorists = new List<Terrorist>();
            foreach (var terrorist in terrorists)
                if (!terrorist.IsDead) livingTerrorists.Add(terrorist);
            return livingTerrorists.ToArray();
        }

        public static CounterTerrorist[] LivingCounterTerrorists()
        {
            var livingCounterTerrorists = new List<CounterTerrorist>();
            foreach (var counterterrorist in counterterrorists)
                if (!counterterrorist.IsDead) livingCounterTerrorists.Add(counterterrorist);
            return livingCounterTerrorists.ToArray();
        }

        public static int TerroristsRemaining()
        {
            return LivingTerrorists().Length;
        }

        public static int CounterTerroristsRemaining()
        {
            return LivingCounterTerrorists().Length;
        }

        public static bool TerroristsDead()
        {
            return TerroristsRemaining() == 0;
        }

        public static bool CounterTerroristsDead()
        {
            return CounterTerroristsRemaining() == 0;
        }

    }
}
