using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KolkoiKrzyzyk
{
    public partial class Form1 : Form
    {

        bool turn = true; // Tura X (Playera 1) jeśli true, Y (Playera 2) jeśli false
        int turn_count = 0;
        static string player1, player2;
        bool against_computer = false;

        public Form1()
        {
            InitializeComponent();
        } // Inicjacja

        public static void setPlayerNames(String n1, String n2) // UpDate imion
        {
            player1 = n1;
            player2 = n2;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Gra stworzona przez Patryka Morawskiego", "About");
        } // About

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        } // Exit

        private void Button_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (turn)
                b.Text = "X";
            else
                b.Text = "O";

            turn = !turn;
            b.Enabled = false;
            turn_count++;

            checkforWinner();
            if ((!turn) && (against_computer))
            {
                computer_make_move();
            }
        } // Co się dzieje po kliknięciu

        private void computer_make_move()
        {
            // priority 1: get tick tac toe
            // priority 2: block x tic tac toe
            // priority 3: go for center 
            // priority 4: go for corner space
            // priority 5: pick open space

            Button move = null;

            // priority 1 

            move = look_for_win_or_block("O"); // look for win
            if (move == null)
            {
                move = look_for_win_or_block("X"); // look for block
                if (move == null)
                {
                    move = look_for_C1_A3("X");
                    if (move == null)
                    {
                        move = look_for_C2_A1_or_A3("X"); // naprawa błędu algorytmu
                        if (move == null)
                        {
                            move = look_for_center();
                            if (move == null)
                             {
                                  move = look_for_corner();
                                  if (move == null)
                                 {
                                   move = look_for_open_space();
                                 }
                        }
                    }
                        }
                        
                }
            }
            try
            {

                move.PerformClick();
        
            }
            catch { }
      

        }  // W jakiej kolejności komputer wykonuje ruch

        private Button look_for_C1_A3(string mark)
        {
            if (C1.Text == mark && A3.Text == mark)
                return B1;
            else
                return null;
        }

        private Button look_for_C2_A1_or_A3(string mark)
        {
            if (C2.Text == mark && A3.Text == mark)
                return B3;
            else
                return null;
        }

        private Button look_for_center()
        {
            if (B2.Text == "")
                return B2;
            else
                return null;
        } // Priorytet Centrum

        private Button look_for_open_space()
        {

            Console.WriteLine("Looking for open space");
            Button b =null;
            foreach ( Control c in Controls) // Każdy komponent 
            {
                b = c as Button;
                if  (b != null)
                {
                    if (b.Text == "")
                        return b;
                }
            }

            return null;
        }  // Szukanie Wolnej Przestrzeni

        private Button look_for_corner()
        {
            Console.WriteLine("Looking for corner");
            if (A1.Text == "O")
            {
                if (A1.Text == "O")
                {
                    if (A3.Text == "")
                        return A3;
                    if (C3.Text == "")
                        return C3;
                    if (C1.Text == "")
                        return C1;
                }

                if (A3.Text == "O")
                {
                    if (A1.Text == "")
                        return A1;
                    if (C3.Text == "")
                        return C3;
                    if (C1.Text == "")
                        return C1;
                }

                if (C3.Text == "O")
                {
                    if (A1.Text == "")
                        return A3;
                    if (A3.Text == "")
                        return A3;
                    if (C1.Text == "")
                        return C1;
                }

                if (C1.Text == "O")
                {
                    if (A1.Text == "")
                        return A1;
                    if (A3.Text == "")
                        return A3;
                    if (C3.Text == "")
                        return C3;
                }

                if (A1.Text == "")
                    return A1;
                if (A3.Text == "")
                    return A3;
                if (C1.Text == "")
                    return C1;
                if (C3.Text == "")
                    return C3;
            }
            return null;
        } // Priorytet Róg

        private Button look_for_win_or_block(string mark)
        {
            Console.WriteLine("Looking for win or block: " + mark);

            // HORIZONTAL TESTS
            if ((A1.Text == mark) && (A2.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A2.Text == mark) && (A3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (A3.Text == mark) && (A2.Text == ""))
                return A2;

            if ((B1.Text == mark) && (B2.Text == mark) && (B3.Text == ""))
                return B3;
            if ((B2.Text == mark) && (B3.Text == mark) && (B1.Text == ""))
                return B1;
            if ((B1.Text == mark) && (B3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((C1.Text == mark) && (C2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((C2.Text == mark) && (C3.Text == mark) && (C1.Text == ""))
                return C1;
            if ((C1.Text == mark) && (C3.Text == mark) && (C2.Text == ""))
                return C2;

            //VERTICAL TESTS
            if ((A1.Text == mark) && (B1.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B1.Text == mark) && (C1.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C1.Text == mark) && (B1.Text == ""))
                return B1;

            if ((A2.Text == mark) && (B2.Text == mark) && (C2.Text == ""))
                return C2;
            if ((B2.Text == mark) && (C2.Text == mark) && (A2.Text == ""))
                return A2;
            if ((A2.Text == mark) && (C2.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B3.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B3.Text == mark) && (C3.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C3.Text == mark) && (B3.Text == ""))
                return B3;

            //DIAGONAL TESTS
            if ((A1.Text == mark) && (B2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B2.Text == mark) && (C3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B2.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B2.Text == mark) && (C1.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C1.Text == mark) && (B2.Text == ""))
                return B2;

            return null;
        } // Szukanie takich samych w rzędzie, koluminie i na ukos

        private void checkforWinner()
        {
            bool there_is_a_winner = false;
            // Pionowe
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (A1.Text == A3.Text) && (!A1.Enabled && !A2.Enabled && !A3.Enabled))
                there_is_a_winner = true;
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (B1.Text == B3.Text) && (!B1.Enabled && !B2.Enabled && !B3.Enabled))
                there_is_a_winner = true;
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (C1.Text == C3.Text) && (!C1.Enabled && !C2.Enabled && !C3.Enabled))
                there_is_a_winner = true;
            // Poziome
            else if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (A1.Text == C1.Text) && (!A1.Enabled && !B1.Enabled && !C1.Enabled))
                there_is_a_winner = true;
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (A2.Text == C2.Text) && (!A2.Enabled && !B2.Enabled && !C2.Enabled))
                there_is_a_winner = true;
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (A3.Text == B3.Text) && (!A3.Enabled && !B3.Enabled && !C3.Enabled))
                there_is_a_winner = true;
            // Ukos
            else if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (A1.Text == C3.Text) && (!A1.Enabled && !C3.Enabled && !B2.Enabled))
                there_is_a_winner = true;
            else if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && (A3.Text == C1.Text) && (!C1.Enabled && !A3.Enabled && !B2.Enabled))
                there_is_a_winner = true;
   


            if (there_is_a_winner)
            {
                disableButtons();
                String winner = "";
                if (turn)
                {
                    winner = player2;
                    o_win_count.Text = (Int32.Parse(o_win_count.Text) + 1).ToString();
                }
                else
                {
                    winner = player1;
                    x_win_count.Text = (Int32.Parse(x_win_count.Text) + 1).ToString();
                }
                

                MessageBox.Show(winner + " Wins!", "Yay!");
                if (MessageBox.Show(" Zacząć nową gre ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    turn = true;
                    turn_count = 0;

                    foreach (Control c in Controls)
                    {
                        try
                        {
                            Button b = (Button)c;
                            b.Enabled = true;
                            b.Text = "";
                        }
                        catch { }
                    } 
                }
                else {

                    foreach (Control c in Controls)
                    {
                        try
                        {
                            Button b = (Button)c;
                            b.Enabled = false;
                            b.Text = "";
                        }
                        catch { }
                    }
                }

            }
            else
            {
                if (turn_count == 9)
                {
                    MessageBox.Show("Draw", ":( :(");
                    draw_count.Text = (Int32.Parse(draw_count.Text) + 1).ToString();
                }
            }
        } // Sprawdza Kto wygrał

        private void disableButtons() // Wyłącza pozostałe przyciski
        {
            try
            {
                foreach (Control c in Controls)
                {
                    Button b = (Button)c;
                    b.Enabled = false;
                }
            }
            catch { }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn = true;
            turn_count = 0;
            
                foreach (Control c in Controls)
                {
                    try
                    {
                        Button b = (Button)c;
                        b.Enabled = true;
                        b.Text = "";
                    }
                    catch { }
                }       
        } // Nowa Gra

        private void Button_Enter(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                if (turn)
                    b.Text = "X";
                else b.Text = "O";
            }
        }  // Pojawienie się podglądu X i O

        private void Button_Leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                b.Text = "";
            }
        }  // Zniknięcie podglądu

        private void resetScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            o_win_count.Text = "0";
            x_win_count.Text = "0";
            draw_count.Text = "0";
        } // Reset Wyników

        private void Form1_Load(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            label1.Text = player1;
            label3.Text = player2;
        } // Ładowanie Form2 jako pierwsze

        private void againsCompToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (against_computer == false)
              {
                 against_computer = true;
                 label3.Text = "Computer";
               }
           else
             { 
                  against_computer = false;
            
                  Form2 f2 = new Form2();
                  f2.ShowDialog();
                  label1.Text = player1;
                  label3.Text = player2;
             }
        }  // Przeciw komputerowi ON/OFF
    }
}
