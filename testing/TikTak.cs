using System;
using System.Text.RegularExpressions;
namespace testing
{
	public class TikTak
	{
		short[,] board;
        static int gameNumber;
        Regex allowMove = new Regex("^[1-3]{2}$");

        static void clearBoard(short[,] board){
            for (int i=0; i<3; i++){
                for (int j=0; j<3; j++){
                    board[i,j] = 0;
                }
            }
        }

		public TikTak()
		{
			board = new short[3, 3];
            clearBoard(board);
            gameNumber = 0;
		}

        public void startGame()
        {
            gameNumber++;
            Console.WriteLine($@"Game #{gameNumber}");
            bool Player1 = true;
            int winner = 0;
            while (winner == 0)
            {
                takeTurn(Player1);
                winner = gameEnd();
                Player1 = !Player1;
            }
            if ( winner == 3)
            {
                Console.WriteLine("Hopefully next time you guys can figure out who is better");
            } else if (winner == 1)
            {
                Console.WriteLine("Congrats Player 1 you won");
            } else
            {
                Console.WriteLine("Congrats Player 2 you won");
            }

        }
        
        void takeTurn(bool Player1)
        {
            string name;
            if (Player1)
            {
                name = "Player1";
            }
            else
            {
                name = "Player2";
            }
            while (true)
            {
            Console.Write($"{name} make your turn\nEnter your move:");
            string move = Console.ReadLine();
            if (allowMove.IsMatch(move))
            {
                if (makeMove(Player1,move))
                {
                    break;
                }
            }
            }
            printScreen();
        }

        bool makeMove(bool Player1, string move)
        {
            int temp = int.Parse(move);
            (int, int) m = (temp / 10 - 1, temp % 10 - 1);
            if (board[m.Item1, m.Item2] != 0)
            {
                return false;
            }
            recordMove(Player1, m);
            return true;
        }

        void recordMove(bool Player1, (int,int) m)
        {
            short draw = 2;
            if (Player1)
            {
                draw = 1;
            }
            board[m.Item1, m.Item2] = draw;
        }

        short gameEnd()
        {
            // 0 - game going, 1 - player1 win,  2 - player2 win, 3 - draw
            (int,int) b = encodeBoard();
            Console.WriteLine(b);
            bool win1 = checkWin(b.Item1);
            bool win2 = checkWin(b.Item2);
            if ( (b.Item1 | b.Item2) == 0x1FF )
            {
                return 3;
            } else if (win1)
            {
                return 1;
            } else if (win2)
            {
                return 2;
            }
            return 0;
        }

        bool checkWin(int pos)
        {
            for (int i=0; i<3; i++)
            {
                if ( (pos & (7 << (i * 3)) ) == (7 << (i * 3)))
                {
                    return true;
                } else if ( (pos & (0b1_001_001 << i)) == (0b1_001_001 << i))
                {
                    return true;
                }
            }
            if ( (pos & 0b100_010_001) == 0b100_010_001)
            {
                return true;
            } else if ( (pos & 0b001_010_100) == 0b001_010_100)
            {
                return true;
            }
            return false;
        }

        (int,int) encodeBoard()
        {
            (int, int) ret = (0,0);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 1) ret.Item1 |= 1 << (i*3 + j);
                    else if (board[i, j] == 2) ret.Item2 |= 1 << (i * 3 + j);
                }
            }
            return ret;
        }

		void printScreen()
		{
            for(int i=0; i<3; i++){
                for(int j=0; j<3; j++){
                    char p = ' ';
                    if (board[i,j] == 1) p = '+';
                    else if (board[i,j] == 2) p = '-';
                    Console.Write(p);
                    if(j != 2)
                    {
                        Console.Write('|');
                    }
                        
                }
                Console.WriteLine();
                if (i != 2)
                {
                    Console.WriteLine("-----");
                }
            }
		}
    }

    class MainClass
    {
        static void Main(string[] args){
            TikTak game = new TikTak();
            while (true)
            {
                game.startGame();
                string repeat = "";
                while(repeat == "")
                {
                    repeat = Console.ReadLine();
                    if (repeat == "again" || repeat == "quit")
                    {
                        break;
                    }
                    repeat = "";
                }
                if (repeat == "quit" )
                {
                    Console.WriteLine("Thank for enjoying the game");
                    break;
                }
            }
        }
    }

}

