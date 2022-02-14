using System;
using Raylib_cs;

Raylib.InitWindow(800, 600, "Game");
Raylib.SetTargetFPS(30);

const int GROUND = 580;
float playerX = 10;
float playerY = GROUND;
//float WalkingSpeed = 6; ska göra något med denna senare
float SideGravity = 0;
float Gravity = 0;
float speed = 6;
float Acceleration = 0.6f;

Rectangle playerRect = new Rectangle(playerX, playerY, 50, 100);


while (Raylib.WindowShouldClose() == false)
{


    if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) playerX += speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A)){ 
        if (GROUND - playerY < 1){   //detta kollar om man är på marken eller inte så man inte kan fortsätta gå runt när man är i luften
        playerX -= speed;

        }
    }
    if (Raylib.IsKeyDown(KeyboardKey.KEY_SPACE)) Gravity = -4; //detta gör så att när man trycker på space så blir gravitationen minus så att man går uppot som är som att hoppa
    playerY += Gravity;
    if (playerY > GROUND)  //detta gör så att gubben inte kan åka under marken
    {
        playerY = GROUND;
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

    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.SKYBLUE);
    Raylib.DrawRectangleRec(playerRect, Color.GOLD);

    Raylib.EndDrawing();
}