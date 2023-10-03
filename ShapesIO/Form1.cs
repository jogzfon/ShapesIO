using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapesIO
{
    public partial class Form1 : Form
    {
        private int playerX = 100; // Initial player X position
        private int playerY = 100; // Initial player Y position
        private int playerSize = 50; // Player circle size
        private int moveDistance = 10; // Movement distance
        private int count = 0;
        private int minimumDistance = 25;
        private List<Enemy> enemies = new List<Enemy>();
        private Random random = new Random();

        private bool Left, Right, Up, Down;

        public Form1()
        {
            InitializeComponent();
            scoreTime.Start();
            timer.Start();
            enemyTimer.Start();
            SetUp();
        }

        private void SetUp()
        {
            this.Width = 1920;
            this.Height = 1080;
            score.Font = new Font("Arial", 20, FontStyle.Regular);
            this.BackgroundImage = Image.FromFile("Background.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;
        }

        private void Paint_Game(object sender, PaintEventArgs e)
        {
            // Create a Graphics object for drawing on the form
            Graphics g = e.Graphics;

            foreach (var enemy in enemies)
            {
                g.FillEllipse(Brushes.Red, enemy.X, enemy.Y, enemy.Size, enemy.Size);
            }

            g.FillEllipse(Brushes.Black, playerX, playerY, playerSize, playerSize);
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Spawn a new enemy with random position and radius
            if (Up)
            {
                playerY -= moveDistance;
            }
            if (Down)
            {
                playerY += moveDistance;
            }
            if (Left)
            {
                playerX -= moveDistance;
            }
            if (Right)
            {
                playerX += moveDistance;
            }
            // Redraw the form to display the new enemy
            Invalidate();
        }

        private void enemy_Timer(object sender, EventArgs e)
        {
            // Spawn a new enemy with random position and size
            int enemyX = random.Next(10, this.ClientSize.Width - 100); // Adjust the range as needed
            int enemyY = random.Next(10, this.ClientSize.Height - 100); // Adjust the range as needed
            int enemySize = random.Next(20, 50); // Adjust the size range as needed
            int enemySpeed = random.Next(1, 5);

            Enemy enemy = new Enemy(enemyX, enemyY, enemySize, enemySpeed);
            enemies.Add(enemy);

            // Move and update existing enemies
            foreach (var existingEnemy in enemies.ToList())
            {
                // Implement enemy movement based on their speed
                int deltaX = playerX - existingEnemy.X;
                int deltaY = playerY - existingEnemy.Y;
                double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

                if (distance > 0)
                {
                    int moveX = (int)(existingEnemy.Speed * deltaX / distance);
                    int moveY = (int)(existingEnemy.Speed * deltaY / distance);

                    existingEnemy.X += moveX;
                    existingEnemy.Y += moveY;
                }

                if (distance < minimumDistance)
                {
                    // Adjust the enemy's position to ensure it's at a safe distance
                    int newX, newY;
                    do
                    {
                        newX = random.Next(0, this.ClientSize.Width - existingEnemy.Size);
                        newY = random.Next(0, this.ClientSize.Height - existingEnemy.Size);
                    } while (CheckPlayerEnemyCollision(playerX, playerY, playerSize, new Enemy(newX, newY, existingEnemy.Size, existingEnemy.Speed)));

                    existingEnemy.X = newX;
                    existingEnemy.Y = newY;
                }

                // Check for collisions between enemies and the player here
                if (CheckPlayerEnemyCollision(playerX, playerY, playerSize, existingEnemy))
                {
                    GameOver();
                    // Remove the collided enemy
                    enemies.Remove(existingEnemy);
                }

                // Remove enemies that go out of bounds or collide with the player
                if (existingEnemy.X < 0 || existingEnemy.Y < 0 ||
                    existingEnemy.X + existingEnemy.Size > this.ClientSize.Width ||
                    existingEnemy.Y + existingEnemy.Size > this.ClientSize.Height)
                {
                    enemies.Remove(existingEnemy);
                }
            }
            Invalidate();
        }
        private void GameOver()
        {
            scoreTime.Stop();
            timer.Stop();
            enemyTimer.Stop();
            MessageBox.Show("Player collided with an enemy! Total score is: " + count+ " seconds");
            Application.Restart();
        }
        private void Score_Time(object sender, EventArgs e)
        {
            score.Text = "TIME: " + count; 
            count++;
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            // Handle arrow key input for player movement
            if (e.KeyCode == Keys.Left)
            {
                Left = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                Right = true;
            }
            if (e.KeyCode == Keys.Up)
            {
                Up = true;
            }
            if (e.KeyCode == Keys.Down)
            {
                Down = true;
            }
        }

        private void Starter_Time(object sender, EventArgs e)
        {

        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                Left = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                Right = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                Up = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                Down = false;
            }
        }
        private bool CheckPlayerEnemyCollision(int playerX, int playerY, int playerSize, Enemy enemy)
        {
            Rectangle playerRect = new Rectangle(playerX, playerY, playerSize, playerSize);
            Rectangle enemyRect = new Rectangle(enemy.X, enemy.Y, enemy.Size, enemy.Size);

            return playerRect.IntersectsWith(enemyRect);
        }
        public class Enemy
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Size { get; set; }
            public int Speed { get; set; }

            public Enemy(int x, int y, int size, int speed)
            {
                X = x;
                Y = y;
                Size = size;
                Speed = speed;
            }
        }
    }
}
