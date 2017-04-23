using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIv2 : MonoBehaviour, connect4AI

{
    private BoardEval be;
    private int playerNumber;
    private int depth = 6;
    private System.Random r = new System.Random();
    // Use this for initialization


    private int myTeam;
    void Start()
    {
        be = new BoardEval();

    }

    public int Place(int[,] board)
    {
        return getMove(board);
    }

    public void SetTeam(int teamNumber)
    {
        playerNumber = teamNumber;
    }

    public int getMove(int[,] board)
    {


        
        int moves = be.moveCount(board);
        if (board.GetLength(0) == 6)
        {
            if (moves > 8) depth = 7;
            if (moves > 11) depth = 8;
            if (moves > 17) depth = 9;
            if (moves > 20) depth = 11;
            if (moves > 25) depth = 13;
        }
        if (board.GetLength(0) == 7)
        {
            if (moves > 10) depth = 7;
            if (moves > 16) depth = 8;
            if (moves > 21) depth = 9;
            if (moves > 22) depth = 12;
        }
        if (moves < 5) return reducedMove(board);



        int[,] copy = be.copyOfBoard(board);
        int[] temp = ab(copy, 6, -250, 250, true, 0);


        //print("Alpha beta returns" + temp[0] + " with a value of: " + temp[1]);
        if (temp[0] == 0 && temp[1] == 0)
        {
            int semiRand = r.Next(4) + 1;
            int[,] copy2 = be.copyOfBoard(board);
            if (!be.moveIsIllegal(copy2, semiRand))
            {
                be.putMove(copy2, playerNumber == 1, semiRand);
                if (ab(copy2, depth, -250, 250, false, 0)[1] == 0)
                {
                    //print("Height of board" + board.GetLength(1));
                    //print(board[semiRand, board.GetLength(1) - 1]);
                    return semiRand;
                }
            }

        }
        return temp[0];
    }



    public int[] ab(int[,] board, int depth, int alpha, int beta, bool max, int moves)
    {
        int[] result = new int[2];
        result[0] = -1;

        int boardStatus = be.checkGameOver(board);
       
        if (boardStatus == playerNumber)
        {
            // someone wins -> rtn 1 or -1 * turns
            if (moves < 2)
            {
                be.printBoard(board);
            }
            result[1] = 22 - moves;
            return result;
        }
        else if (boardStatus != 0)
        {
            if (moves < 2) be.printBoard(board);
            result[1] = moves - 22;
            return result;
        }
        if (be.exhaustedOptions(board)) return result;
        if (depth == 0)
        {
            result[1] = 0;
            //System.out.println("Board at depth 0:");
            //be.printBoard(board);
            return result;
        } // wtd here?


        if (max)
        {

            int[] resultMax = new int[2];
            resultMax[0] = -1;
            resultMax[1] = -999;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                int[,] copy = be.copyOfBoard(board);
                if (be.moveIsIllegal(board, i)) continue; // Not sure here;
                copy = be.putMove(copy, playerNumber == 1, i);

                int[] next = ab(copy, depth - 1, alpha, beta, false, (moves + 1));
                if (depth == 9)
                {
                    print("Max Evaluated: " + next[0] + " as " + next[1]);
                }
                if (resultMax[0] == -1 || next[1] > resultMax[1])
                {

                    resultMax[0] = i;
                    resultMax[1] = next[1];
                    alpha = next[1];
                }

                //alpha = Math.Max(alpha, ab(copy, depth - 1, alpha, beta, false, moves + 1)[1]);
                if (alpha >= beta) break;
            }
            return resultMax;
        }

        else
        {

            int[] resultMin = new int[2];
            resultMin[0] = -1;
            resultMin[1] = 999;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                int[,] copy = be.copyOfBoard(board);
                if (be.moveIsIllegal(board, i)) continue; // Not sure here;

                copy = be.putMove(copy, playerNumber != 1, i);

                int[] next = ab(copy, depth - 1, alpha, beta, true, (moves));
                if (depth == 10)
                {
                    print("Min Evaluated: " + next[0] + " as " + next[1]);
                }
                if (resultMin[0] == -1 || next[1] < resultMin[1])
                {
                    resultMin[0] = i;
                    resultMin[1] = next[1];
                    alpha = next[1];
                }
                if (alpha >= beta) break;
            }
            return resultMin;
        }

        //return 0;
    }

    int reducedMove(int[,] board)
    {
        print("Doing a reduced move to save computations:");
        int known = knownBoards(board);
        if (known != -1) return known;
        bool p1 = be.isP1(board);
        int i = board.GetLength(0);

        if (i % 2 == 0)
        {
            int openended = be.openended(board);
            if (openended != -1) return openended;
            print("Returning one of two middle values");
            if (!be.moveIsIllegal(board, i / 2) && !be.moveIsIllegal(board, i / 2 - 1))
                return i / 2 - (int)(r.Next(1));
            if (!be.moveIsIllegal(board, i / 2)) return i / 2;
            if (!be.moveIsIllegal(board, i / 2 - 1)) return i / 2 - 1;

        }
        else
        {
            if (!be.moveIsIllegal(board, i / 2)) return i / 2;
        }
        print("returning a completely random move");

        //return r.Next(7);
        int rand = (r.Next(board.GetLength(0)));
        while (be.moveIsIllegal(board, rand)) rand = (r.Next(board.GetLength(0)));
        return rand;
    }


    public int knownBoards(int[,] b)
    {
        int x = b.GetLength(0);
        int y = b.GetLength(1);
        int[,] b2 = new int[x, y];


        if (b.GetLength(0) == 6)
        {

        }




        if (b.GetLength(0) == 7)

        // p1 boards

        {
            if (b[0, 0] == 0 && b[1, 0] == 0 && b[2, 0] == 0 && b[3, b.GetLength(1) - 1] == 0 && b[4, 0] == 0 && b[5, 0] == 0 && b[6, 0] == 0)
            {
                return 3;
            }

            // p1 boards
            // one stone played
            b2 = new int[x, y];
            b2[2, 0] = 1;
            if (b.Equals(b2)) return 2;
            b2 = new int[x, y];
            b2[3, 0] = 1;
            if (b.Equals(b2)) return 3;
            b2 = new int[x, y];
            b2[4, 0] = 1;
            if (b.Equals(b2)) return 4;
            b2 = new int[x, y];

            // p2 boards
            // General:

            b2 = new int[x, y];


            // one stone played


            // two stones played

        }

        return -1;
    }


}




