using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TourneySoft
{
    public partial class EditPlayersControl : UserControl
    {
        public EditPlayersControl()
        {
            InitializeComponent();
        }
        

        private void EditPlayersControl_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill; //Fill out entire parent
            if(Global.currentTournament.isActive == true)
            {
                addPlayerBtn.Enabled = false; //Cant add players if tournament running
            }
            playerContainer.Columns.Add("playerName", "Name");
            playerContainer.Columns.Add("playerWins", "Wins");
            playerContainer.Columns.Add("playerLosses", "Losses");
            populatePlayerContainer(); //Update list of players
        }

        /// <summary>
        /// Populate te player view in control
        /// </summary>
        private void populatePlayerContainer()
        {
            playerContainer.Rows.Clear(); //Clear old info
            foreach (var player in Global.currentTournament.players)
            {
                playerContainer.Rows.Add(player.name, player.wins, player.losses); //Add new info
            } 
        }


        /// <summary>
        /// Add the player if possible
        /// </summary>
        private void addPlayerBtn_Click(object sender, EventArgs e)
        {
            if(newPlayerName.Text.Length > 0) //There is info in the playername box
            {
                if(Global.currentTournament.players.Any(x => x.name == newPlayerName.Text)) //If player already in list
                {
                    MessageBox.Show("Player names must be unique!"); //alert the user that they already have a player with that name.
                }
                else
                {
                    Global.currentTournament.players.Add(new Player(newPlayerName.Text)); //Add player to the list
                }
            }
            populatePlayerContainer(); //Repop view
            newPlayerName.Clear(); //Clear textbox
        }

        /// <summary>
        /// Enable player addition on keystroke {enter}
        /// </summary>
        private void newPlayerName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter && addPlayerBtn.Enabled) //Press add player btn on enter keystroke
            {
                e.Handled = true; //Key is handled
                e.SuppressKeyPress = true; //Don't ding
                addPlayerBtn_Click(sender, e); //Passthrough to btn_click
            }
        }

        /// <summary>
        /// Remove a player by name
        /// </summary>
        /// <param name="n">Name of player to remove</param>
        private void removePlayerByName(string n)
        {
            int removeIndex = -1; //Invalid index
            for (int i = 0; i < Global.currentTournament.players.Count; i++) //Loop through players in tourney
            {
                if(Global.currentTournament.players[i].name == n) //Player found at i
                {
                    removeIndex = i; //store i
                }
            }
            if(!(removeIndex == -1)) //I found
            {
                Global.currentTournament.players.RemoveAt(removeIndex); //Remove I
            }
        }

        /// <summary>
        /// confirm dropping of player before execution
        /// </summary>
        private void dropPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Int32 rowIndex = playerContainer.Rows.GetFirstRow(DataGridViewElementStates.Selected); //Selected Row
            string playerName = playerContainer.Rows[rowIndex].Cells[0].Value.ToString(); //Player Name in row
            string dropMsg = string.Format("Are you sure you want to drop {0}?", playerName); //Confirmation String
            DialogResult d = MessageBox.Show(dropMsg,"Drop Player?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(d == DialogResult.Yes) // Confirmation aquired
            {
                playerContainer.ClearSelection(); //Clear selection from dataview
                removePlayerByName(playerName); //Drop player from tournament
                populatePlayerContainer(); //repop data
            }
        }

        /// <summary>
        /// Display context menu to drop players
        /// </summary>
        private void playerContainer_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right) //Right click on row
            {
                var hti = playerContainer.HitTest(e.X, e.Y); //get mouse hit
                if(hti.Type == DataGridViewHitTestType.Cell) //Mouse hit a cell
                {
                    playerViewContext.Show(MousePosition); //Display context menu
                    playerContainer.ClearSelection(); //Clear old selection
                    playerContainer.Rows[hti.RowIndex].Selected = true; //Select row under mouse.
                }
            }
        }
    }
}
