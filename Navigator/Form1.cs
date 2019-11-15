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
        Panel startPanel,
              goalPanel;
        Panel[] path;

        Color pathClr,
              wallClr;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GenerateBoard();
            GenerateStartLocation();
            GenerateGoalLocation();
            GenerateShortestPath(startPanel, goalPanel);
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
                    startPanel = _navigationGridPanels[row, col];
                    startPanel.BackColor = Color.GreenYellow;
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
                    goalPanel = _navigationGridPanels[row, col];
                    goalPanel.BackColor = Color.Red;
                    break;
                }
            }
        }

        private void GenerateShortestPath(Panel startPanel, Panel goalPanel)
        {
            Panel currentLoc = startPanel;

            if (GoalIsDirectlyRightOfStartPoint())
            {
                while (_navigationGridPanels[(currentLoc.Location.X + 1 / 40), (currentLoc.Location.Y / 40)].BackColor != wallClr &&
                       _navigationGridPanels[(currentLoc.Location.X + 1 / 40), (currentLoc.Location.Y / 40)].BackColor != goalPanel.BackColor)
                {
                    currentLoc = _navigationGridPanels[(currentLoc.Location.X / 40) + 1, (currentLoc.Location.Y / 40)];
                    currentLoc.BackColor = Color.Yellow;
                }
            }
            else if (GoalIsDirectlyLeftOfStartPoint())
            {
                while (_navigationGridPanels[(currentLoc.Location.X / 40) - 1, (currentLoc.Location.Y / 40)].BackColor != wallClr &&
                       _navigationGridPanels[(currentLoc.Location.X / 40) - 1, (currentLoc.Location.Y / 40)].BackColor != goalPanel.BackColor)
                {
                    currentLoc = _navigationGridPanels[(currentLoc.Location.X / 40) - 1, (currentLoc.Location.Y / 40)];
                    currentLoc.BackColor = Color.Yellow;
                }
            }
            else if (GoalIsDirectlyAboveStartPoint())
            {
                while (_navigationGridPanels[(currentLoc.Location.X / 40), (currentLoc.Location.Y / 40) - 1].BackColor != wallClr &&
                       _navigationGridPanels[(currentLoc.Location.X / 40), (currentLoc.Location.Y / 40) - 1].BackColor != goalPanel.BackColor)
                {
                    currentLoc = _navigationGridPanels[(currentLoc.Location.X / 40), (currentLoc.Location.Y / 40) - 1];
                    currentLoc.BackColor = Color.Yellow;
                }
            }
            else if (GoalIsDirectlyBelowStartPoint())
            {
                while (_navigationGridPanels[(currentLoc.Location.X / 40), (currentLoc.Location.Y / 40) + 1].BackColor != wallClr &&
                       _navigationGridPanels[(currentLoc.Location.X / 40), (currentLoc.Location.Y / 40) + 1].BackColor != goalPanel.BackColor)
                {
                    currentLoc = _navigationGridPanels[(currentLoc.Location.X / 40), (currentLoc.Location.Y / 40) + 1];
                    currentLoc.BackColor = Color.Yellow;
                }
            }
            else if (GoalIsBelowAndRightOfStartPoint())
            {
                
            }
            else if (GoalIsAboveAndRightOfStartPoint()) 
            {
                
            }
            else if (GoalIsAboveAndLeftOfStartPoint())
            {
                
            }
            else if (GoalIsAboveAndLeftOfStartPoint())
            {
                
            }
        }

        private Boolean GoalIsDirectlyAboveStartPoint()
        {
            return GoalIsAboveStartPoint() && GoalAndStartPointAreEqualHorizontally();
        }

        private Boolean GoalIsDirectlyBelowStartPoint()
        {
            return GoalIsBelowStartPoint() && GoalAndStartPointAreEqualHorizontally();
        }

        private Boolean GoalIsDirectlyRightOfStartPoint()
        {
            return GoalIsRightOfStartPoint() && GoalAndStartPointAreEqualVertically();
        }

        private Boolean GoalIsDirectlyLeftOfStartPoint()
        {
            return GoalIsLeftOfStartPoint() && GoalAndStartPointAreEqualVertically();
        }

        private Boolean GoalIsAboveAndLeftOfStartPoint()
        {
            return GoalIsAboveStartPoint() && GoalIsLeftOfStartPoint();
        }

        private Boolean GoalIsAboveAndRightOfStartPoint()
        {
            return GoalIsAboveStartPoint() && GoalIsRightOfStartPoint();
        }

        private Boolean GoalIsBelowAndLeftOfStartPoint()
        {
            return GoalIsBelowStartPoint() && GoalIsLeftOfStartPoint();
        }

        private Boolean GoalIsBelowAndRightOfStartPoint()
        {
            return GoalIsBelowStartPoint() && GoalIsRightOfStartPoint();
        }

        private Boolean GoalAndStartPointAreEqualVertically() {
            return startPanel.Location.Y == goalPanel.Location.Y;
        }

        private Boolean GoalAndStartPointAreEqualHorizontally()
        {
            return startPanel.Location.X == goalPanel.Location.X;
        }

        private Boolean GoalIsAboveStartPoint()
        {
            return startPanel.Location.Y > goalPanel.Location.Y;
        }

        private Boolean GoalIsBelowStartPoint()
        {
            return startPanel.Location.Y < goalPanel.Location.Y;
        }

        private Boolean GoalIsRightOfStartPoint()
        {
            return startPanel.Location.X < goalPanel.Location.X;
        }

        private Boolean GoalIsLeftOfStartPoint()
        {
            return startPanel.Location.X > goalPanel.Location.X;
        }
    }
}
