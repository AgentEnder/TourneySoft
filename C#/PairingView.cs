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
    public partial class PairingView : UserControl
    {
        public PairingView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Executes on control loading, initailizes the view to empty state.
        /// </summary>
        private void PairingView_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill; //Fill parent control completely
            pairingsView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(17,161,1);
            pairingsView.DefaultCellStyle.SelectionForeColor = pairingsView.DefaultCellStyle.ForeColor;
            for (int i = 0; i < Global.currentTournament.numPlayersInPairs; i++)
            {
                pairingsView.Columns.Add(string.Format("player{0}", i + 1), string.Format("Player {0}", i + 1));
            }
            if(Global.currentTournament.prevRoundNumber != Global.currentTournament.roundNumber)
            {
                Global.currentTournament.PairPlayers();
            }
            populatePairingsView();
        }

        
        /// <summary>
        ///     Select Winners by left clicking on the appropriate cell
        /// </summary>
        private void pairingsView_MouseClick(object sender, MouseEventArgs e)
        {
            var hti = pairingsView.HitTest(e.X, e.Y);
            if (hti.Type == DataGridViewHitTestType.Cell)
            {
                int rowIndex = hti.RowIndex;
                int columnIndex = hti.ColumnIndex;

                if(e.Button == MouseButtons.Left)
                {
                    for(int i = 0; i < pairingsView.Rows[rowIndex].Cells.Count; i++) //Foreach cell in the selected row
                    {
                        if(i == columnIndex)
                        {
                            pairingsView.Rows[rowIndex].Cells[i].Style.BackColor = Color.FromArgb(17,161,1); //Visual Confirmation of selection
                            var pairing = Global.currentTournament.pairings[rowIndex]; //Pair clicked on
                            pairing.winner = Global.currentTournament.players.Find(x => x.name == (string)pairingsView.Rows[rowIndex].Cells[i].Value); //Assign winner
                            Global.currentTournament.pairings[rowIndex] = pairing; //Assign updated pairing
                        }else
                        {
                            pairingsView.Rows[rowIndex].Cells[i].Style.BackColor = pairingsView.BackgroundColor; //Visual confirmation that the row has been selected
                        }
                    }

                }

            }
        }


        /// <summary>
        ///     Check if any of the pairs do not have a winner, 
        ///     assign wins, and then generate new pairings
        /// </summary>
        private void nextRoundBtn_Click(object sender, EventArgs e)
        {
            bool AllSelected = true; //Default to is not
            foreach (var pair in Global.currentTournament.pairings)
            {
                bool pairHasWinner = false;
                foreach (var player in pair.pairedPlayers)
                {
                    if(pair.winner == player)
                    {
                        pairHasWinner = true;
                        break;
                    }
                }
                if (!pairHasWinner)
                {
                    AllSelected = false;
                    break;
                }
            }
            if (AllSelected == true)//All winners selected
            {
                foreach (var pair in Global.currentTournament.pairings) //Loop through each pair
                {
                    int winnerIndex = Global.currentTournament.players.FindIndex(x => x.name == pair.winner.name);
                    foreach (var player in pair.pairedPlayers.FindAll(x=> x.name != pair.winner.name))
                    {
                        int indexOfLoser = Global.currentTournament.players.FindIndex(x => x.name == player.name);
                        if (indexOfLoser > -1)
                        {
                            Global.currentTournament.players[indexOfLoser].losses += 1;
                        }
                    }
                    Global.currentTournament.players[winnerIndex].wins+=1; //Assign Win
                }
                Global.currentTournament.roundNumber += 1; //Increment round number
                Global.currentTournament.AssignPoints(); //Assign player points
                Global.currentTournament.PairPlayers(); //Generate new pairs
                if(Global.currentTournament.prevRoundNumber >= Global.currentTournament.numRounds){ EndBtn_Click(new object(), EventArgs.Empty); }
                populatePairingsView(); //Refresh pairing view
                Global.currentTournament.prevRoundNumber += 1;
            }else
            {
                MessageBox.Show("You must choose winners for each pairing first!"); //alert the user that they still need to select winners
            }
        }

        /// <summary>
        /// Clear the pairings view and repopulate it, used to update and initialize the data
        /// </summary>
        private void populatePairingsView()
        {
            pairingsView.Rows.Clear(); //Clear the view
            for (int i = 0; i < Global.currentTournament.pairings.Count; i++) //Loop through pairings
            {
                Tournament.PlayerPairing pair = Global.currentTournament.pairings[i]; //Reference to current pair
                DataGridViewRow row = new DataGridViewRow(); //Row to hold data
                row.HeaderCell.Value = i.ToString(); //Number the rows
                List<string> names = new List<string>();
                foreach (var player in pair.pairedPlayers)
                {
                    names.Add(player.name);
                }
                row.CreateCells(pairingsView, names.ToArray());//Add cells to the row 
                pairingsView.Rows.Add(row); //Add row to the table
            }
            if (pairingsView.Rows.Count > 0) { //Rows exist
                pairingsView.Rows[0].Cells[0].Selected = false; //No default selection
            }
        }

        /// <summary>
        /// Button to end tournament early
        /// </summary>
        private void EndBtn_Click(object sender, EventArgs e)
        {
            Global.currentTournament.isOver = true;
            Global.currentTournament.isActive = false;
            Global.currentTournament.RankPlayers();
            if (this.Parent == null || this.Parent.GetType() != typeof(Panel))
            {
                return; //Future proofing
            }
            this.Parent.Controls.Add(new tournamentResults());//Add editplayer dialog
            this.Parent.Controls.Remove(this); //Remove this.
        }
    }
}
