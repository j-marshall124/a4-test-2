// Include the namespaces (code libraries) you need below.
using System;
using System.Numerics;

// The namespace your code is in.
namespace MohawkGame2D
{
    /// <summary>
    ///     Your game code goes inside this class!
    /// </summary>
    public class Game
    {
        // Place your variables here:
        Vector2 playerPosition = new Vector2(400, 700);
        float playerSize = 50;
        Vector2 enemyPosition = new Vector2(0, 300);
        Vector2 enemyPositionLeft = new Vector2(-900,500);
        Vector2 enemySize = new Vector2(100, 50);
        Vector2 enemyVelocity = new Vector2(100, 0);
        bool showGame = true;
        bool showGameOver = false;
        bool showGameWin = false;
        int lives = 3;

        /// <summary>
        ///     Setup runs once before the game loop begins.
        /// </summary>
        public void Setup()
        {
            Window.SetSize(800, 800);
            Window.SetTitle("Test");
            Window.TargetFPS = 60;
        }

        /// <summary>
        ///     Update runs every frame.
        /// </summary>
        public void Update()
        {
            Window.ClearBackground(Color.Black);
            Draw.LineSize = 0;
            Draw.FillColor = Color.Green;
            if (showGame == true)
            {
                Gameplay();
            }
            else if (showGameOver == true)
            {
                GameOver();
            }
            else if(showGameWin == true)
            {
                Win();
            }
            
        }

        public void Gameplay()
        {
            Draw.Rectangle(0, 700, Window.Width, Window.Height);
            Draw.Rectangle(0, 0, Window.Width, 100);
            Text.Color = Color.Black;
            Text.Draw($"Lives: {lives}", 10, 10);
            Player();
            EnemyRight();
            EnemyLeft();

            if (playerPosition.X >= enemyPosition.X && playerPosition.X <= enemyPosition.X + enemySize.X
                && playerPosition.Y >= enemyPosition.Y && playerPosition.Y <= enemyPosition.Y)
            {
                lives -= 1;
                playerPosition = new Vector2(400, 700);
            }
            else if (playerPosition.X >= enemyPositionLeft.X && playerPosition.X <= enemyPositionLeft.X + enemySize.X
                && playerPosition.Y >= enemyPositionLeft.Y && playerPosition.Y <= enemyPositionLeft.Y)
            {
                lives -= 1;
                playerPosition = new Vector2(400, 700);
            }

            if (playerPosition.Y <= 100)
            {
                showGame = false;
                showGameWin = true;
            }
            else if (lives <= 0)
            {
                showGame = false;
                showGameOver = true;
            }
        }
        public void Player()
        {
            // Setup
            Draw.FillColor = Color.LightGray;
            Draw.Square(playerPosition, playerSize);

            // Movement
            if (Input.IsKeyboardKeyPressed(KeyboardInput.A))
            {
                playerPosition.X -= 50;
            }
            if (Input.IsKeyboardKeyPressed(KeyboardInput.D))
            {
                playerPosition.X += 50;
            }
            if (Input.IsKeyboardKeyPressed(KeyboardInput.W))
            {
                playerPosition.Y -= 50;
            }
            if (Input.IsKeyboardKeyPressed(KeyboardInput.S))
            {
                playerPosition.Y += 50;
            }

            // Collision
            if (playerPosition.Y - playerSize < 0)
            {
                playerPosition.Y = 0;
            }
            if (playerPosition.Y + playerSize > 800)
            {
                playerPosition.Y = 750;
            }
            if (playerPosition.X - playerSize < 0)
            {
                playerPosition.X = 0;
            }
            if (playerPosition.X + playerSize > 800)
            {
                playerPosition.X = 750;
            }
        }

        public void EnemyRight()
        {
            Draw.FillColor = Color.Red;
            Draw.Rectangle(enemyPosition, enemySize);
            enemyPosition += enemyVelocity * Time.DeltaTime;
            if (enemyPosition.X > 800)
            {
                enemyPosition.X = -100;
            }
        }
        public void EnemyLeft()
        {
            Draw.FillColor = Color.Red;
            Draw.Rectangle(enemyPositionLeft, enemySize);
            enemyPositionLeft -= enemyVelocity * Time.DeltaTime;
            if (enemyPositionLeft.X < -100)
            {
                enemyPositionLeft.X = 900;
            }
        }

        public void Win()
        {
            Window.ClearBackground(Color.Black);
            Text.Color = Color.White;
            Text.Draw("YOU WIN", 300, 300);
            Text.Draw("Press 'Space' to play again", 150, 400);
            if (Input.IsKeyboardKeyPressed(KeyboardInput.Space))
            {
                playerPosition = new Vector2(400, 700);
                enemyPosition = new Vector2(0, 300);
                enemyPositionLeft = new Vector2(-900, 500);
                showGameWin = false;
                showGame = true;
            }

        }
        public void GameOver()
        {
            Window.ClearBackground(Color.Black);
            Text.Color = Color.White;
            Text.Draw("GAME OVER", 300, 300);
            Text.Draw("Press 'Space' to play again", 150, 400);
            if (Input.IsKeyboardKeyPressed(KeyboardInput.Space))
            {
                playerPosition = new Vector2(400, 700);
                enemyPosition = new Vector2(0, 300);
                enemyPositionLeft = new Vector2(-900, 500);
                lives = 3;
                showGameOver = false;
                showGame = true;
            }
        }
    }

}
