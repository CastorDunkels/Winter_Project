using System.IO;
using System;
using Raylib_cs;

Raylib.InitWindow(800, 600, "Game");
Raylib.SetTargetFPS(30);

const int GROUND = 580;
float playerX = 10;
float playerY = GROUND;
float speed = 6;
float WalkingSpeed = 0;
float FlyingSpeed = 0;
float Gravity = 0;
float Jump = -10;
float Acceleration = 0.6f;
float Direction = 0;
float leftWall = 0;
float jeffX = 500;
float jeffY = 0;

Rectangle playerRect = new Rectangle(playerX, playerY, 50, 20);
Rectangle jeff = new Rectangle(jeffX, jeffY, 100, 500);

while (Raylib.WindowShouldClose() == false)
{
    if (playerX > 600) {
        jeffX -= speed;
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

        }



    if (playerY - GROUND < -20)
    {
        playerX += FlyingSpeed;
    }

    playerY += Gravity;
    if (playerY > GROUND)  //detta gör så att gubben inte kan åka under marken
    {
        playerY = GROUND;
    }
    if (playerX < leftWall + 5)
    {
        playerX = leftWall;
    }
    if (playerX > Raylib.GetScreenWidth() - playerRect.width){
        playerX = Raylib.GetScreenWidth();
    }

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
    jeff.x = jeffX;
    jeff.y = jeffY;

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.SKYBLUE);
    Raylib.DrawRectangleRec(playerRect, Color.GOLD);
    Raylib.DrawRectangleRec(jeff, Color.BLACK);

    Console.WriteLine(WalkingSpeed);

    Raylib.EndDrawing();
}