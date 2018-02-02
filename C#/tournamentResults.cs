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
    public partial class tournamentResults : UserControl
    {
        public tournamentResults()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On load, build playerview and fill the view
        /// </summary>
        private void tournamentResults_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            playerView.Columns.Add("playerRank", "Rank");
            playerView.Columns.Add("playerName", "Player");
            playerView.Columns.Add("playerWins", "#Wins");
            playerView.Columns.Add("playerLosses", "#Losses");
            foreach (var player in Global.currentTournament.players)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(playerView, player.rank, player.name, player.wins, player.losses);
                playerView.Rows.Add(row);
            }
            playerView.Sort(playerView.Columns[0], ListSortDirection.Ascending);
        }

        /// <summary>
        /// Start a new tournament, with options to keep players
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult usePlayers = MessageBox.Show("Start with the same players?", "New Tournament", MessageBoxButtons.YesNo);
            if(usePlayers == DialogResult.Yes)
            {
                foreach (var player in Global.currentTournament.players)
                {
                    player.wins = 0;
                    player.rank = 0;
                    player.losses = 0;
                    player.hasBye = false;
                    player.byeCount = 0;
                    player.prevOpponents = new Dictionary<string, int>();
                }
                Global.currentTournament = new Tournament(Global.currentTournament.players);
            }else
            {
                Global.currentTournament = new Tournament();
            }
            if (this.Parent == null || this.Parent.GetType() != typeof(Panel))
            {
                return; //Future proofing
            }
            this.Parent.Controls.Add(new NewTournamentControl());//Add new tournament dialog
            this.Parent.Controls.Remove(this); //Remove this.
        }
    }
}
