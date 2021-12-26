using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AOC21CSharp
{
    class RowColInfo
    {
        public int cnt;
        public int round;
    }

    class bingo_cell
    {
        public int ball;
        public int round;
    }

    class bingo_card
    {    
        public bingo_card()
        {
            for (int i = 0; i < row.Length; i++)
            {
                row[i] = new RowColInfo();
                col[i] = new RowColInfo();

                for (int j = 0; j < 5; j++)
                    board[i, j] = new bingo_cell();
            }                
        }

        public bingo_cell[,] board = new bingo_cell[5,5];
        public RowColInfo[] row = new RowColInfo[5];
        public RowColInfo[] col = new RowColInfo[5];
        public int round = int.MaxValue;
        public bool scored = false;
    }


    class Day4
    {
        private static char[] split = { ' ' };

        private static List<bingo_card> BuildBingoCard(int[] balls, StreamReader input)
        {
            int ball;
            List<bingo_card> cards = new List<bingo_card>();
            //Read in bingo cards
            while (!input.EndOfStream)
            {
                input.ReadLine();
                bingo_card card = new bingo_card();
                for (int rowidx = 0; rowidx < 5 && !input.EndOfStream; ++rowidx)
                {
                    string[] row = input.ReadLine().Split(split, StringSplitOptions.RemoveEmptyEntries);
                    for (int colidx = 0; colidx < 5; ++colidx)
                    {
                        if (Int32.TryParse(row[colidx], out ball))
                        {
                            int ballround = balls[ball];

                            card.board[rowidx, colidx].ball = ball;
                            card.board[rowidx, colidx].round = ballround;

                            //Ensure ball # was called
                            if (ballround > 0)
                            {
                                card.row[rowidx].cnt++;
                                card.col[colidx].cnt++;
                                if (card.row[rowidx].round < ballround)
                                    card.row[rowidx].round = ballround;
                                if (card.col[colidx].round < ballround)
                                    card.col[colidx].round = ballround;
                            }
                        }
                    }

                    if (card.row[rowidx].cnt == 5 && card.round > card.row[rowidx].round)
                        card.round = card.row[rowidx].round;
                }

                //Find Round Column Card Scored...(i.e. lowest rd)
                for (int i = 0; i < 5; i++)
                {
                    if (card.col[i].cnt == 5 && card.round > card.col[i].round)
                        card.round = card.col[i].round;
                }

                //Mark Card as Scored
                card.scored = card.round != int.MaxValue;

                cards.Add(card);
            }

            return cards;
        }

        /// <summary>
        /// alculate score (all non-called balls x round won).
        /// </summary>
        /// <param name="card"></param>
        /// <param name="round"></param>
        /// <returns></returns>
        private static int ScoreCard(bingo_card card, int round)
        {
            //Calculate score (all non-called balls x round won)
            int sum = 0;
            for (int rowidx = 0; rowidx < 5; rowidx++)
                for (int colidx = 0; colidx < 5; colidx++)
                {
                    if (card.board[rowidx, colidx].round > round)
                        sum += card.board[rowidx, colidx].ball;
                }

            return sum;
        }


    /// <summary>
    /// Solution looking to simply not loop through every card with each ball pull. 
    /// See alt solution for loop solution.
    /// </summary>
    /// <returns></returns>
    public static int Part1()
        {
            StreamReader input = new System.IO.StreamReader(@"..\..\..\input\Day4_Input.txt");

            //Read in Bingo Pulls
            string[] sballs = input.ReadLine().Split(',');

            int maxball = 0; int ball;
            List<int> balls_pulled = new List<int>(100);
            foreach (string s in sballs)
            {
                if (Int32.TryParse(s, out ball))
                {
                    balls_pulled.Add(ball);
                    if (ball > maxball) maxball = ball;
                }
            }

            //Store round at ball number
            int[] balls = new int[maxball + 1];
            for (int i = 0; i < balls_pulled.Count(); ++i)
                balls[balls_pulled[i]] = i+1;

            List<bingo_card> cards = BuildBingoCard(balls, input);


            //find card with lowest round
            int winnerIdx = int.MaxValue;
            int winnerRd = int.MaxValue;
            for(int cardidx = 0; cardidx < cards.Count(); cardidx++)
            {
                if (winnerRd > cards[cardidx].round)
                {
                    winnerRd = cards[cardidx].round;
                    winnerIdx = cardidx;
                }
            }

            //Calculate Score (non-selected balls * winning ball)
            return ScoreCard(cards[winnerIdx], winnerRd) * balls_pulled[winnerRd - 1];
        }

        public static int Part2()
        {
            StreamReader input = new System.IO.StreamReader(@"..\..\..\input\Day4_Input.txt");

            //Read in Bingo Pulls
            string[] sballs = input.ReadLine().Split(',');

            int maxball = 0; int ball;
            List<int> balls_pulled = new List<int>(100);
            foreach (string s in sballs)
            {
                if (Int32.TryParse(s, out ball))
                {
                    balls_pulled.Add(ball);
                    if (ball > maxball) maxball = ball;
                }
            }

            //Store round at ball number
            int[] balls = new int[maxball + 1];
            for (int i = 0; i < balls_pulled.Count(); ++i)
                balls[balls_pulled[i]] = i + 1;

            //Read in cards, Set Round Card scores
            List<bingo_card> cards = BuildBingoCard(balls, input);

            //find card with highest round
            int winnerIdx = int.MaxValue;
            int winnerRd = 0;
            for (int cardidx = 0; cardidx < cards.Count(); cardidx++)
            {
                if (winnerRd < cards[cardidx].round)
                {
                    winnerRd = cards[cardidx].round;
                    winnerIdx = cardidx;
                }
            }

            //Calculate Score (non-selected balls * winning ball)
            return ScoreCard(cards[winnerIdx], winnerRd) * balls_pulled[winnerRd - 1];
        }

        public static int Part1Alt_easy()
        {
            StreamReader input = new System.IO.StreamReader(@"..\..\..\input\Day4_Input.txt");

            //Read in Bingo Pulls
            string[] sballs = input.ReadLine().Split(',');

            int iball;
            List<int> temp_balls = new List<int>(100);
            foreach (string s in sballs)
            {
                if (Int32.TryParse(s, out iball))
                {
                    temp_balls.Add(iball);
                }
            }

            List<bingo_card> cards = new List<bingo_card>();
            char[] split = { ' ' };
            //Read in bingo cards
            while (!input.EndOfStream)
            {
                //Skip empty line
                input.ReadLine();
                bingo_card card = new bingo_card();
                for (int rowidx = 0; rowidx < 5; ++rowidx)
                {
                    string[] row = input.ReadLine().Split(split, StringSplitOptions.RemoveEmptyEntries);
                    for (int colidx = 0; colidx < 5; ++colidx)
                    {
                        if (Int32.TryParse(row[colidx], out iball))
                        {
                            card.board[rowidx, colidx].ball = iball;
                        }
                    }
                }

                cards.Add(card);
            }

            bool winner = false;
            bool found = false;
            foreach (int ball in temp_balls)
            {
                foreach (bingo_card card in cards)
                {
                    found = false;
                    for (int rowidx = 0; rowidx < 5; ++rowidx)
                    {
                        for (int colidx = 0; colidx < 5; ++colidx)
                            if (card.board[rowidx, colidx].ball == ball)
                            {
                                card.board[rowidx, colidx].ball = 0;
                                card.row[rowidx].cnt++;
                                card.col[colidx].cnt++;

                                //Check if column scored
                                if (card.col[colidx].cnt == 5 || card.row[rowidx].cnt == 5)
                                    winner = true;
                                found = true;
                                break;
                            }
                        if (found)
                            break;
                    }

                    if (winner)
                    {
                        int sum = 0;
                        for (int rowidx = 0; rowidx < 5; ++rowidx)
                            for (int colidx = 0; colidx < 5; ++colidx)
                                sum += card.board[rowidx, colidx].ball;
                        return sum * ball;
                    }


                }
            }

            return 0;
        }

        public static int Part2Alt_easy()
        {
            StreamReader input = new System.IO.StreamReader(@"..\..\..\input\Day4_Input.txt");

            //Read in Bingo Pulls
            string[] sballs = input.ReadLine().Split(',');

            int iball;
            List<int> temp_balls = new List<int>(100);
            foreach (string s in sballs)
            {
                if (Int32.TryParse(s, out iball))
                {
                    temp_balls.Add(iball);
                }
            }

            List<bingo_card> cards = new List<bingo_card>();
            //Read in bingo cards
            while (!input.EndOfStream)
            {
                //Skip empty line
                input.ReadLine();
                bingo_card card = new bingo_card();
                for (int rowidx = 0; rowidx < 5; ++rowidx)
                {
                    string[] row = input.ReadLine().Split(split, StringSplitOptions.RemoveEmptyEntries);
                    for (int colidx = 0; colidx < 5; ++colidx)
                    {
                        if (Int32.TryParse(row[colidx], out iball))
                        {
                            card.board[rowidx, colidx].ball = iball;
                        }
                    }
                }

                cards.Add(card);
            }

            int winner = 0;
            int winning_ball = 0;
            bool found = false;
            foreach (int ball in temp_balls)
            {
                int cardnum = 0;
                foreach (bingo_card card in cards)
                {
                    if (card.scored)
                    {
                        cardnum++;
                        continue;
                    }

                    found = false;
                    for (int rowidx = 0; rowidx < 5; ++rowidx)
                    {
                        for (int colidx = 0; colidx < 5; ++colidx)
                        {
                            if (card.board[rowidx, colidx].ball == ball)
                            {
                                card.board[rowidx, colidx].ball = -1;
                                card.row[rowidx].cnt++;
                                card.col[colidx].cnt++;

                                //Check if column scored
                                if (card.col[colidx].cnt == 5 || card.row[rowidx].cnt == 5)
                                {
                                    card.scored = true;
                                    winner = cardnum;
                                    winning_ball = ball;
                                }
                                found = true;
                                break;
                            }
                        }

                        if (found)
                            break;
                    }

                    cardnum++;
                }
            }

            bingo_card winningcard = cards[winner];
            int sum = 0;
            for (int rowidx = 0; rowidx < 5; ++rowidx)
                for (int colidx = 0; colidx < 5; ++colidx)
                    if (winningcard.board[rowidx, colidx].ball > 0)
                        sum += winningcard.board[rowidx, colidx].ball;
            return sum * winning_ball;
        }
    }
}
