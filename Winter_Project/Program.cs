using System;
using Raylib_cs;

Raylib.InitWindow(800, 600, "Game");
Raylib.SetTargetFPS(30);

float playerX = 10;
float playerY = 580;

Rectangle playerRect = new Rectangle(playerX, playerY, 50, 20);
float speed = 2;

while (Raylib.WindowShouldClose() == false) {
    if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) playerRect.x += speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) playerRect.x -= speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) playerRect.y += speed;
    if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) playerRect.y -= speed;


    Raylib.BeginDrawing();

    Raylib.ClearBackground(Color.SKYBLUE);
    Raylib.DrawRectangleRec(playerRect, Color.GOLD);

    Raylib.EndDrawing();
}