namespace MazeGame
{
    public partial class Form1 : Form
    {
        private char[,] maze;
        private Point playerPosition;
        private int mazeWidth;
        private int mazeHeight;
        public Form1()
        {
            InitializeComponent();
            LoadMaze("maze.txt");
            DrawMaze();
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(MainForm_KeyDown);
        }

        private void LoadMaze(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            mazeWidth = lines[0].Length;
            mazeHeight = lines.Length;
            maze = new char[mazeHeight, mazeWidth];

            for (int i = 0; i < mazeHeight; i++)
            {
                for (int j = 0; j < mazeWidth; j++)
                {
                    maze[i, j] = lines[i][j];
                    if (maze[i, j] == 'S')
                    {
                        playerPosition = new Point(j, i);
                    }
                }
            }
        }

        private void DrawMaze()
        {
            Graphics g = panel1.CreateGraphics();
            int cellWidth = panel1.Width / mazeWidth;
            int cellHeight = panel1.Height / mazeHeight;

            for (int i = 0; i < mazeHeight; i++)
            {
                for (int j = 0; j < mazeWidth; j++)
                {
                    Brush brush;
                    switch (maze[i, j])
                    {
                        case '#':
                            brush = Brushes.Black;
                            break;
                        case ' ':
                            brush = Brushes.White;
                            break;
                        case 'S':
                            brush = Brushes.Green;
                            break;
                        case 'E':
                            brush = Brushes.Red;
                            break;
                        default:
                            brush = Brushes.White;
                            break;
                    }
                    g.FillRectangle(brush, j * cellWidth, i * cellHeight, cellWidth, cellHeight);
                }
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            int newX = playerPosition.X;
            int newY = playerPosition.Y;

            switch (e.KeyCode)
            {
                case Keys.Up:
                    newY--;
                    break;
                case Keys.Down:
                    newY++;
                    break;
                case Keys.Left:
                    newX--;
                    break;
                case Keys.Right:
                    newX++;
                    break;
            }

            if (newX >= 0 && newX < mazeWidth && newY >= 0 && newY < mazeHeight && maze[newY, newX] != '#')
            {
                playerPosition = new Point(newX, newY);
                DrawMaze();
                if (maze[newY, newX] == 'E')
                {
                    MessageBox.Show("Congratulations! You reached the exit!");
                }
            }
        }
    }
}
