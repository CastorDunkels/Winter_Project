using System;
using Raylib_cs;

Raylib.InitWindow(800, 600, "Game");
Raylib.SetTargetFPS(30);

const int GROUND = 580;
float playerHealth = 3;
float playerHit = playerHealth--;
float playerX = 400;
float playerY = GROUND;
float speed = 6;
float WalkingSpeed = 0;
float FlyingSpeed = 0;
float Gravity = 0;
float Jump = -10;
float Acceleration = 0.6f;
float Direction = 0;
float leftWall = 0;
float rightWall = 750;
float enemyX = 500;
float enemyY = GROUND - 20;

Rectangle playerRect = new Rectangle(playerX, playerY, 50, 20);
Rectangle enemy = new Rectangle(enemyX, enemyY, 40, 40);

while (Raylib.WindowShouldClose() == false)
{
    if (playerX < enemyX)
    {
        enemyX -= speed / 2;  //gör så att fienden går mot spelaren beroende på om man har högre x värde eller mindre x värde
    }
    if (playerX > enemyX)
    {
        enemyX += speed / 2;
    }



    if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
    {
        Direction = 1;
        if (GROUND - playerY < 1)
        { //detta kollar om man är på marken eller inte så man inte kan fortsätta gå runt när man är i luften
            playerX += speed;

            WalkingSpeed = speed * Direction;

        }

    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A))  //göra en metod för att gå kanske
    {
        Direction = -1;
        if (GROUND - playerY < 1)
        {
            playerX -= speed;

            WalkingSpeed = speed * Direction;

        }

    }


    if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE))
        if (GROUND - playerY < 1)
        {   //detta gör så att man inte kan hoppa mer än när man är på marken
            Gravity = Jump; //detta gör så att när man trycker på space så blir gravitationen minus så att man går uppot som är som att hoppa
            FlyingSpeed = WalkingSpeed;
            WalkingSpeed = 0;

        }



    if (playerY - GROUND < -20)
    {
        playerX += FlyingSpeed; //detta gör så att man håller kvar sin speed när man hoppar
    }

    playerY += Gravity;
    if (playerY > GROUND)  //detta gör så att gubben inte kan åka under marken eller gå ut åt sidorna
    {
        playerY = GROUND;
    }
    if (playerX < leftWall + 5)
    {
        playerX = leftWall;
    }
    if (playerX > rightWall)
    {
        playerX = rightWall;
    }

    if (enemyX < playerX + 50 && enemyX > playerX - 50 && enemyY <= playerY + 25) //kollar om enemy colliderar med player för att se om hp ska minskas
    {
        playerHealth = playerHit;
    }
    else
    {
        Console.WriteLine(playerHealth + 100);
    }
    if (playerHealth <= 0)
    {

        Raylib.DrawText("YOU DIED", 250, 250, 60, Color.RED);
    }
    Raylib.DrawText("Health: " + playerHealth, 40, 20, 30, Color.BLACK);  //skriver ut hur mycket hp man har

    if (playerY == GROUND)
    {
        Gravity = 0;
    }
    else
    {
        Gravity += Acceleration;  //detta gör så att när man är över marken så går gravity igång 
    }

    playerRect.y = playerY;  //detta gör så att visuella delen och modellen är separata
    playerRect.x = playerX;
    enemy.x = enemyX;
    enemy.y = enemyY;

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.SKYBLUE);
    Raylib.DrawRectangleRec(playerRect, Color.GOLD);
    Raylib.DrawRectangleRec(enemy, Color.BLACK);

    Raylib.EndDrawing();
}
