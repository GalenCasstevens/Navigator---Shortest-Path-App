using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Navigator
{
    public partial class Form1 : Form
    {
        private Panel[,] _navigationGridPanels;
        Random rnd = new Random();
        Color pathClr,
              wallClr;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GenerateBoard();
            GenerateStartLocation();
            GenerateGoalLocation();
            // GenerateShortestPath(); not implemented yet
        }

        private void GenerateBoard()
        {
            const int tileSize = 40;
            const int gridSize = 12;
            pathClr = Color.Blue;
            wallClr = Color.DarkGray;

            _navigationGridPanels = new Panel[gridSize, gridSize];

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    var newPanel = new Panel
                    {
                        Size = new Size(tileSize, tileSize),
                        Location = new Point(tileSize * i, tileSize * j)
                    };

                    Controls.Add(newPanel);

                    _navigationGridPanels[i, j] = newPanel;

                    int wall = rnd.Next(10);

                    if (wall < 3)
                    {
                        newPanel.BackColor = wallClr;
                    }
                    else
                    {
                        newPanel.BackColor = pathClr;
                    }
                }
            }
        }

        private void GenerateStartLocation()
        {
            while (true)
            {
                int row = rnd.Next(12);
                int col = rnd.Next(12);

                if (_navigationGridPanels[row, col].BackColor == pathClr)
                {
                    _navigationGridPanels[row, col].BackColor = Color.GreenYellow;
                    break;
                }
            }
        }

        private void GenerateGoalLocation()
        {
            while (true)
            {
                int row = rnd.Next(12);
                int col = rnd.Next(12);

                if (_navigationGridPanels[row, col].BackColor == pathClr)
                {
                    _navigationGridPanels[row, col].BackColor = Color.Red;
                    break;
                }
            }
        }

        private void GenerateShortestPath()
        {

        }
    }
}