class BoardEval2
{

    int x;
    int y;

    public int checkGameOver(int[,] board)
    {
        x = board.GetLength(0);
        y = board.GetLength(1);
        int winner = 0;
        for (int x = 0; x < this.x; x++)
        {
            for (int y = 0; y < this.y; y++)
            {

                if (
                        (winner = checkUp(board, x, y)) != 0
                        || (winner = checkSide(board, x, y)) != 0
                        || (winner = checkDiagonalUp(board, x, y)) != 0
                        || (winner = checkDiagonalDown(board, x, y)) != 0)
                    return winner;
            }
        }

        return winner;
    }

    private int checkUp(int[,] board, int x, int y)
    {
        // Check end to end first?
        if (y + 3 < this.y
                && board[x, y] != 0
                && board[x, y] == board[x, y + 1]
                && board[x, y + 1] == board[x, y + 2]
                && board[x, y + 2] == board[x, y + 3])
            return board[x, y];
        return 0;
    }

    private int checkSide(int[,] board, int x, int y)
    {
        if (x + 3 < this.x
                && board[x, y] != 0
                && board[x, y] == board[x + 1, y]
                && board[x + 1, y] == board[x + 2, y]
                && board[x + 2, y] == board[x + 3, y])
            return board[x, y];
        return 0;
    }

    private int checkDiagonalUp(int[,] board, int x, int y)
    {

        if (
                x + 3 < this.x
                && y + 3 < this.y
                && board[x, y] != 0
                && board[x, y] == board[x + 1, y + 1]
                && board[x + 1, y + 1] == board[x + 2, y + 2]
                && board[x + 2, y + 2] == board[x + 3, y + 3])
            return board[x, y];
        return 0;

    }

    private int checkDiagonalDown(int[,] board, int x, int y)
    {
        if (
                x + 3 < this.x
                && y - 3 >= 0
                && board[x, y] != 0
                && board[x, y] == board[x + 1, y - 1]
                && board[x + 1, y - 1] == board[x + 2, y - 2]
                && board[x + 2, y - 2] == board[x + 3, y - 3])
            return board[x, y];
        return 0;
    }

    public void printBoard(int[,] board)
    {
        for (int j = board.GetLength(1) - 1; j >= 0; j--)
        {
            System.String s = "";
            for (int i = 0; i < board.GetLength(0); i++)
            {
                s += board[i, j] + " ";

            }
            Debug.Log(s);
            //Debug.Log("");
        }
        Debug.Log("");
        Debug.Log("");
    }

    public bool moveIsIllegal(int[,] board, int column)
    {
        int top = board.GetLength(1) - 1;
        bool illegal = (board[column, top]) != 0;
        return illegal;
    }
    public int[,] copyOfBoard(int[,] board)
    {

        int[,] copy = board.Clone() as int[,];


        return copy;
    }
    public bool exhaustedOptions(int[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            if (!moveIsIllegal(board, i)) return false;
        }
        return true;
    }
    public int[,] putMove(int[,] board, bool p1, int col)
    {
        for (int i = 0; i < board.GetLength(1); i++)
        {
            if (board[col, i] == 0)
            {
                if (p1)
                {
                    board[col, i] = 1;
                }
                else
                {
                    board[col, i] = 2;
                }
                break;
            }
        }
        return board;
        //return checkGameOver(board);
    }

    public int moveCount(int[,] board)
    {
        int count = 0;
        for (int j = board.GetLength(1) - 1; j >= 0; j--)
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                if (board[i, j] != 0) count++;
            }

        }
        return count;
    }
    public bool isP1(int[,] board)
    {
        // if p1 has made more moves than p2, we are p2;
        int p1MovesCount = 0;
        int p2MovesCount = 0;

        for (int x = 0; x < this.x; x++)
        {
            for (int y = 0; y < this.y; y++)
            {

                if (board[x, y] == 1) p1MovesCount++;
                else if (board[x, y] == 2) p2MovesCount++;
            }
        }
        return !(p1MovesCount > p2MovesCount);
    }

    public int winningMoveExists(int[,] board)
    {
        int answer = -1;
        for (int i = 0; i < board.GetLength(0); i++)
        {
            if (this.moveIsIllegal(board, i)) continue;
            int[,] copy = this.copyOfBoard(board);
            putMove(copy, true, i);
            if (this.checkGameOver(copy) == 1) return i;
            copy = this.copyOfBoard(board);
            putMove(copy, false, i);
            if (this.checkGameOver(copy) == 2) return i;
        }
        return answer;
    }
    public int openended(int[,] board)
    {

        for (int i = 1; i < board.GetLength(0) - 3; i++)
        {

            if (board[i - 1, 0] == 0 && board[i, 0] == board[i + 1, 0] && board[i, 0] != 0 && board[i + 2, 0] == 0)
            {
                return i - 1;
            }
        }
        return -1;
    }




}
